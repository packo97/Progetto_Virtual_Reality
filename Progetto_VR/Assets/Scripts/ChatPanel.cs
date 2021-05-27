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
        /*
         * A seconda dell'input testuale che arriva, modifico il testo del pannello.
         * 
         */
        
        if (text.Equals("vuota"))
            testo.text = "Peccato! La cassa è vuota...";
        else if (text.Equals("vita"))
            testo.text = "Che fortuna! E' stato trovato l'oggetto " + text;
        else if (text.Equals("gomma"))
            testo.text = "Ho sempre sognato di poter rimbalzare!";
        else if (text.Equals("rame"))
            testo.text = "Mio padre me lo diceva sempre.. che mondo sarebbe senza elettricità?";
        else if (text.Equals("ghiaccio"))
            testo.text = "Brrr che freddoooo";
        else if (text.Equals("colla"))
            testo.text = "E ora? Un chip per appicicarmi? Che noia...";
        else if (text.Equals("carta"))
            testo.text = "Un chip per diventare di carta? Potrei utilizzarlo per volare...";
        else
            testo.text = "Interessante... Un codice... " + text;

    }
}
