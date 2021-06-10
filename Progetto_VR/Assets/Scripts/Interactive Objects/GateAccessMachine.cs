using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GateAccessMachine : MonoBehaviour
{
    public bool isActive;
    [SerializeField] private Text codice;
    [SerializeField] private Text info;
    [SerializeField] private string correctKey;
    [SerializeField] private DoorInput door;
    private int cifreInserite;

    private MeshRenderer _meshRenderer;

    [SerializeField] private Material correctMaterial;
    [SerializeField] private Material wrongMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material closedMaterial;
    
    
    
    [SerializeField] private AudioClip wrongCode;
    [SerializeField] private AudioClip correctCode;
    
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        codice.text = "____";
        cifreInserite = 0;
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AccessMachine()
    {
        /*
         * Se la la macchina non è accessa, viene accessa e viceversa.
         * In accordo a questo modificio il contenuto visualizzato (testo e colore).
         * 
         */
        
        isActive = !isActive;
        if (isActive)
        {
            info.text = "INSERT CODE";
            info.color = Color.yellow;
            codice.color = Color.yellow;
            ChangeMaterial(defaultMaterial);
        }
        else
        {
            info.text = "PRESS E";
            info.color = Color.gray;
            codice.color = Color.gray;
            codice.text = "____";
            ChangeMaterial(closedMaterial);
        }
            
    }

    private void ChangeMaterial(Material material)
    {
        /*
         * Cambia il colore della macchina.
         * 
         */
        
        Material[] materials = _meshRenderer.materials;
        materials[0] = material;
        _meshRenderer.materials = materials;
    }
    
    public void InsertCode(char c)
    {
        /*
         * Inserisce un numero nella macchina e fa partire il controllo alla fine dell'inserimento.
         * 
         */
        
        int indice_primo_trattino = codice.text.IndexOf("_");
        StringBuilder codiceBuilder = new StringBuilder(codice.text);
        codiceBuilder[indice_primo_trattino] = c;
        codice.text = codiceBuilder.ToString();
        CheckCode();
    }

    private void CheckCode()
    {
        /*
         * Se la macchina ha il codice corretto, apre la porta.
         * In accordo modifica il contenuto visualizzato (testo e colore).
         * 
         */
        
        cifreInserite = 0;
        foreach (char c in codice.text)
        {
            if (c != '_')
                cifreInserite++;
        }
        
        if (cifreInserite == 4)
        {
            if (correctKey.Equals(codice.text))
            {
                door.OpenDoor();
                info.text = "CORRECT";
                info.color = Color.green;
                codice.color = Color.green;
                codice.color = Color.green;
                ChangeMaterial(correctMaterial);
                
                GetComponent<AudioSource>().PlayOneShot(correctCode);
            }
            else
            {
                codice.text = "____";
                info.text = "ERROR";
                info.color = Color.red;
                codice.color = Color.red;
                ChangeMaterial(wrongMaterial);
                
                GetComponent<AudioSource>().PlayOneShot(wrongCode);
            }
        }
    }
    
}
