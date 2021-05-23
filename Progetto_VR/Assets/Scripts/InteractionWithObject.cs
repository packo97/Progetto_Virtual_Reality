using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithObject : MonoBehaviour
{
    private GameObject interactable_object;
    private bool isTaken = false;
    private Transformation transformation;

    
    [SerializeField] private int electricalTime;
    
    public bool isEnteringCode;
    
    // Start is called before the first frame update
    void Start()
    {
        transformation = GetComponent<Transformation>();
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
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 3);
        if (Input.GetKeyDown(KeyCode.E))
        {
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
                
                if (transformation.transf == Transformation.TypeOfTransformation.Rame) 
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
                            Messenger<float>.Broadcast(GameEvent.ON_STICK_TIME, electricalTime);
                            StartCoroutine(ElectricalTime());
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
                
                /*
                 * Accendi/Spegni una macchina per inserire i codici.
                 *
                 */
                if (hitCollider.GetComponent<GateAccessMachine>())
                {
                    GateAccessMachine gateAccessMachine = hitCollider.GetComponent<GateAccessMachine>();
                    gateAccessMachine.AccessMachine();
                    //isEnteringCode = true;
                }
                
                /*
                 * Apri cassa.
                 * 
                 */

                if (hitCollider.GetComponent<CrateController>())
                {
                    CrateController crateController = hitCollider.GetComponent<CrateController>();
                    if (!crateController.isOpen)
                    {
                        Debug.Log("Prendi una " + crateController.contenuto);
                        crateController.OpenCrate();
                    }
                    else
                    {
                        Debug.Log("chiudi cassa");
                        crateController.CloseCrate();
                    }
                }
                
            }
        }

        /*
         * Inserisci codice nella macchina accesa.
         * 
         */
        
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<GateAccessMachine>())
            {
                GateAccessMachine gateAccessMachine = hitCollider.GetComponent<GateAccessMachine>();
                if (gateAccessMachine.isActive)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
                        gateAccessMachine.InsertCode('0');
                    else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                        gateAccessMachine.InsertCode('1');
                    else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                        gateAccessMachine.InsertCode('2');
                    else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
                        gateAccessMachine.InsertCode('3');
                    else if(Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
                        gateAccessMachine.InsertCode('4');
                    else if(Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
                        gateAccessMachine.InsertCode('5');
                    else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
                        gateAccessMachine.InsertCode('6');
                    else if(Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
                        gateAccessMachine.InsertCode('7');
                    else if(Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
                        gateAccessMachine.InsertCode('8');
                    else if(Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
                        gateAccessMachine.InsertCode('9');
                }
            }
        }



    }
    
    
    private IEnumerator ElectricalTime()
    {
        Debug.Log("numero chiamate");
        while (electricalTime > 0)
        {
            if (!GameEvent.isPaused)
            {
                electricalTime -= 1;
                Messenger.Broadcast(GameEvent.DECREASE_STICK_TIME);
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
        
        ElectricBehavior electricityOnPlayer = GetComponentInChildren<ElectricBehavior>(true);
        electricityOnPlayer.ElectricSwitch();
        transformation.transf = Transformation.TypeOfTransformation.Default;
        transformation.ChangeMaterial();
        //isStickTime = false;
        electricalTime = 800;
        
        Messenger.Broadcast(GameEvent.OFF_STICK_TIME);
        
    }
}

    
