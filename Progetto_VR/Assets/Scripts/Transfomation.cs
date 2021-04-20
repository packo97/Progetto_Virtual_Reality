using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transfomation : MonoBehaviour
{
    //Tipi di materiale per le trasformaz<ioni
    public enum Tranformation {Default=0 , Rame, Carta, Ghiaccio, Gomma, Colla};
    public Tranformation transf;
    [SerializeField] private Material Rame;
    [SerializeField] private Material Carta;
    [SerializeField] private Material Ghiaccio;
    [SerializeField] private Material Gomma;
    [SerializeField] private Material Colla;
    private SkinnedMeshRenderer[] list;
    private Material[] defaultMAT;

    //particellare per la trasformazione
    private ParticleSystem particelle;
    private Color defColor;

    //variabili per simulare il rimbalzo della gomma
    float jumpPoint, collisionPoint, distance;
    bool isGrounded=true;
    
    //tasto per la trasformazione
    public KeyCode transformInput=KeyCode.Tab;

    private ElectricBehavior _electricBehavior;
    
    Rigidbody body;
    void Start()
    {
        transf = Tranformation.Default;
        body = GetComponent<Rigidbody>();
        list = GetComponentsInChildren<SkinnedMeshRenderer>(true);
        defaultMAT = list[0].materials;
        particelle = GetComponentInChildren<ParticleSystem>();
        defColor = particelle.startColor;

        _electricBehavior = GetComponentInChildren<ElectricBehavior>(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(transformInput))
        {
            if(transf == Tranformation.Colla)
            {
                transf = 0;
                Debug.Log(transf);
            }
            else
            {
                
                transf++;
                Debug.Log(transf);
            }
            ChangeMaterial();
            particelle.Play();
            _electricBehavior.Reset();
        }
        

    }


    
    void OnCollisionEnter(Collision collision)
    {
        /*
         Se la trasformazione attiva � di gomma e il player non � attaccato al terreno calcolo la 
        distanza tra il punto di stacco e di collisione che ne determina la forza con cui 
        il player rimbalza (sopra un certo limite la forza di rimbalzo viene bloccata ad 
        un determinato gap). finche la distanza di rimbalzo non sar� trascurabile il player continuar� 
        a rimbalzare ma con forza minore.

         */
        if (transf==Tranformation.Gomma && !isGrounded)
        {
            collisionPoint = transform.position.y;
            distance = jumpPoint - collisionPoint;
            
            if (distance < 0)
                distance = 0;
           
            Vector3 vec = new Vector3(0, 250 * distance, 0);
           
            if (distance * 250 > 1200)
                vec.y = 1200;

            body.AddForce(vec , ForceMode.Impulse);
            jumpPoint /=2;
            if (distance < 1)
                isGrounded = true;
            distance = 0;
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        /*
         *  Se la trasformazione attiva � di gomma e il player si stacca da una piattaforma
         *  o terreno viene salvato il punto di stacco e la booleana di stacco viene settata a true.
         * */
        if (isGrounded && transf==Tranformation.Gomma)
        {
            jumpPoint = transform.position.y;
            isGrounded = false;
        }
    }

    private void ChangeMaterial()
    {
        Material[] materials = list[0].materials;
        Material temp;
        if (transf == Tranformation.Rame)
        {
            temp = Rame;
        }
        else if (transf == Tranformation.Carta)
        {
            temp = Carta;
        }
        else if (transf == Tranformation.Ghiaccio)
        {
            temp = Ghiaccio;
        }
        else if (transf == Tranformation.Gomma)
        {
            temp = Gomma;
        }
        else if (transf == Tranformation.Colla)
        {
            temp = Colla;
        }
        else
        {
            particelle.startColor = defColor;
            list[0].materials = defaultMAT;
            return;
        }

        for (int i = 0; i < materials.Length; i++)
            materials[i] = temp;
        list[0].materials = materials;
        
        particelle.startColor = temp.color;
    }
}
