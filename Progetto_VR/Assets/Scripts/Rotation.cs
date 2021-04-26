using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] int rangeRotation;
    [System.Serializable] public enum Method {clockwise=0, counterClockwise};
    
  
    
    public Method mod;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ObjectRotation()
    {
        if (mod == Method.clockwise)
        {
            for (int i = 1; i <= rangeRotation; i++)
            {
                transform.Rotate(0, 1, 0);
                yield return null;
            }
                
        }
        else if (mod == Method.counterClockwise)
        {
            for (int i = 1; i <= rangeRotation; i++)
            {
                transform.Rotate(0, -1, 0);
                yield return null;
            }
        }
    }
    
   
}
