using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    [SerializeField] float force;
    public enum ForceDirection {x=0, y, z};

    [SerializeField] ForceDirection dir;

    private void OnTriggerStay(Collider other)
    {
        /*
         * Quando un player si trova nell'area di trigger e ha la trasformazione CARTA attiva, riceve una forza
         * 
         */
        
        if (other.tag == "Player" && other.GetComponent<Transformation>().transf == Transformation.TypeOfTransformation.Carta)
        {
            if (dir == ForceDirection.x)
                other.GetComponent<Rigidbody>().AddForce(new Vector3(force, 0.25f, 0), ForceMode.Impulse);
            else if (dir == ForceDirection.y)
                other.GetComponent<Rigidbody>().AddForce(new Vector3(0, force, 0), ForceMode.Impulse);
            else if (dir == ForceDirection.z)
                other.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0.25f, force), ForceMode.Impulse);

        }
    }

    
    private void OnDisable()
    {
        /*
         * Quando lo script viene disattivato, disattivo in accordo rotazione e il box collider del ventolone
         * 
         */
        
        GetComponent<Spin>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        transform.GetChild(9).gameObject.SetActive(false);
        if (GetComponentInParent<SlideInput>()!=null)
            GetComponentInParent<SlideInput>().CloseDoor();
        
     
    }



    private void OnEnable()
    {
        /*
         * Quando lo script viene attivato, attivo la rotazione del ventolone e attivo il boxcollider
         * 
         */
        
        GetComponent<Spin>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        transform.GetChild(9).gameObject.SetActive(true);
        if (GetComponentInParent<SlideInput>()!=null){
            GetComponentInParent<SlideInput>().OpenDoor();
        }
        
    }
    
}
