using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ChatPanel : MonoBehaviour
{
    [SerializeField] private Text testo;
    [SerializeField] private Image image;
    [SerializeField] private Sprite janusSprite;
    [SerializeField] private Sprite friendSprite;
    
    private int indexSentence;

    private List<(string, string)> dialogue;
    // Start is called before the first frame update
    void Start()
    {
        indexSentence = 0;
        string path = "Assets/Resources/Dialogues/dialogue_janus_friend.txt";
        string[] lines = File.ReadAllLines(path);
        
        dialogue = new List<(string, string)>();
        foreach (string line in lines)
        {
            //Debug.Log(line);
            string[] split = line.Split('\t');
            (string, string) whoTalk_sentence = (split[0], split[1]);
            //Debug.Log(whoTalk_sentence.Item1 + ": " + whoTalk_sentence.Item2);
            dialogue.Add(whoTalk_sentence);
        }
        
        
            
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

    public void NextSentence()
    {
        if (dialogue[indexSentence].Item2.Equals("End"))
        {
            gameObject.SetActive(false);
            DoublePlayerController.SetDoublePlayerMode(true);
        }
        else
        {
            if (dialogue[indexSentence].Item1.Equals("Janus"))
                image.sprite = janusSprite;
            else if (dialogue[indexSentence].Item1.Equals("Friend"))
                image.sprite = friendSprite;

            image.preserveAspect = true;
            
            testo.text = dialogue[indexSentence].Item2;
            
            if (indexSentence < dialogue.Count - 1)
                indexSentence++;
        }
        
    }
    
}
