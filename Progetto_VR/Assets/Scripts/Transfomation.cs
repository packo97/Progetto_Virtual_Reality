using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transfomation : MonoBehaviour
{
    //Tipi di materiale per le trasformaz<ioni
    [System.Serializable] public enum Tranformation {Default=0 , Rame, Carta, Ghiaccio, Gomma, Colla};
    public Tranformation transf;
    
    //variabili per simulare il rimbalzo della gomma
    float jumpPoint, collisionPoint, distance;
    bool isGrounded=true;

    Rigidbody body;
    void Start()
    {
        transf = Tranformation.Default;
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }


    
    void OnCollisionEnter(Collision collision)
    {
        /*
         Se la trasformazione attiva è di gomma e il player non è attaccato al terreno calcolo la 
        distanza tra il punto di stacco e di collisione che ne determina la forza con cui 
        il player rimbalza (sopra un certo limite la forza di rimbalzo viene bloccata ad 
        un determinato gap). finche la distanza di rimbalzo non sarà trascurabile il player continuarà 
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
         *  Se la trasformazione attiva è di gomma e il player si stacca da una piattaforma
         *  o terreno viene salvato il punto di stacco e la booleana di stacco viene settata a true.
         * */
        if (isGrounded && transf==Tranformation.Gomma)
        {
            jumpPoint = transform.position.y;
            isGrounded = false;
        }
    }
}
