using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInput : MonoBehaviour
{
    [SerializeField] private bool isOpen = false;
    [System.Serializable] public enum Method {UpDownY=0, RightLeftX, ForwardBackwardZ};
    
    //i limiti sono le posizioni locali di y o x 
    [SerializeField] private float OpenLimit;
    [SerializeField] private float CloseLimit;
    public Method mod;


    // Update is called once per frame
    void Update()
    {
    
        if(isOpen == true && mod == Method.UpDownY && transform.localPosition.y < OpenLimit)
            transform.Translate(Vector3.up * (Time.deltaTime * 4));
        else if(isOpen == false && mod == Method.UpDownY && transform.localPosition.y > CloseLimit)
            transform.Translate(Vector3.down * (Time.deltaTime * 4));
        else if (isOpen == true && mod == Method.ForwardBackwardZ && transform.localPosition.z < OpenLimit)
            transform.Translate(Vector3.forward * (Time.deltaTime * 4));
        else if (isOpen == false && mod == Method.ForwardBackwardZ && transform.localPosition.z > CloseLimit)
            transform.Translate(Vector3.back * (Time.deltaTime * 4));
        else if (isOpen == true && mod == Method.RightLeftX && transform.localPosition.x < OpenLimit)
            transform.Translate(Vector3.right * (Time.deltaTime * 4));
        else if (isOpen == false && mod == Method.RightLeftX && transform.localPosition.x > CloseLimit)
            transform.Translate(Vector3.left * (Time.deltaTime * 4));
    }

    public void OpenDoor()
    {
        if (!isOpen)
            GetComponent<AudioSource>().Play();

        isOpen = true;
    }

    public void CloseDoor()
    {
        if(isOpen)
            GetComponent<AudioSource>().Play();

        isOpen = false;
    }
}
