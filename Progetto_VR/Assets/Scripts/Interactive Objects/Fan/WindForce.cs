using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    [SerializeField] float force;
    public enum ForceDirection {x=0, y, z};

    [SerializeField] ForceDirection dir;
    /*
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag=="Player" && other.GetComponent<Transformation>().transf==Transformation.TypeOfTransformation.Carta)
        {
            if(dir==ForceDirection.x)
                other.GetComponent<Rigidbody>().AddForce(new Vector3(force, 0.25f, 0), ForceMode.Impulse);
            else if (dir == ForceDirection.y)
                other.GetComponent<Rigidbody>().AddForce(new Vector3(0, force, 0), ForceMode.Impulse);
            else if (dir == ForceDirection.z)
                other.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0.25f, force), ForceMode.Impulse);

        }
    }
    */
    private void Start()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Player" && other.GetComponent<Transformation>().transf == Transformation.TypeOfTransformation.Carta)
        {
            Debug.Log(other);
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
        GetComponent<Spin>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        transform.GetChild(9).gameObject.SetActive(false);
        if (GetComponentInParent<SlideInput>()!=null)
            GetComponentInParent<SlideInput>().CloseDoor();
        
     
    }



    private void OnEnable()
    {
        GetComponent<Spin>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        transform.GetChild(9).gameObject.SetActive(true);
        if (GetComponentInParent<SlideInput>()!=null){
            GetComponentInParent<SlideInput>().OpenDoor();
        }
        
    }
    
}
