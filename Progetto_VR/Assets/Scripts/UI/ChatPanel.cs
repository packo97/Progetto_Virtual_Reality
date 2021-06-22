
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChatPanel : MonoBehaviour
{
    [SerializeField] private Text testo;
    [SerializeField] private Image image;
    [SerializeField] private Sprite janusSprite;
    [SerializeField] private Sprite friendSprite;
    [SerializeField] private Sprite scientist1Sprite;
    [SerializeField] private Sprite scientist2Sprite;
    [SerializeField] private Sprite scientist3Sprite;

    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
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
        image.sprite = janusSprite;
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

    public void NextSentence((string, string) sentence)
    {

        if (sentence.Item2.Equals("End"))
        {
            gameObject.SetActive(false);
            DoublePlayerController.SetDoublePlayerMode(true);
        }
        else
        {
            if (sentence.Item1.Equals("Janus"))
                image.sprite = janusSprite;
            else if (sentence.Item1.Equals("Friend"))
                image.sprite = friendSprite;
            else if (sentence.Item1.Equals("Scientist1"))
                image.sprite = scientist1Sprite;
            else if (sentence.Item1.Equals("Scientist2"))
                image.sprite = scientist2Sprite;
            else if (sentence.Item1.Equals("Scientist3"))
                image.sprite = scientist3Sprite;
       
                

            image.preserveAspect = true;

            testo.text = sentence.Item2;
        }
        
        audio.Play();
    }

}
