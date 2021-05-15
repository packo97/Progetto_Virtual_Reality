using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Transformation : MonoBehaviour
{
    //Tipi di materiale per le trasformaz<ioni
    public enum TypeOfTransformation {Default=0, Gomma, Rame, Ghiaccio, Colla, Carta};
    public TypeOfTransformation transf;
    [SerializeField] private Material Default;
    [SerializeField] private Material Rame;
    [SerializeField] private Material Carta;
    [SerializeField] private Material Ghiaccio;
    [SerializeField] private Material Gomma;
    [SerializeField] private Material Colla;

    private MeshRenderer meshRenderer;

    //particellare per la trasformazione
    private ParticleSystem particelle;

    //tasto per la trasformazione
    public KeyCode transformInput=KeyCode.Tab;

    private ElectricBehavior _electricBehavior;
    private bool _isGrounded;
    private Rigidbody _rigidbody;
    
    private float _jumpPoint, _collisionPoint, _distance, _jumpLimit;
    [SerializeField] private float _iceBreakLimit;
    
    [SerializeField] private GameObject icePlanePrefab;
    private RaycastHit iceHit;
    private float _fallPoint;
    
    private bool isStickTime = false;

    [SerializeField] private int stickTime;

    private PlayerController player;
    private Animator _animator;

    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private Material[] defaultMaterials;
    
    private void Awake()
    {
        Messenger<TypeOfTransformation>.AddListener(GameEvent.SELECTED_TRANSFORMATION, ApplyTransformation);
    }
    
    private void OnDestroy()
    {
        Messenger<TypeOfTransformation>.RemoveListener(GameEvent.SELECTED_TRANSFORMATION, ApplyTransformation);
    }

    private void ApplyTransformation(TypeOfTransformation type)
    {
        /*
         * Applico la trasformazione selezionata dalla UI.
         * Cambio il material e applico il particellare.
         * Faccio il reset di tutte le variabili perchè ho cambiato trasformazione.
         * 
         */
        
        transf = type;

        ChangeMaterial();
        particelle.Play();
        //_animator.SetInteger("Trasformation", (int) transf);
        
        if (transf == TypeOfTransformation.Carta)
        {
            player.SetJumpForce(50);
            _rigidbody.mass = 10;
        }
        else
        {
            player.SetJumpForce(350);
            _rigidbody.mass = 50;
        }
        
        _electricBehavior.Reset();
        _rigidbody.isKinematic = false;
        isStickTime = false;
        GetComponent<PlayerController>().SetClimbing(false, Vector3.zero);
        if(transf != TypeOfTransformation.Colla)
            Messenger.Broadcast(GameEvent.OFF_STICK_TIME);
    }

    void Start()
    {
        transf = TypeOfTransformation.Default;
        
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        
        particelle = GetComponentInChildren<ParticleSystem>();

        _electricBehavior = GetComponentInChildren<ElectricBehavior>(true);
        _rigidbody = GetComponent<Rigidbody>();
        _jumpLimit = 750f;
        _isGrounded = true;
        

        _fallPoint = transform.position.y;

        player = GetComponent<PlayerController>();
        _animator = GetComponentInChildren<Animator>();
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        defaultMaterials = _skinnedMeshRenderer.materials;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(transformInput))
        {
            transf=(TypeOfTransformation)(((int)transf+1)%6);
            ChangeMaterial();
            particelle.Play();
            _electricBehavior.Reset();
            _rigidbody.isKinematic = false;
            isStickTime = false;
            GetComponent<PlayerController>().SetClimbing(false, Vector3.zero);
            if(transf != TypeOfTransformation.Colla)
                Messenger.Broadcast(GameEvent.OFF_STICK_TIME);
        }
        */
        
        if (transform.position.y > _fallPoint)
            _fallPoint = transform.position.y;

        if (Input.GetMouseButton(1))
        {
            Messenger.Broadcast(GameEvent.OPEN_MENU_TRANSFORMATION);
        }

    }
    
    private void ChangeMaterial()
    {
        /*
         * Cambio il material del player a seconda della trasformazione attualmente applicata
         * 
         */
        
        //Material[] materials = meshRenderer.materials;
        Material[] materials = _skinnedMeshRenderer.materials;
        
        Material temp = null;
        if (transf == TypeOfTransformation.Rame)
        {
            temp = Rame;
        }
        else if (transf == TypeOfTransformation.Carta)
        {
            temp = Carta;
        }
        else if (transf == TypeOfTransformation.Ghiaccio)
        {
            temp = Ghiaccio;
        }
        else if (transf == TypeOfTransformation.Gomma)
        {
            temp = Gomma;
        }
        else if (transf == TypeOfTransformation.Colla)
        {
            temp = Colla;
        }

        if (transf == TypeOfTransformation.Default)
        {
            materials = defaultMaterials;
            temp = materials[0];
        }
        else
        for (int i = 0; i < materials.Length; i++)
            materials[i] = temp;
        
        //Debug.Log("cambia material");

        //materials[0] = temp;
        //meshRenderer.materials = materials;
        _skinnedMeshRenderer.materials = materials;
        particelle.startColor = temp.color;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        /*
         Se la trasformazione attiva è di gomma e il player non è attaccato al terreno calcolo la 
        distanza tra il punto di stacco e di collisione che ne determina la forza con cui 
        il player rimbalza (sopra un certo limite la forza di rimbalzo viene bloccata ad 
        un determinato gap). finche la distanza di rimbalzo non sarà trascurabile il player continuerà
        a rimbalzare ma con forza minore.
         */

        if (!_isGrounded && transf == TypeOfTransformation.Gomma)
        {
            _collisionPoint = transform.position.y;
            _distance = _jumpPoint - _collisionPoint;

            if (_distance < 0)
                _distance = 0;

            Vector3 vec = new Vector3(0, 150 * _distance, 0);

            if (_distance * 150 > _jumpLimit)
                vec.y = _jumpLimit;
            
            _rigidbody.AddForce(vec, ForceMode.Impulse);
            _jumpPoint /= 2;

            if (_distance < 1)
                _isGrounded = true;
            
            _distance = 0;
        }

        if (transf == TypeOfTransformation.Ghiaccio)
        {
            if (collision.gameObject.tag.Equals("Water"))
            {
                /*
                * Se sono di ghiaccio e tocco l'acqua, istanzio un plabe di ghiaccio sotto i piedi del player
                * e faccio partire una coroutine che gestisce il tempo di vita del ghiaccio
                */
                GameObject _icePlane = Instantiate(icePlanePrefab) as GameObject;
                _icePlane.transform.position = transform.position;
            
                StartCoroutine(_icePlane.GetComponent<IcePlane>().IceTime());
            }
            else
            {
                _collisionPoint = transform.position.y;
                _distance = _fallPoint - _collisionPoint;
                
                if (_distance >= _iceBreakLimit)
                {
                    PlayerController playerController = GetComponent<PlayerController>();
                    transform.position = playerController.getRespawnPosition();
                    playerController.Hurt();
                }
                _fallPoint = _collisionPoint;
            }
                
        }
        
        if (transf == TypeOfTransformation.Colla)
        {
            float knees = transform.position.y + GetComponent<CapsuleCollider>().height / 2;
            
            float normal_y = collision.contacts[0].normal.y;
            bool horizontalCollision = !(normal_y >= 0.9f && normal_y <= 1.1);
            
            bool collisionAboveKnees = false;
            foreach (ContactPoint contactPoint in collision.contacts)
            {
                if (contactPoint.point.y > knees)
                    collisionAboveKnees = true;
            }
            
            //Debug.Log(normal_y);
            if (horizontalCollision && collisionAboveKnees)
            {
                //transform.SetParent(collision.collider.transform,true);
                
                gameObject.AddComponent<FixedJoint>();
                GetComponent<FixedJoint>().connectedBody = collision.rigidbody;
                
                
                //Debug.Log("collisione con muro");
                GetComponent<PlayerController>().SetClimbing(true, collision.contacts[0].normal);
                //_rigidbody.isKinematic = true;
                //_rigidbody.detectCollisions = true;
                
               if (!isStickTime)
               {
                   //Debug.Log("parte il collatima");
                   isStickTime = true;
                   Messenger.Broadcast(GameEvent.ON_STICK_TIME);
                   StartCoroutine(StickTime());
               }
            }
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        /*
         *  Se la trasformazione attiva è di gomma e il player si stacca da una piattaforma
         *  o terreno viene salvato il punto di stacco e la booleana di stacco viene settata a true.
         *
         */
        
        if (transf == TypeOfTransformation.Colla)
        {
            //transform.parent = null;
            //GetComponent<FixedJoint>().connectedBody = null;
        }
            if ( _isGrounded && (transf == TypeOfTransformation.Gomma))
        {
            _jumpPoint = transform.position.y;
            _isGrounded = false;
            //Debug.Log(_jumpPoint);
        }
    }
    private IEnumerator StickTime()
    {
        while (stickTime > 0)
        {
            if (!GameEvent.isPaused)
            {
                stickTime -= 1;
                Messenger.Broadcast(GameEvent.DECREASE_STICK_TIME);
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }

        Destroy(GetComponent<FixedJoint>());
        
        GetComponent<PlayerController>().SetClimbing(false, Vector3.zero);
        transf = TypeOfTransformation.Default;
        ChangeMaterial();
        isStickTime = false;
        stickTime = 80;
        
        Messenger.Broadcast(GameEvent.OFF_STICK_TIME);
        
    }

    public void ResetStick()
    {
        stickTime = 0;
    }
}
