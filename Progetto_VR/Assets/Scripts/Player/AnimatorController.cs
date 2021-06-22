using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _playerController;
    private Transformation _transformation;

    private InteractionWithObject _interactionWithObject;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _transformation = GetComponent<Transformation>();

        _interactionWithObject = GetComponent<InteractionWithObject>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Aggiornamento animazioni
         */
        
        int moveSpeed = (int) _playerController.GetMoveSpeed();
        _animator.SetInteger("Speed",moveSpeed);
        //Debug.Log(moveSpeed);

        bool isJumping = _playerController.IsJumping();
        _animator.SetBool("Jumping", isJumping);
        //Debug.Log(isJumping);

        int trasformation_index = (int) _transformation.transf;
        _animator.SetInteger("Trasformation", trasformation_index);
        //Debug.Log(trasformation_index);

        bool died = _playerController.IsDied();
        _animator.SetBool("Died", died);

        bool isClimbing = _playerController.IsClimbing();
        _animator.SetBool("Climbing", isClimbing);

        bool isBackFlipTime = _playerController.backFlipTime;
        _animator.SetBool("BackFlipTime", isBackFlipTime);
        //Debug.Log(_animator.GetCurrentAnimatorStateInfo(0).IsName("Happy Idle"));
        

        bool isTaken = _interactionWithObject.isTaken;
        _animator.SetBool("IsTaken", isTaken);
    }
}
