using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithObject : MonoBehaviour
{
    private GameObject interactable_object;
    private bool isTaken = false;
    private Transfomation transformation;

    // Start is called before the first frame update
    void Start()
    {
        transformation = GetComponent<Transfomation>();
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
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 2);
            
            foreach (Collider hitCollider in hitColliders)
            {
                if (isTaken)
                {
                    interactable_object.transform.position = transform.TransformPoint(0, 1, 1);
                    interactable_object.SetActive(true);
                    isTaken = false;
                }
                else
                {
                    //Debug.Log(hitCollider.name);
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
                
                if (transformation.transf == Transfomation.Tranformation.Rame) 
                {
                    // se interagiamo con un electrical
                    if (hitCollider.gameObject.GetComponent<Electrical>() != null)
                    {
                        ElectricBehavior electricity = hitCollider.gameObject.GetComponentInChildren<ElectricBehavior>(true);
                        //vediamo se l'electrical è attivo
                        bool isElectricalActive = electricity.gameObject.activeSelf;
                        
                        ElectricBehavior electricityOnPlayer = GetComponentInChildren<ElectricBehavior>(true);
                        //se interagiamo con l'electrical active ci attiviamo le nostre particelle
                        if (isElectricalActive)
                        {
                            Debug.Log("electrical acceso");
                            electricityOnPlayer.ElectricSwitch();
                            Debug.Log(electricityOnPlayer.gameObject.activeSelf);
                        }
                        else //se interagiamo con l'electrical spento ma noi siamo carichi lo attiviamo e noi perdiamo la carica
                        {
                            Debug.Log("electrical spento");
                            //vediamo se noi siamo carichi
                            bool isElectricityActiveOnPlayer = electricityOnPlayer.gameObject.activeSelf;
                            if (isElectricityActiveOnPlayer)
                            {
                                electricity.ElectricSwitch();
                                electricityOnPlayer.ElectricSwitch();
                            }
                        }
                    }
                }
            }
        }
    }
}

    
