using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        transform.Translate(0, 5, 0);
    }

    public void CloseDoor()
    {
        transform.Translate(0, -5, 0);
    }
}
