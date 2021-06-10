using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePlayerController : MonoBehaviour
{
    public enum PlayerActive {Janus = 0, Friend};
    public static PlayerActive playerActive;

    [SerializeField] private GameObject playerJanus;
    [SerializeField] private GameObject playerFriend;
    [SerializeField] private OrbitCameraRB camera;
    
    private static bool doublePlayerMode;
    
    // Start is called before the first frame update
    void Start()
    {
        doublePlayerMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchPlayer();
        }
        
        if (playerActive == PlayerActive.Janus)
        {
            playerFriend.GetComponent<MovementInput>().enabled = false;
            playerJanus.GetComponent<PlayerController>().enabled = true;
            camera.target = playerJanus.transform.Find("target");
            camera.GetComponent<Camera>().cullingMask = ~(1 << LayerMask.NameToLayer("Friend Layer")); 
        }
        else if (playerActive == PlayerActive.Friend && doublePlayerMode)
        {
            playerJanus.GetComponent<PlayerController>().enabled = false;
            playerFriend.GetComponent<MovementInput>().enabled = true;
            camera.target = playerFriend.transform.Find("target");
            camera.GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer("Friend Layer");
            
        }
    }

    private void SwitchPlayer()
    {
        playerActive = (PlayerActive)(((int) playerActive + 1) % 2);
    }
    
    
    public static void SetDoublePlayerMode(bool value)
    {
        doublePlayerMode = value;
    }
}
