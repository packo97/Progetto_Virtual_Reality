using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private ContactPoint _contactPoint;
    
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private float footstepLength;
    [SerializeField] private AudioClip footstep;
    [SerializeField] private AudioClip footstepWater;
    
    private bool step;
    public bool onWater;

    private int Nroom;
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
        
        
        /*
         * Per evitare problemi con frame alti
         */
        
        //#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
        //#endif
        


        step = true;
        onWater = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isClimbing && !isDied && !GameEvent.isPaused)
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

        
    }

    private void Move()
    {
        /*
         * Muove sulle assi  x e z considerando la rotazione attuale
         *
         * Imposto la velocita in base a se sono grounded o in aria.
         * Corro se premo Left Shift
         * 
         */
        
        float adjustedFootstepLength = footstepLength;
        
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
                adjustedFootstepLength= footstepLength/2;
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
            
            movement *= Time.deltaTime;
            
            
            Vector3 surface_normal = groundHit.normal;
            
            Vector3 movement_XZ = new Vector3(movement.x,0, movement.z);
            Vector3 rb_velocity_XZ = new Vector3(_rigidbody.velocity.x,0, _rigidbody.velocity.z);
            Vector3 forceMovement = movement_XZ.normalized * currentSpeed - rb_velocity_XZ;
            
            /*
             * Per le slope surface potrebbe aiutare
             */
            //Vector3 temp = Vector3.Cross(surface_normal, forceMovement);
            //Vector3 forceMovementWithSlope = Vector3.Cross(temp, surface_normal);
            //movement *= Time.deltaTime;
            
            //la seguente riga non funziona benissimo con le collisioni
            //_rigidbody.MovePosition(transform.position + movement);
            
            Debug.Log(forceMovement);
            _rigidbody.AddForce(forceMovement, ForceMode.VelocityChange);
            
            if(step && !isJumping){
                if(!onWater){
                    GetComponent<AudioSource>().PlayOneShot(footstep);
                    StartCoroutine(WaitForFootSteps(adjustedFootstepLength));
                }
                else{
                    GetComponent<AudioSource>().PlayOneShot(footstepWater);
                    StartCoroutine(WaitForFootSteps(adjustedFootstepLength));
                }
            }
        }
        else
        {
            /*
             * Evito che la capsula scivoli sulle scale
             * 
             */
            
            if (groundHit.collider != null && groundHit.collider.tag.Equals("Stair"))
                _rigidbody.isKinematic = true;
        }
        

    }
    
    private IEnumerator WaitForFootSteps(float stepLength)
    {
        step=false;
        yield return new WaitForSeconds(stepLength);
        step=true;
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
        
        if (Physics.Raycast(ray, out groundHit, (colliderHeight / 2) + 1f))
        {
            //Debug.Log("Grounded");
            return true;
        }

        if (somethingIsColliding)
        {
            return true;
        }
        
        return false;
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
            if (_lives > 0)
            {
                Messenger.Broadcast(GameEvent.PLAYER_DIE);
                Debug.Log("Life " + _lives);
                if (typeOfKill == Kill.TypeOfKill.Electricity)
                    StartCoroutine(KillTime());
                else
                {
                    transform.position = respawnPosition;
                    isDied = false;
                    
                    LoadDynamicOBJ();
                }    
            }
            else
            {
                Messenger.Broadcast(GameEvent.PLAYER_DIE);
                Messenger.Broadcast(GameEvent.GAME_OVER);
                StartCoroutine(BackToStartScene());
            }
            GetComponent<AudioSource>().PlayOneShot(hurtSound);
        }
    }

    private IEnumerator BackToStartScene()
    {
        /*
         * Ricarico la scena
         * 
         */
        
        yield return new WaitForSecondsRealtime(10);
        SceneManager.LoadScene("GameScene");
    }

    private IEnumerator KillTime()
    {
        /*
         * Periodo di morte
         * 
         */
        
        yield return new WaitForSecondsRealtime(6);
        transform.position = respawnPosition;
        isDied = false;
        
        LoadDynamicOBJ();
    }

    private void LifeUp()
    {
        _lives += 1;
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


    private bool somethingIsColliding;
    private void OnCollisionStay(Collision other)
    {
        somethingIsColliding = true;
    }

    private void OnCollisionExit(Collision other)
    {
        somethingIsColliding = false;
    }

    public void SetRoom(int r)
    {
        Nroom = r;
    }

    public void LoadDynamicOBJ()
    {
        /*
         * Ricarico la stanza dove sono morto
         * 
         */
        
        GameObject tmp = GameObject.FindWithTag("Room_"+Nroom);
        tmp.GetComponent<LoadObject>().ReloadRoom();
    }
}
