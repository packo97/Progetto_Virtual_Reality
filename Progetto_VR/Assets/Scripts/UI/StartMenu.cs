
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private SettingsMenu _settingsMenu;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _settingsMenu.Close();
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
    
    public void OpenSettings()
    {
        gameObject.SetActive(false);
        _settingsMenu.Open();
    }

    public void CloseSettings()
    {
        gameObject.SetActive(true);
        _settingsMenu.Close();
    }
    
}
