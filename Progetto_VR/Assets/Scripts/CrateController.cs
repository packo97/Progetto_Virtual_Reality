using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CrateController : MonoBehaviour
{
    public enum Contenuto {Vuota=0, Vita, Codice};

    public Contenuto contenuto;
    
    [SerializeField] private string codice;
    
    public bool isOpen;

    [SerializeField] private Transform maniglia;

    [SerializeField] private GameObject life;

    [SerializeField] private GameObject paper;
    
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        if (contenuto == Contenuto.Vita)
            life.SetActive(true);
        else if (contenuto == Contenuto.Codice)
            paper.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCrate()
    {
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


        StartCoroutine(OpeningAnimation());
        
        Messenger<Contenuto,string>.Broadcast(GameEvent.OPEN_CRATE, contenuto, messaggio);
    }

    private IEnumerator OpeningAnimation()
    {
        for (int i = 0; i < 70; i++)
        {
            maniglia.Rotate(-1,0,0);
            yield return new WaitForSeconds(0.01f);
        }
        if (contenuto == Contenuto.Vita)
            life.SetActive(false);
        else if (contenuto == Contenuto.Codice)
            paper.SetActive(false);
    }

    public void CloseCrate()
    {
        isOpen = false;
        if (contenuto == Contenuto.Vita)
            contenuto = Contenuto.Vuota;
        StartCoroutine(CloseAnimation());
        Messenger.Broadcast(GameEvent.CLOSE_CRATE);
    }
    
    private IEnumerator CloseAnimation()
    {
        for (int i = 0; i < 70; i++)
        {
            maniglia.Rotate(+1,0,0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
