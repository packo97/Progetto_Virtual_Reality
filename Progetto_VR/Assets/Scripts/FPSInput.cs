using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent((typeof(CharacterController)))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 12.0f;

    private CharacterController _charController;

    public float gravity = 0f;
    public bool isGrounded;
    public float jumpSpeed = 40.0F;
    private Vector3 moveDirection = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal -> A/D on the keyboard
        //Vertical -> W/S on the keyboard
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        //We use Time.deltaTime to have the same speed in different computers
        //transform.Translate(deltaX * Time.deltaTime,0,deltaZ * Time.deltaTime);

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
        
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
        
        //salto
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("SALTOOOOOO");
            moveDirection.y = jumpSpeed;
            moveDirection.y -= gravity * Time.deltaTime;
            _charController.Move(moveDirection * Time.deltaTime);
        }

    }
    void OnCollisionStay(){
        isGrounded = true;
    }
}
