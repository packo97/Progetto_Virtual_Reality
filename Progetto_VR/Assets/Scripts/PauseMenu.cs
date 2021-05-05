using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private SettingsMenu _settingsMenu;
    // Start is called before the first frame update
    void Start()
    {
        _settingsMenu.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Open()
    {
        gameObject.SetActive(true);
        PauseGame();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        UnPauseGame();
    }
    
    public void PauseGame()
    {
        GameEvent.isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void OpenSettings()
    {
        Close();
        _settingsMenu.Open();
    }

    public void CloseSettings()
    {
        Open();
        _settingsMenu.Close();
    }

    public void UnPauseGame()
    {
        GameEvent.isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        /*non funzione nell'editor unity*/
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
