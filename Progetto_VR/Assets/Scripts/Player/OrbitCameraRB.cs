using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class OrbitCameraRB : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    public float rotSpeed = 1.5f;
    private float _rotY;
    private float _rotX;
    
    private Vector3 _offset;
    private float initialDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        _rotY = transform.eulerAngles.y;
        _offset = new Vector3(6, 6, 6);
        initialDistance = Vector3.Distance(target.position, transform.position);

    }
    
    private void LateUpdate()
    {
        /*
         * Se il gioco è in pausa o la scelta delle trasformazioni è aperta non è possibile muovere la camera.
         * Calcolo la rotazione sull'asse X e sull'asse Y.
         * Calcolo il Quaternion sulla base delle due rotazioni.
         * Controllo se c'è un qualsiasi gameobject tra il Player e la Camera.
         * Setto la posizione della Camera e la rotazione orientata sempre verso il Player.
         * 
         */
        
        if (!GameEvent.isPaused && !GameEvent.isChoosingTransformation)
        {
            
            _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
            _rotX += Input.GetAxis("Mouse Y") * rotSpeed * 3;
            
            //_rotX = Mathf.Clamp(_rotX, -30, 120);

            Quaternion rotation = Quaternion.Euler(_rotX, _rotY, 0);
            ObstaclesBetweenTarget();
            
            transform.position = target.position - (rotation * _offset);
            transform.LookAt(target);
            
        }
    }

    private void ObstaclesBetweenTarget()
    {
        /*
         *  Calcolo la rotazione della Camera.
         *  Calcolo la direzione in cui sta guardando la Camera.
         *  Calcolo la distanza tra la Camera e il Player.
         *
         *  Uso uno SphereCast per controllare se c'è un gameobject tra la Camera e il Player.
         *  Se è presente un gameobject nella traiettoria, faccio un Lerp sull'offset così da avvicinare la camera al
         *  Player finchè non collide più.
         *
         *  Se la Camera è più vicina al Player rispetto alla posizione iniziale, controllo se con la posizionare
         *  originale avverrebbe una collisione.
         *  Se non avviene, faccio un Lerp dalla posizione attuale della Camera alla sua posizione originale.
         * 
         */

        Quaternion lookRotation = Quaternion.Euler(transform.eulerAngles);
        Vector3 lookDirection = lookRotation * Vector3.forward;
        float distance = Vector3.Distance(transform.position, target.position);

        //Debug.DrawRay(target.position, -lookDirection * distance, Color.green);

        if (Physics.SphereCast(target.position, 1.5f, -lookDirection, out RaycastHit hit, distance))
        {
            if (!hit.collider.tag.Equals("Player") && !hit.collider.isTrigger)
            {
                //Debug.Log("collido ancora");
                //Debug.Log("collisione con " + hit.collider.name);
                _offset = Vector3.Lerp(_offset, Vector3.zero, Time.deltaTime);
            }
        }
        else
        {
            if (!Physics.SphereCast(target.position, 1.5f, -lookDirection, out RaycastHit hit2, initialDistance + 1))
            {
                //Debug.Log("non colliderei");
                _offset = Vector3.Lerp(_offset, new Vector3(6, 6, 6), Time.deltaTime);

            }
        }
    }
}
