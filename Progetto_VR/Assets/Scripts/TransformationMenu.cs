using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformationMenu : MonoBehaviour
{
    private Vector3 centerOfTheScreen = new Vector3(551,307,0);

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
        Vector3 mouseDelta = Input.mousePosition - centerOfTheScreen;
        /* calcolo la direzione utilizzando la normale della variazione del movimento del mouse*/
        Vector3 direction = mouseDelta.normalized;
        
        /*
         * uso Vector3.Dot per capire la direzione del movimento
         * prendo memoria della trasformazione attualmente selezionata
         * imposto il colore per far capire di quale tipo di trasformazione ho preso memoria
         * 
         */
        float dot = Vector3.Dot(direction, Vector3.up);
        if (dot > 0.5) {
            //UP
            float dot2 = Vector3.Dot(direction, Vector3.right);
            if (dot2 > 0.5)
            {
                // UP RIGHT
                _selectedType = Transformation.TypeOfTransformation.Gomma;
                ResetColor();
                _rubberImage.color = new Color32(191, 195, 126,255);
            }
            else if (dot2 < 0.5)
            {
                // UP LEFT
                _selectedType = Transformation.TypeOfTransformation.Default;
                ResetColor();
                _defaultImage.color = new Color32(191, 195, 126,255);
            }
        }
        else if (dot < -0.5) { //can be <= for sideways
            //DOWN
            float dot2 = Vector3.Dot(direction, Vector3.right);
            if (dot2 > 0.5)
            {
                //DOWN RIGHT
                _selectedType = Transformation.TypeOfTransformation.Ghiaccio;
                ResetColor();
                _iceImage.color = new Color32(191, 195, 126, 255);
            }
            else if (dot2 < 0.5)
            {
                //DOWN LEFT
                _selectedType = Transformation.TypeOfTransformation.Colla;
                ResetColor();
                _glueImage.color = new Color32(191, 195, 126, 255);
            }
                
        }
        else {
            dot = Vector3.Dot(direction, Vector3.right);
            if (dot > 0.5) { //can be >= for sideways
                //RIGHT
                _selectedType = Transformation.TypeOfTransformation.Rame;
                ResetColor();
                _coppertImage.color = new Color32(191, 195, 126, 255);
            }
            else if (dot < -0.5) { //can be <= for sideways
                //LEFT
                _selectedType = Transformation.TypeOfTransformation.Carta;
                ResetColor();
                _paperImage.color = new Color32(191, 195, 126, 255);
            }
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
