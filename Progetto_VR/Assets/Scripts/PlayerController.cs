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
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

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
    public bool IsGrounded()
    {
        /*
         * Controlla se siamo attaccati al terreno
         * 
         */
        
        //Debug.Log(Physics.Raycast(transform.position, -Vector3.up, 1f));
        float colliderHeight = _capsuleCollider.height;
        Ray ray2 = new Ray(transform.position + new Vector3(0, colliderHeight / 2, 0), Vector3.down);
        //Debug.Log(Physics.Raycast(ray2, out groundHit, (colliderHeight / 2) + 0.2f));
        return Physics.Raycast(ray2, out groundHit, (colliderHeight / 2) + 0.2f);
    }
    private void Hurt()
    {
        /*
         * Diminuisce la vita di 1
         * 
         */
        
        _lives -= 1;
        Debug.Log("Life " + _lives);
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
    
    

    private void OnTriggerEnter(Collider other)
    {
        /*
         * 1 - Se collide con un oggetto che è taggato con "Respawn" -> Imposta la respawn postion
         * 2 - Se collide con un oggetto che è taggato con "Die" -> Imposta la posizione attuale alla respawn position
         *     e diminuisci la vita di 1.
         */
        
        if (other.tag.Equals("Respawn"))
        {
            respawnPosition = other.GetComponent<Transform>().position;
            Debug.Log("Respawn position " + respawnPosition);
        }
        else if (other.tag.Equals("Die"))
        {
            transform.position = respawnPosition;
            Hurt();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        /*
         * 1 - Se collide con particelle che sono taggate cond "Die" -> cambia la posizione attuale nella respawn position
         *     e diminuisci la vita di 1.
         */
        
        if (other.tag.Equals("Die"))
        {
            transform.position = respawnPosition;
            Hurt();
        }
    }
}
