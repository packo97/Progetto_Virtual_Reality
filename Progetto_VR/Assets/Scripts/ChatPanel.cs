using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatPanel : MonoBehaviour
{
    [SerializeField] private Text testo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
    
    public void ChangeText(string text)
    {
        if (text.Equals("vuota"))
            testo.text = "Peccato! La cassa è vuota...";
        else if (text.Equals("vita"))
            testo.text = "Che fortuna! E' stato trovato l'oggetto " + text;
        else
            testo.text = "Interessante... Un codice... " + text;

    }
}
