using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public const string PLAYER_DIE = "PLAYER_DIE";
    
    public const string ON_STICK_TIME = "ON_STICK_TIME";
    public const string OFF_STICK_TIME = "OFF_STICK_TIME";
    public const string DECREASE_STICK_TIME = "DECREASE_STICK_TIME";

    public const string OPEN_MENU_TRANSFORMATION = "OPEN_MENU_TRANSFORMATION";
    public const string CLOSE_MENU_TRANSFORMATION = "CLOSE_MENU_TRANSFORMATION";

    public const string SELECTED_TRANSFORMATION = "SELECTED_TRANSFORMATION";
        
    public static bool isPaused = false;
    public static bool isChoosingTransformation = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
