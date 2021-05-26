using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithObject : MonoBehaviour
{
    private GameObject interactable_object;
    public bool isTaken = false;
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
                    
                    Destroy(interactable_object.GetComponent<FixedJoint>());
                    interactable_object.transform.position = transform.TransformPoint(0, 1, 1);
                    //interactable_object.transform.Rotate(0,0,0);
                    interactable_object.transform.eulerAngles = Vector3.zero;
                    isTaken = false;
                    break;
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
                            isTaken = true;
                            //Debug.Log(transform.TransformPoint(-0.2f, 1.7f, 0.4f));
                            // le due righe seguenti vanno fixate
                            interactable_object.transform.position = transform.TransformPoint(-0.2f, 1.7f, 0.4f);
                            interactable_object.transform.eulerAngles = new Vector3(0,0,-160);
                            //interactable_object.transform.Rotate(0,0,-160);
                            
                            interactable_object.AddComponent<FixedJoint>();
                            interactable_object.GetComponent<FixedJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();
                            
                            break;
                        }

                    }
                }
                
                if (transformation.transf == Transformation.TypeOfTransformation.Rame) 
                {
                    // se interagiamo con un electrical
                    if (hitCollider.gameObject.GetComponent<Electrical>() != null)
                    {
                        ElectricBehavior electricity = hitCollider.gameObject.GetComponentInChildren<ElectricBehavior>(true);
                        Vector3 forward = transform.TransformDirection(Vector3.forward);
                        Vector3 toOther = electricity.transform.position - transform.position;
                        bool isInFrontOfMe = false;
                        if (Vector3.Dot(forward, toOther) > 0)
                        {
                            isInFrontOfMe = true;
                        }

                        if (isInFrontOfMe)
                        {
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
                }
                
                /*
                 * Accendi/Spegni una macchina per inserire i codici.
                 *
                 */
                if (hitCollider.GetComponent<GateAccessMachine>())
                {
                    GateAccessMachine gateAccessMachine = hitCollider.GetComponent<GateAccessMachine>();
                    
                    Vector3 forward = transform.TransformDirection(Vector3.forward);
                    Vector3 toOther = gateAccessMachine.transform.position - transform.position;
                    
                    bool isInFrontOfMe = false;
                    if (Vector3.Dot(forward, toOther) > 0)
                    {
                        isInFrontOfMe = true;
                    }
                    
                    if (isInFrontOfMe)
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
                    
                    Vector3 forward = transform.TransformDirection(Vector3.forward);
                    Vector3 toOther = crateController.transform.position - transform.position;
                    bool isInFrontOfMe = false;
                    if (Vector3.Dot(forward, toOther) > 0)
                    {
                        isInFrontOfMe = true;
                    }
                    
                    if (!crateController.isOpen && isInFrontOfMe)
                    {
                        crateController.OpenCrate();
                    }
                    else if (crateController.isOpen && isInFrontOfMe)
                    {
                        crateController.CloseCrate();
                    }

                    // per evitare che due casse vicine vengano aperte contemporaneamente
                    break;
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

    
