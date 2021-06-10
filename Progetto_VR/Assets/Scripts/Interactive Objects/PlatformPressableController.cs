using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class PlatformPressableController : MonoBehaviour
{
    private PlatformPressable[] platforms;
    
    [SerializeField] private DoorInput door;
    // Start is called before the first frame update
    void Start()
    {
        platforms = GetComponentsInChildren<PlatformPressable>();
    }

    // Update is called once per frame
    void Update()
    {
        int countPlat = 0;
        foreach (PlatformPressable plat in platforms)
        {
            if (plat.active)
                countPlat++;
            
        }
        
        if (countPlat == platforms.Length)
            door.OpenDoor();
        else
        {
            door.CloseDoor();
        }
    }

    public void ResetAll()
    {
        foreach (PlatformPressable plat in platforms)
        {
            plat.Reset();

        }
    }
}
