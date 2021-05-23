using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    [SerializeField] float force;
    public enum ForceDirection {x=0, y, z};

    [SerializeField] ForceDirection dir;
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag=="Player" && other.GetComponent<Transformation>().transf==Transformation.TypeOfTransformation.Carta)
        {
            if(dir==ForceDirection.x)
                other.GetComponent<Rigidbody>().AddForce(new Vector3(force, 0, 0), ForceMode.Impulse);
            else if (dir == ForceDirection.y)
                other.GetComponent<Rigidbody>().AddForce(new Vector3(0, force, 0), ForceMode.Impulse);
            else if (dir == ForceDirection.z)
                other.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, force), ForceMode.Impulse);

        }
    }
    private void OnDisable()
    {
        GetComponentInParent<Spin>().enabled = false;
    }



    private void OnEnable()
    {
        GetComponentInParent<Spin>().enabled = true;
    }
}
