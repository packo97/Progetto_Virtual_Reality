using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public const string PLAYER_DIE = "PLAYER_DIE";
    public const string GAME_OVER = "GAME_OVER";
    
    public const string ON_STICK_TIME = "ON_STICK_TIME";
    public const string OFF_STICK_TIME = "OFF_STICK_TIME";
    public const string DECREASE_STICK_TIME = "DECREASE_STICK_TIME";

    public const string OPEN_MENU_TRANSFORMATION = "OPEN_MENU_TRANSFORMATION";
    public const string CLOSE_MENU_TRANSFORMATION = "CLOSE_MENU_TRANSFORMATION";
    public const string SELECTED_TRANSFORMATION = "SELECTED_TRANSFORMATION";
    public const string ENABLE_TRANSFORMATION = "ENABLE_TRANSFORMATION";
    

    public const string OPEN_CRATE = "OPEN_CRATE";
    public const string CLOSE_CRATE = "CLOSE_CRATE";

    public const string LIFE_UP = "LIFE_UP";

    public const string NEXT_SENTENCE = "NEXT_SENTENCE";
    
    public static bool isPaused = false;
    public static bool isChoosingTransformation = false;
    
}
