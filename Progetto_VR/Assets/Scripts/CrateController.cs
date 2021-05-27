using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CrateController : MonoBehaviour
{
    public enum Contenuto {Vuota=0, Vita, Codice, ChipGomma, ChipRame, ChipGhiaccio, ChipColla, ChipCarta};

    public Contenuto contenuto;
    
    [SerializeField] private string codice;
    
    public bool isOpen;

    [SerializeField] private Transform maniglia;

    [SerializeField] private GameObject life;

    [SerializeField] private GameObject paper;

    [SerializeField] private GameObject chip;
    
    // Start is called before the first frame update
    void Start()
    {
        /*
         * A seconda del contenuto della casa, rendo visibile il corrispondente oggetto.
         * 
         */
        
        
        isOpen = false;
        if (contenuto == Contenuto.Vita)
            life.SetActive(true);
        else if (contenuto == Contenuto.Codice)
            paper.SetActive(true);
        else if (contenuto == Contenuto.ChipGomma || contenuto == Contenuto.ChipRame || contenuto == Contenuto.ChipGhiaccio || contenuto == Contenuto.ChipColla || contenuto == Contenuto.ChipCarta)
            chip.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCrate()
    {
        /*
         * Quando viene richiesto di aprire la cassa imposto il messaggio da mandare all'user interface.
         * Faccio partire l'animazione di apertura.
         * Se il contenuto è una trasformazione, notifico il menu delle trasformazioni.
         * 
         */
        
        isOpen = true;
        string messaggio = null;
        if (contenuto == Contenuto.Codice)
            messaggio = codice;
        else if (contenuto == Contenuto.Vita)
        {
            messaggio = "vita";
        }
        else if (contenuto == Contenuto.Vuota)
        {
            messaggio = "vuota";
        }
        else if (contenuto == Contenuto.ChipGomma)
        {
            messaggio = "gomma";
        }
        else if (contenuto == Contenuto.ChipRame)
        {
            messaggio = "rame";
        }
        else if (contenuto == Contenuto.ChipGhiaccio)
        {
            messaggio = "ghiaccio";
        }
        else if (contenuto == Contenuto.ChipColla)
        {
            messaggio = "colla";
        }
        else if (contenuto == Contenuto.ChipCarta)
        {
            messaggio = "carta";
        }
        
        StartCoroutine(OpeningAnimation());
        
        Messenger<Contenuto,string>.Broadcast(GameEvent.OPEN_CRATE, contenuto, messaggio);
        if (contenuto == Contenuto.ChipGomma || contenuto == Contenuto.ChipRame || contenuto == Contenuto.ChipGhiaccio || contenuto == Contenuto.ChipColla || contenuto == Contenuto.ChipCarta)
            Messenger<string>.Broadcast(GameEvent.ENABLE_TRANSFORMATION, messaggio);
    }

    private IEnumerator OpeningAnimation()
    {
        /*
         * Animazione apertura ruotando il coperchio della cassa con un pivot rappresentato dalla maniglia.
         * 
         */
        
        for (int i = 0; i < 70; i++)
        {
            maniglia.Rotate(-1,0,0);
            yield return new WaitForSeconds(0.01f);
        }
        if (contenuto == Contenuto.Vita)
            life.SetActive(false);
        else if (contenuto == Contenuto.ChipGomma || contenuto == Contenuto.ChipRame || contenuto == Contenuto.ChipGhiaccio || contenuto == Contenuto.ChipColla || contenuto == Contenuto.ChipCarta)
            chip.SetActive(false);

    }

    public void CloseCrate()
    {
        /*
         * Quando viene richiesto di chiudere la cassa, se non è un codice, elimino il contenuto.
         * Avvio l'animazione di chiusura.
         * Notifico alla user interface la chiusura dell'interfaccia.
         * 
         */
        
        isOpen = false;
        if (contenuto == Contenuto.Vita || contenuto == Contenuto.ChipGomma || contenuto == Contenuto.ChipRame || contenuto == Contenuto.ChipGhiaccio || contenuto == Contenuto.ChipColla || contenuto == Contenuto.ChipCarta)
            contenuto = Contenuto.Vuota;
        StartCoroutine(CloseAnimation());
        Messenger.Broadcast(GameEvent.CLOSE_CRATE);
    }
    
    private IEnumerator CloseAnimation()
    {
        /*
        * Animazione chiusura ruotando il coperchio della cassa con un pivot rappresentato dalla maniglia.
        * 
        */
        
        for (int i = 0; i < 70; i++)
        {
            maniglia.Rotate(+1,0,0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
