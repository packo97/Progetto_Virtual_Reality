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

    public bool isMoving;

    private ConstantForce _constantForce;
    [SerializeField] private float constantForceValue;
    private Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        _constantForce = GetComponent<ConstantForce>();
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
        if (other.collider.name.Equals("start"))
        {
            isMoving = false;
            target = "end";
        }
            
        else if (other.collider.name.Equals("end"))
        {
            target = "start";
            isMoving = false;
        }
    }
    
    public bool IsMoving()
    {
        return isMoving;
    }


    public void AddConstantForce()
    {
        _constantForce.enabled = true;
        if (target == "end")
        {
            direction = (end.position - start.position).normalized;
        }
        else
        {
            direction = (start.position - end.position).normalized;
        }
        if (Vector3.Distance(direction, Vector3.left) == 0)
        {
            _constantForce.relativeForce = new Vector3(-constantForceValue, 0, 0);
        }
        else if (Vector3.Distance(direction,Vector3.right)==0)
        {
            _constantForce.relativeForce = new Vector3(constantForceValue, 0, 0);
        }
        else if (Vector3.Distance(direction,Vector3.forward)==0)
        {
            _constantForce.relativeForce = new Vector3(0, 0, constantForceValue);
        }
        else if (Vector3.Distance(direction,Vector3.back)==0)
        {
            _constantForce.relativeForce = new Vector3(0, 0, -constantForceValue);
        }
    }

    public void RemoveConstantForce()
    {
        _constantForce.enabled = false;
    }
}
