using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    public void Exit()
    {
        /*non funzione nell'editor unity*/
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
    
}
