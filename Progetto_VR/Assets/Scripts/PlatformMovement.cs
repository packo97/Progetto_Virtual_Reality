using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    
    private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        /*
         *  Movimento della piattaforma:
         *      1 - Se la piattaforma è nella startPosition, prendo come target da raggiungere l'end position
         *      2 - Se la piattaforma è nell' endPosition, prendo come target da raggiungere la start position
         */
        
        if(transform.localPosition == endPosition)
        {
            StartCoroutine(Vector3LerpCoroutine(gameObject, startPosition, speed));
        }
        if(transform.localPosition == startPosition)
        {
            StartCoroutine(Vector3LerpCoroutine(gameObject, endPosition, speed));
        }
    }
    
    IEnumerator Vector3LerpCoroutine(GameObject obj, Vector3 target, float speed)
    {
        /*
         * La piattaforma si muove verso il target, finchè quest'ultimo non è stato raggiunto
         */
        
        Vector3 startPosition = obj.transform.localPosition;
        float time = 0f;
 
        while(obj.transform.localPosition != target)
        {
            obj.transform.localPosition = Vector3.Lerp(startPosition, target, (time/Vector3.Distance(startPosition, target))*speed);
            time += Time.deltaTime;
            yield return null;
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
         * Se un gameobject triggera la piattaforma, viene inserita nella hierarchy così da seguire il movimento della
         * piattaforma
         */
        other.gameObject.transform.SetParent(gameObject.transform,true);
    }

    private void OnTriggerExit(Collider other)
    {
        /*
         * Se un gameobject esce dal trigger della piattaforma, viene rimosso dalla hierarchy così da non seguire più
         * il movimento della piattaforma
         */
        other.gameObject.transform.parent = null;
    }
}
