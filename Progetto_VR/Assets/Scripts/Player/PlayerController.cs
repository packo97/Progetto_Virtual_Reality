using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotSpeed;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    
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
    public bool unStickPhase = false;
    
    private Animator _animator;

    private Transformation _transformation;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _animator = GetComponent<Animator>();
        _transformation = GetComponent<Transformation>();
        _lives = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isClimbing)
            Move();
        
        if(!isClimbing && IsGrounded())
            if (Input.GetKeyDown(KeyCode.Space)) 
                Jump();
        
        if(isClimbing)
            if (Input.GetKeyDown(KeyCode.Space)) 
                UnStick();
        
        ControlMaterialPhysics();
    }

    private void Move()
    {
        /*
         * Muove sulle assi  x e z considerando la rotazione attuale
         */
        
        Vector3 movement = Vector3.zero;
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        
        if (horInput != 0 || vertInput != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = 10f;
            }
            else
            {
                moveSpeed = 5f;
            }
            
            //imposto il vettore del movimento
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            //evitare cose strane quando si muove in diagonale
            movement = Vector3.ClampMagnitude(movement, moveSpeed);
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
        
        
        //informo l'animator
        //_animator.SetFloat("Speed", movement.magnitude);
        //Debug.Log(movement.magnitude);
        movement *= Time.deltaTime;
        _rigidbody.MovePosition(transform.position + movement);
    }
    
    private void Jump()
    {
        /*
         * Salta se sei attaccato al terreno
         * 
         */
        
        //if (IsGrounded())
        //{
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        //_animator.SetBool("Jumping", true);
        //}
    }
    
    private void UnStick()
    {
        //unStickPhase = true;
        Destroy(GetComponent<FixedJoint>());
        isClimbing = false;
        _transformation.ResetStick();
        //Vector3 unStickForce = opposite_direction * jumpForce;
        //Debug.Log("unstick force: " + unStickForce + _rigidbody.isKinematic);
        //_rigidbody.AddForce(unStickForce, ForceMode.Impulse);
        
    }
    
    public bool IsGrounded()
    {
        /*
         * Controlla se siamo attaccati al terreno
         * 
         */
        
        //Debug.Log(Physics.Raycast(transform.position, -Vector3.up, 1f));
        float colliderHeight = _capsuleCollider.height;
        Ray ray = new Ray(transform.position + new Vector3(0, colliderHeight / 2, 0), Vector3.down);
        
        //Debug.Log(Physics.Raycast(ray, out groundHit, (colliderHeight / 2) + 0.2f));
        return Physics.Raycast(ray, out groundHit, (colliderHeight / 2) + 0.2f);
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
    
    public void Hurt()
    {
        /*
         * Diminuisce la vita di 1
         * 
         */
        
        _lives -= 1;
        Messenger.Broadcast(GameEvent.PLAYER_DIE);
        Debug.Log("Life " + _lives);
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
            Debug.Log("Respawn position " + respawnPosition);
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

    public void SetJumpForce(float value)
    {
        jumpForce = value;
    }
    
}
