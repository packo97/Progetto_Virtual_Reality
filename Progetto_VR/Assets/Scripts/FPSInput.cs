using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent((typeof(CharacterController)))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float _gravity = 9.81f;
    [SerializeField]
    private float _jumpSpeed = 3.5f;
    [SerializeField]
    private float _doubleJumpMultiplier = 0.5f;

    private CharacterController _controller;

    private float _directionY;

    private bool _canDoubleJump = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
      
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal -> A/D on the keyboard
        //Vertical -> W/S on the keyboard
        float deltaX = Input.GetAxis("Horizontal") * _moveSpeed;
        float deltaZ = Input.GetAxis("Vertical") * _moveSpeed;
        //We use Time.deltaTime to have the same speed in different computers
        //transform.Translate(deltaX * Time.deltaTime,0,deltaZ * Time.deltaTime);

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, _moveSpeed);
        movement.y = _gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        
        
        if (_controller.isGrounded)
        {
            _canDoubleJump = true;

            if (Input.GetButtonDown("Jump"))
            {
                _directionY = _jumpSpeed;
            }
        } else {
            if(Input.GetButtonDown("Jump") && _canDoubleJump) {
                _directionY = _jumpSpeed * _doubleJumpMultiplier;
                _canDoubleJump = false;
            }
        }
        
        _directionY -= _gravity * Time.deltaTime;

        movement.y = _directionY;

        _controller.Move(movement);
    }
}
