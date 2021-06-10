using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideInput : MonoBehaviour
{
    [SerializeField] private bool isOpen = false;
    [System.Serializable] public enum Method {UpDownY=0, RightLeftZ, ForwardBackwardX};
    
    //i limiti sono le posizioni locali di y o x 
    [SerializeField] private float OpenLimit;
    [SerializeField] private float CloseLimit;
    public Method mod;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Up and Down
        if(isOpen == true && mod == Method.UpDownY && transform.localPosition.y < OpenLimit){
            transform.Translate(Vector3.up * (Time.deltaTime * 4));
            Debug.Log(transform.localPosition.y);
        }
        else if(isOpen == false && mod == Method.UpDownY && transform.localPosition.y > CloseLimit)
            transform.Translate(Vector3.down * (Time.deltaTime * 4));
        //right and left
        else if (isOpen == true && mod == Method.RightLeftZ && transform.localPosition.z > OpenLimit)
            transform.Translate(Vector3.right * (Time.deltaTime * 4));
        else if (isOpen == false && mod == Method.RightLeftZ && transform.localPosition.z < CloseLimit)
            transform.Translate(Vector3.left * (Time.deltaTime * 4));
        //Forward and Backward
        else if (isOpen == true && mod == Method.ForwardBackwardX && transform.localPosition.x < OpenLimit)
            transform.Translate(Vector3.forward * (Time.deltaTime * 4));
        else if (isOpen == false && mod == Method.ForwardBackwardX && transform.localPosition.x > CloseLimit)
            transform.Translate(Vector3.back * (Time.deltaTime * 4));

    }

    public void OpenDoor()
    {
        isOpen = true;
    }

    public void CloseDoor()
    {
        isOpen = false;
    }
}
