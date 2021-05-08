using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformationMenu : MonoBehaviour
{
    private Vector3 centerOfTheScreen = new Vector2(551,307);

    private Transformation.TypeOfTransformation _selectedType;

    [SerializeField] private Image _defaultImage;
    [SerializeField] private Image _rubberImage;
    [SerializeField] private Image _coppertImage;
    [SerializeField] private Image _iceImage;
    [SerializeField] private Image _glueImage;
    [SerializeField] private Image _paperImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        /* calcolo la differenza tra la posizione del mouse e il centro dello schermo */
        Vector2 mouseDelta = Input.mousePosition - centerOfTheScreen;
        /* calcolo la direzione utilizzando la normale della variazione del movimento del mouse*/
      
        float angle = (Mathf.Atan2(mouseDelta.x, mouseDelta.y) / Mathf.PI) * 180f;
        if (angle < 0)
            angle += 360;
        
        if(angle>=0 && angle < 60)
        {
            _selectedType = Transformation.TypeOfTransformation.Gomma;
            ResetColor();
            _rubberImage.color = new Color32(191, 195, 126, 255);
        }
        else if (angle >= 60 && angle < 120)
        {
            _selectedType = Transformation.TypeOfTransformation.Rame;
            ResetColor();
            _coppertImage.color = new Color32(191, 195, 126, 255);
        }
        else if (angle >= 120 && angle < 180)
        {
            _selectedType = Transformation.TypeOfTransformation.Ghiaccio;
            ResetColor();
            _iceImage.color = new Color32(191, 195, 126, 255);
        }
        else if (angle >= 180 && angle < 240)
        {
            _selectedType = Transformation.TypeOfTransformation.Colla;
            ResetColor();
            _glueImage.color = new Color32(191, 195, 126, 255);
        }
        else if (angle >= 240 && angle < 300)
        {
            _selectedType = Transformation.TypeOfTransformation.Carta;
            ResetColor();
            _paperImage.color = new Color32(191, 195, 126, 255);
        }
        else
        {
            _selectedType = Transformation.TypeOfTransformation.Default;
            ResetColor();
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
        _rubberImage.color = new Color32(255,255,255,255);
        _coppertImage.color = new Color32(255,255,255,255);
        _iceImage.color = new Color32(255,255,255,255);
        _glueImage.color = new Color32(255,255,255,255);
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
        //rallenta il gioco
    }

    public void Close()
    {
        /*
         * Chiudo il menu di scelta delle trasformazioni
         * 
         */
        
        gameObject.SetActive(false);
        GameEvent.isChoosingTransformation = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //torna alla velocita di gioco normale
    }
}
