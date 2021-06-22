using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformationMenu : MonoBehaviour
{
    private Vector3 centerOfTheScreen;

    private Transformation.TypeOfTransformation _selectedType;

    [SerializeField] private Image _defaultImage;
    [SerializeField] private Image _rubberImage;
    [SerializeField] private Image _copperImage;
    [SerializeField] private Image _iceImage;
    [SerializeField] private Image _glueImage;
    [SerializeField] private Image _paperImage;

    private bool _rubber_available;
    private bool _copper_available;
    private bool _ice_available;
    private bool _glue_available;
    private bool _paper_available;
    
    
    private void Awake()
    {
        Messenger<string>.AddListener(GameEvent.ENABLE_TRANSFORMATION, EnableTransformation);
        
        centerOfTheScreen = new Vector2(Screen.width / 2, Screen.height / 2);

        _rubber_available = false;
        _copper_available = false;
        _ice_available = false;
        _glue_available = false;
        _paper_available = false;
        
        _rubberImage.color = new Color32(255,255,255,150);
        _copperImage.color = new Color32(255,255,255,150);
        _iceImage.color = new Color32(255,255,255,150);
        _glueImage.color = new Color32(255,255,255,150);
        _paperImage.color = new Color32(255,255,255,150);
    }
    
    private void OnDestroy()
    {
        Messenger<string>.RemoveListener(GameEvent.ENABLE_TRANSFORMATION, EnableTransformation);
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * In base alla posizione del mouse seleziono la trasformazione da applicare se disponibile
         *  
         */
        
        
        /* calcolo la differenza tra la posizione del mouse e il centro dello schermo */
        Vector2 mouseDelta = Input.mousePosition - centerOfTheScreen;
        /* calcolo la direzione utilizzando la normale della variazione del movimento del mouse*/
      
        float angle = (Mathf.Atan2(mouseDelta.x, mouseDelta.y) / Mathf.PI) * 180f;
        if (angle < 0)
            angle += 360;
        
        ResetColor();
        if(angle>=0 && angle < 60)
        {
            if (_rubber_available)
            {
                _selectedType = Transformation.TypeOfTransformation.Gomma;    
                _rubberImage.color = new Color32(191, 195, 126, 255);
            }
        }
        else if (angle >= 60 && angle < 120)
        {
            if (_copper_available)
            {
                _selectedType = Transformation.TypeOfTransformation.Rame;    
                _copperImage.color = new Color32(191, 195, 126, 255);
            }
            
        }
        else if (angle >= 120 && angle < 180)
        {
            if (_ice_available)
            {
                _selectedType = Transformation.TypeOfTransformation.Ghiaccio;
                _iceImage.color = new Color32(191, 195, 126, 255);
            }
        }
        else if (angle >= 180 && angle < 240)
        {
            if (_glue_available)
            {
                _selectedType = Transformation.TypeOfTransformation.Colla;
                _glueImage.color = new Color32(191, 195, 126, 255);
            }
        }
        else if (angle >= 240 && angle < 300)
        {
            if (_paper_available)
            {
                _selectedType = Transformation.TypeOfTransformation.Carta;
                _paperImage.color = new Color32(191, 195, 126, 255);
            }
        }
        else
        {
            _selectedType = Transformation.TypeOfTransformation.Default;
            _defaultImage.color = new Color32(191, 195, 126, 255);
        }

      

        if (Input.GetMouseButtonUp(1))
        {
            /*
             * Quando il pulsante destro del mouse viene rilasciato, notifichi al gioco la trasformazione che deve
             * essere applicata e chiude il menu di scelta della trasformazione.
             */
            Messenger<Transformation.TypeOfTransformation>.Broadcast(GameEvent.SELECTED_TRANSFORMATION, _selectedType);
            Close();
        }
            
    }

    private void ResetColor()
    {
        /*
         * Reset dei colori delle immagini delle trasformazioni.
         * 
         */
        _defaultImage.color = new Color32(255,255,255,255);
        if (_rubber_available)
            _rubberImage.color = new Color32(255,255,255,255);
        if (_copper_available)
            _copperImage.color = new Color32(255,255,255,255);
        if (_ice_available)
            _iceImage.color = new Color32(255,255,255,255);
        if (_glue_available)
            _glueImage.color = new Color32(255,255,255,255);
        if (_paper_available)
            _paperImage.color = new Color32(255,255,255,255);
    }
    
    public void Open()
    {
        /*
         * Apro il menu di scelta delle trasformazioni
         */
        
        gameObject.SetActive(true);
        GameEvent.isChoosingTransformation = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Close()
    {
        /*
         * Chiudo il menu di scelta delle trasformazioni
         * 
         */
        //Debug.Log("chiudo");
        gameObject.SetActive(false);
        GameEvent.isChoosingTransformation = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //torna alla velocita di gioco normale
    }

    private void EnableTransformation(string type_of_transformation)
    {
        /*
         * Abilito una determinata trasformazione
         * 
         */
        if (type_of_transformation == "gomma")
        {
            _rubber_available = true;
            _rubberImage.color = new Color32(255,255,255,255);
        }
        else if (type_of_transformation == "rame")
        {
            _copper_available = true;
            _copperImage.color = new Color32(255,255,255,255);
        }
        else if (type_of_transformation == "ghiaccio")
        {
            _ice_available = true;
            _iceImage.color = new Color32(255,255,255,255);
        }
        else if (type_of_transformation == "colla")
        {
            _glue_available = true;
            _glueImage.color = new Color32(255,255,255,255);
        }
        else if (type_of_transformation == "carta")
        {
            _paper_available = true;
            _paperImage.color = new Color32(255,255,255,255);
        }
    }
}
