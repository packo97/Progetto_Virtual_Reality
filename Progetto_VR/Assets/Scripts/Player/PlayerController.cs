using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotSpeed;

    private float currentSpeed;
    [SerializeField] private float groundSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float airSpeed;
    
    
    private bool isJumping;
    
    private Rigidbody _rigidbody;
    private CapsuleCollider _capsuleCollider;

    private int _lives;
    private Vector3 respawnPosition;
    
    [SerializeField] private PhysicMaterial frictionPhysics;
    [SerializeField] private PhysicMaterial maxFrictionPhysics;
    [SerializeField] private PhysicMaterial airFrictionPhysics;
    [SerializeField] private float slopeLimit;
    
    private RaycastHit groundHit;
    
    private bool isClimbing;
    private Vector3 opposite_direction;
  
    private Transformation _transformation;


    private bool isDied;
    
    
    private void Awake()
    {
        Messenger.AddListener(GameEvent.LIFE_UP, LifeUp);
    }
    
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.LIFE_UP, LifeUp);
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _transformation = GetComponent<Transformation>();
        
        _lives = 5;
        isClimbing = false;
        isJumping = false;
        isDied = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isClimbing && !isDied)
            Move();

        if (!IsGrounded())
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        if(!isClimbing && !isJumping)
            if (Input.GetKeyDown(KeyCode.Space)) 
                Jump();
        
        if(isClimbing)
            if (Input.GetKeyDown(KeyCode.Space)) 
                UnStick();

        
        
        //ControlMaterialPhysics();

        
    }

    private void Move()
    {
        /*
         * Muove sulle assi  x e z considerando la rotazione attuale
         */
        
        Vector3 movement = Vector3.zero;
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        currentSpeed = 0;
        if (horInput != 0 || vertInput != 0)
        {
            _rigidbody.isKinematic = false;
            if (Input.GetKey(KeyCode.LeftShift) && !isJumping)
            {
                currentSpeed = groundSpeed * 2;
            }
            else if (Input.GetKey(KeyCode.LeftShift) && isJumping)
            {
                currentSpeed = airSpeed * 2;
            }
            else if (!isJumping)
            {
                currentSpeed = groundSpeed;
            }
            else if (isJumping)
            {
                currentSpeed = airSpeed;
            }
            
            //imposto il vettore del movimento
            movement.x = horInput * currentSpeed;
            movement.z = vertInput * currentSpeed;
            //evitare cose strane quando si muove in diagonale
            movement = Vector3.ClampMagnitude(movement, currentSpeed);
            //prendiamo la rotazione della camera
            Quaternion tmp = target.rotation;
            //modifichiamo gli angoli della camera, tenendo conto solo della rotazione intorno all'asse y
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            //modifichiamo il movement in base alle coordinate delle camera
            movement = target.TransformDirection(movement);
            //riassegniamo il valore precedentemente salvato
            target.rotation = tmp;
            
            //rotazione lenta
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }
        else
        {
            /*
             * Evito che la capsula scivoli sulle scale
             * 
             */
            Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f);
            //Debug.Log(hit.normal);
            if (hit.normal.y > 0 && hit.normal.y < 1)
            {
                _rigidbody.isKinematic = true;
            }
        }
        
        
        movement *= Time.deltaTime;
        
        //Debug.Log(groundHit.normal);
        _rigidbody.MovePosition(transform.position + movement);
    }
    
    private void Jump()
    {
        /*
         * Salta se sei attaccato al terreno
         * 
         */
        
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    }
    
    public bool backFlipTime = false;
    private void UnStick()
    {
        backFlipTime = true;
        Destroy(GetComponent<FixedJoint>());
        //StartCoroutine(BackFlipTime());
        backFlipTime = false;
        isClimbing = false;
        _transformation.ResetStick();
    }

    private IEnumerator BackFlipTime()
    {
        yield return new WaitForSecondsRealtime(3);
        
        backFlipTime = false;
        isClimbing = false;
        _transformation.ResetStick();
    }
    
    public bool IsGrounded()
    {
        /*
         * Controlla se siamo attaccati al terreno
         * 
         */
        
        float colliderHeight = _capsuleCollider.height + _capsuleCollider.radius * 2;
        Ray ray = new Ray(transform.position + new Vector3(0, colliderHeight / 2, 0), Vector3.down);
        
        if (Physics.Raycast(ray, out groundHit, (colliderHeight / 2) + 0.2f))
        {
            return true;
        }

        return false;
    }
    
    protected virtual void ControlMaterialPhysics()
    {
        // change the physics material to very slip when not grounded
        
        if ((IsGrounded() && GroundAngle() >= slopeLimit + 1) || _rigidbody.velocity == Vector3.zero )
            _capsuleCollider.material = maxFrictionPhysics;
        else if (IsGrounded() && _rigidbody.velocity != Vector3.zero)
            _capsuleCollider.material = frictionPhysics;
        else
            _capsuleCollider.material = airFrictionPhysics;
    }
    public virtual float GroundAngle()
    {
        var groundAngle = Vector3.Angle(groundHit.normal, Vector3.up);
        return groundAngle;
    }
    
    public void Hurt(Kill.TypeOfKill typeOfKill)
    {
        /*
         * Diminuisce la vita di 1
         * 
         */
        if (!isDied)
        {
            _lives -= 1;
            isDied = true;
            Messenger.Broadcast(GameEvent.PLAYER_DIE);
            //Debug.Log("Life " + _lives);
            if (typeOfKill == Kill.TypeOfKill.Electricity)
                StartCoroutine(KillTime());
            else
            {
                transform.position = respawnPosition;
                isDied = false;
            }
        }
        

    }

    private IEnumerator KillTime()
    {
        yield return new WaitForSecondsRealtime(8);
        transform.position = respawnPosition;
        isDied = false;
    }

    private void LifeUp()
    {
        _lives += 1;
        Debug.Log("vite: " + _lives);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        /*
         * 1 - Se collide con un oggetto che è taggato con "Respawn" -> Imposta la respawn postion
         *
         */
        
        if (other.tag.Equals("Respawn"))
        {
            respawnPosition = other.GetComponent<Transform>().position;
            //Debug.Log("Respawn position " + respawnPosition);
        }
    }

    public Vector3 getRespawnPosition()
    {
        return respawnPosition;
    }

    public void SetClimbing(bool value, Vector3 opposite_direction)
    {
        isClimbing = value;
        this.opposite_direction = opposite_direction;
    }

    public bool IsClimbing()
    {
        return isClimbing;
    }

    public bool IsJumping()
    {
        return isJumping;
    }

    public void SetJumpForce(float value)
    {
        jumpForce = value;
    }

    public float GetMoveSpeed()
    {
        return currentSpeed;
    }

    public bool IsDied()
    {
        return isDied;
    }
}
