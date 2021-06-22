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
        /*
         * Verifico quante pedane sono attive.
         * Se sono tutte attive -> apro la porta
         * altrimenti -> chiudo la porta
         * 
         */
        
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
        /*
         * Reset di tutte le piattaforme
         * 
         */
        
        foreach (PlatformPressable plat in platforms)
        {
            plat.Reset();

        }
    }
}
