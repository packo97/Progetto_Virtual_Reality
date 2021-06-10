using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private SettingsMenu _settingsMenu;
    [SerializeField] private GameObject _imageLivesContenitor;
    [SerializeField] private Slider _stickTimeSlider;
    [SerializeField] private TransformationMenu _transformationMenu;
    [SerializeField] private ChatPanel _chatPanel;
    [SerializeField] private GameObject _gameOver;
    
    private int lives;
    
    void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_DIE, OnPlayerDie);
        Messenger.AddListener(GameEvent.GAME_OVER, OpenGameOverPanel);
        
        Messenger<float>.AddListener(GameEvent.ON_STICK_TIME, OnStickTime);
        Messenger.AddListener(GameEvent.OFF_STICK_TIME, OffStickTime);
        Messenger.AddListener(GameEvent.DECREASE_STICK_TIME, DecreaseStickTime);
        Messenger.AddListener(GameEvent.OPEN_MENU_TRANSFORMATION, OpenMenuTransformation);
        Messenger.AddListener(GameEvent.CLOSE_MENU_TRANSFORMATION, CloseMenuTransformation);
        
        Messenger<CrateController.Contenuto,string>.AddListener(GameEvent.OPEN_CRATE, OpenChatPanel);
        Messenger.AddListener(GameEvent.CLOSE_CRATE, CloseChatPanel);
        
        Messenger.AddListener(GameEvent.NEXT_SENTENCE, NextSentence);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_DIE, OnPlayerDie);
        Messenger.RemoveListener(GameEvent.GAME_OVER, OpenGameOverPanel);
        
        Messenger<float>.RemoveListener(GameEvent.ON_STICK_TIME, OnStickTime);
        Messenger.RemoveListener(GameEvent.OFF_STICK_TIME, OffStickTime);
        Messenger.RemoveListener(GameEvent.DECREASE_STICK_TIME, DecreaseStickTime);
        Messenger.RemoveListener(GameEvent.OPEN_MENU_TRANSFORMATION, OpenMenuTransformation);
        Messenger.RemoveListener(GameEvent.CLOSE_MENU_TRANSFORMATION, CloseMenuTransformation);
        
        Messenger<CrateController.Contenuto,string>.RemoveListener(GameEvent.OPEN_CRATE, OpenChatPanel);
        Messenger.RemoveListener(GameEvent.CLOSE_CRATE, CloseChatPanel);

        
        Messenger.RemoveListener(GameEvent.NEXT_SENTENCE, NextSentence);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _pauseMenu.Close();
        _settingsMenu.Close();
        _stickTimeSlider.gameObject.SetActive(false);
        _transformationMenu.Close();
        _chatPanel.Close();
        _gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Apri il menu se il gioco non è in pausa.
         * Chiudi il menu se il gioco è in pausa.
         * 
         */
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameEvent.isPaused)
                _pauseMenu.Open();
            else if (GameEvent.isPaused)
                _pauseMenu.Close();
        }
    }

    private void OnPlayerDie()
    { 
        /*
         * Prendo i figli del gameobject che contiene le immagini delle vite del player.
         * Prendo l'indice dell'ultima posizione dell'array dei figli.
         * Disattivo l'ultimo figlio.
         * 
         */
        Image[] livesImages = _imageLivesContenitor.GetComponentsInChildren<Image>() ;
        int lastIndex = livesImages.Length - 1;
        livesImages[lastIndex].gameObject.SetActive(false);
    }

    private void OnStickTime(float maxValue)
    {
        /*
         * Attivo lo slider dello stick time.
         * Imposto il valore dello slider a 80.
         * 
         */
        _stickTimeSlider.gameObject.SetActive(true);
        _stickTimeSlider.maxValue = maxValue;
        _stickTimeSlider.value = maxValue;
    }

    private void OffStickTime()
    {
        /*
         * Disattivo lo slider per lo stick time.
         * 
         */
        _stickTimeSlider.gameObject.SetActive(false);
    }

    private void DecreaseStickTime()
    {
        /*
         * Diminuisco il valore dello slider di 1.
         * 
         */
        _stickTimeSlider.value -= 1;
    }

    private void OpenMenuTransformation()
    {
        /*
         * Apro il menu delle trasformazioni
         * 
         */
        _transformationMenu.Open();
    }

    private void CloseMenuTransformation()
    {
        /*
         * Chiudo il menu delle trasformazioni
         * 
         */
        _transformationMenu.Close();
    }

    private void OpenChatPanel(CrateController.Contenuto contenuto ,string testo)
    {
        /*
         * A
         */
        
        _chatPanel.Open();
        _chatPanel.ChangeText(testo);

        if (contenuto == CrateController.Contenuto.Vita)
        {
            Image[] livesImages = _imageLivesContenitor.GetComponentsInChildren<Image>(true) ;
            int firstIndex = 0;
            
            for (int i=0; i < livesImages.Length; i++)
            {
                if (_imageLivesContenitor.transform.GetChild(i).gameObject.activeSelf == false)
                {
                    livesImages[i].gameObject.SetActive(true);
                    Messenger.Broadcast(GameEvent.LIFE_UP);
                    break;
                }
            }
            
        }
    }

    private void CloseChatPanel()
    {
        _chatPanel.Close();
        _chatPanel.ChangeText("");
    }

    private void OpenGameOverPanel()
    {
        _gameOver.SetActive(true);
    }

    private void NextSentence()
    {
        _chatPanel.Open();
        _chatPanel.NextSentence();
    }
}
