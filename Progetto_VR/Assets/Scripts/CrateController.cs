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
    
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
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

        Messenger<Contenuto,string>.Broadcast(GameEvent.OPEN_CRATE, contenuto, messaggio);
    }

    public void CloseCrate()
    {
        isOpen = false;
        if (contenuto == Contenuto.Vita)
            contenuto = Contenuto.Vuota;
        Messenger.Broadcast(GameEvent.CLOSE_CRATE);
    }
}
