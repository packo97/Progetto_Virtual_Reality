using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    
    private Rigidbody rigidbody;
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
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        
        
        _lives = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isClimbing)
            Move();
        /*else
            Climbing();*/


        if (Input.GetKeyDown(KeyCode.Space))
            if(!isClimbing)
                Jump();
            else 
                UnStick();

            if (Input.GetKeyDown(KeyCode.LeftShift))
            moveSpeed = 5;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            moveSpeed = 3;

        ControlMaterialPhysics();
    }

    private void Move()
    {
        /*
         * Muove sulle assi  x e z considerando la rotazione attuale
         */
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 direction = transform.right * x + transform.forward * z;

        direction *= moveSpeed;
        direction.y = rigidbody.velocity.y;

    
        rigidbody.velocity = direction;
  
        
        //rigidbody.MovePosition(transform.position + direction * Time.deltaTime);
    }
    
    void Climbing()
    {
        /*
         * Muove sulle assi x e y considerando la rotazione attuale
         * 
         */
        Vector3 direction = Input.GetAxis("Horizontal") * transform.right +  Input.GetAxis("Vertical") * transform.up;
        rigidbody.MovePosition(transform.position + direction * Time.deltaTime * 10);
    }
    

    private void Jump()
    {
        /*
         * Salta se sei attaccato al terreno
         * 
         */
        
        if (IsGrounded())
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void UnStick()
    {
        //unStickPhase = true;
        rigidbody.isKinematic = false;
        isClimbing = false;
        Vector3 unStickForce = opposite_direction * jumpForce;
        Debug.Log("unstick force: " + unStickForce + rigidbody.isKinematic);
        rigidbody.AddForce(unStickForce, ForceMode.Impulse);
        
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
        
        if ((IsGrounded() && GroundAngle() >= slopeLimit + 1) || rigidbody.velocity == Vector3.zero )
            _capsuleCollider.material = maxFrictionPhysics;
        else if (IsGrounded() && rigidbody.velocity != Vector3.zero)
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

  
}
