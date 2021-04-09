using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private int _lives;
    private Vector3 checkpointPosition;
    // Start is called before the first frame update
    void Start()
    {
        checkpointPosition = GetComponent<Transform>().position; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hurt()
    {
        _lives-=1;
        //
        Debug.Log(_lives);
        Debug.Log(checkpointPosition);
        transform.position = new Vector3(checkpointPosition.x, checkpointPosition.y, checkpointPosition.z);
    }
}
