using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarMovement : MonoBehaviour
{
    
    public string target = "end";

    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    [SerializeField] public float forceMovement;

    private bool isMoving;
    
    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Movement()
    {
        /*
         *  Movimento della piattaforma:
         *      1 - Se la piattaforma è nella startPosition, prendo come target da raggiungere l'end position
         *      2 - Se la piattaforma è nell' endPosition, prendo come target da raggiungere la start position
         */
        
        if (!isMoving)
            StartCoroutine(ForceMovement());
    }

    IEnumerator ForceMovement()
    {
        isMoving = true;
        Vector3 direction;

        if (target == "end")
        {
            direction = (end.position - start.position).normalized;
            while (target == "end")
            {
                if (Vector3.Distance(direction, Vector3.left) == 0)
                {
                    GetComponent<Rigidbody>().AddForce(new Vector3(-forceMovement,0, 0));
                }
                else if (Vector3.Distance(direction,Vector3.right)==0)
                {
                    GetComponent<Rigidbody>().AddForce(new Vector3(forceMovement, 0, 0));
                }
                else if (Vector3.Distance(direction,Vector3.forward)==0)
                {
                    GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, forceMovement));
                }
                else if (Vector3.Distance(direction,Vector3.back)==0)
                {
                    GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -forceMovement));
                }
                yield return new WaitForSeconds(0.3f);
            }
        }
        else if (target == "start")
        {
            direction = (start.position - end.position).normalized;
            while (target == "start")
            {
                if (direction == Vector3.left)
                    GetComponent<Rigidbody>().AddForce(new Vector3(-forceMovement,0, 0));
                else if (direction == Vector3.right)
                    GetComponent<Rigidbody>().AddForce(new Vector3(forceMovement,0, 0));
                else if (direction == Vector3.forward)
                    GetComponent<Rigidbody>().AddForce(new Vector3(0,0, forceMovement));
                else if (direction == Vector3.back)
                    GetComponent<Rigidbody>().AddForce(new Vector3(0,0, -forceMovement));
                yield return new WaitForSeconds(0.3f);
            }
        }
            
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.collider.name == "start")
        {
            isMoving = false;
            target = "end";
        }
            
        else if (other.collider.name == "end")
        {
            target = "start";
            isMoving = false;
        }
            
        
        
    }
    
    public bool IsMoving()
    {
        return isMoving;
    }

}
