using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transfomation : MonoBehaviour
{
    //Tipi di materiale per le trasformaz<ioni
    public enum Transformation {Default=0 , Rame, Carta, Ghiaccio, Gomma, Colla};
    public Transformation transf;
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

   
    void Start()
    {
        transf = Transformation.Default;
        
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        
        particelle = GetComponentInChildren<ParticleSystem>();

        _electricBehavior = GetComponentInChildren<ElectricBehavior>(true);
        _rigidbody = GetComponent<Rigidbody>();
        _jumpLimit = 750f;
        _isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(transformInput))
        {
            transf=(Transformation)(((int)transf+1)%6);
            ChangeMaterial();
            particelle.Play();
            _electricBehavior.Reset();
        }
        

    }
    
    private void ChangeMaterial()
    {
        Material[] materials = meshRenderer.materials;
        
        Material temp;
        if (transf == Transformation.Rame)
        {
            temp = Rame;
        }
        else if (transf == Transformation.Carta)
        {
            temp = Carta;
        }
        else if (transf == Transformation.Ghiaccio)
        {
            temp = Ghiaccio;
        }
        else if (transf == Transformation.Gomma)
        {
            temp = Gomma;
        }
        else if (transf == Transformation.Colla)
        {
            temp = Colla;
        }
        else
        {
            temp = Default;
        }

        materials[0] = temp;
        meshRenderer.materials = materials;
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

        if (!_isGrounded && transf == Transformation.Gomma)
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

    }


    private void OnCollisionStay(Collision other)
    {
        /*
        if (transf == Transformation.Colla && other.gameObject.tag.Equals("Climbing") && _rigidbody.velocity.y == Vector3.zero.y)
        {
            if (Input.GetKey(KeyCode.W))
            {
                _rigidbody.AddForce(new Vector3(0,12,0), ForceMode.VelocityChange);
            }
            
        }
        */
    }

    private void OnCollisionExit(Collision collision)
    {
        /*
         *  Se la trasformazione attiva è di gomma e il player si stacca da una piattaforma
         *  o terreno viene salvato il punto di stacco e la booleana di stacco viene settata a true.
         *
         */

        if (_isGrounded && transf == Transformation.Gomma)
        {
            _jumpPoint = transform.position.y;
            _isGrounded = false;
            //Debug.Log(_jumpPoint);
        }

    }
    
    
}
