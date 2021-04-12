using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithObject : MonoBehaviour
{
    private GameObject interactable_object;
    private bool isTaken = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        /*
         * Pressione del tasto "E" per prendere o rilasciare oggetti che possiedono lo script "Interactable".
         * Se l'oggetto è stato già preso, lo rilascio nella mia posizione davanti al player.
         * Se l'oggetto non è stato preso, collide con la sfera di raggio 2 ed è davanti al player allora posso prendere l'oggetto.
         *  
         */



        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isTaken)
            {
                interactable_object.transform.position = transform.TransformPoint(0, 1, 1);
                interactable_object.SetActive(true);
                isTaken = false;
            }
            else
            {
                Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 2);
                foreach (Collider hitCollider in hitColliders)
                {
                    Debug.Log(hitCollider.name);
                    if (hitCollider.gameObject.GetComponent<Interactable>() != null)
                    {
                        interactable_object = hitCollider.gameObject;
                        Vector3 forward = transform.TransformDirection(Vector3.forward);
                        Vector3 toOther = interactable_object.transform.position - transform.position;
                        bool isInFrontOfMe = false;
                        if (Vector3.Dot(forward, toOther) > 0)
                        {
                            isInFrontOfMe = true;
                        }
                        
                        if (!isTaken && isInFrontOfMe)
                        {
                            interactable_object.SetActive(false);
                            isTaken = true;
                        }
                    
                    }
                }
            }
            
            
        }
    }
}

    
