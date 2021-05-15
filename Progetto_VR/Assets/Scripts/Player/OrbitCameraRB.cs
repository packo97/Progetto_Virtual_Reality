using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCameraRB : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float rotSpeed = 1.5f;
    private float _rotY;
    private float _rotX;
    
    private Vector3 _offset;
    
    private bool zoomPosition;
    private Vector3 previousPosition;
    private Vector3 initialOffSet;

    private float initialDistance;
    // Start is called before the first frame update
    void Start()
    {
        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position;

        zoomPosition = false;
        initialOffSet = _offset;
        initialDistance = Vector3.Distance(target.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (!GameEvent.isChoosingTransformation)
        {
            float horInput = Input.GetAxis("Horizontal");
        
            if (horInput != 0)
            {
                _rotY += horInput * rotSpeed;
            }
            else
            {
                _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
            }

            _rotX += Input.GetAxis("Mouse Y") * rotSpeed * 3;
            _rotX = Mathf.Clamp(_rotX, -30, 120);

            Quaternion rotation = Quaternion.Euler(_rotX, _rotY, 0);
            //ObstaclesBetweenTarget();
            transform.position = target.position - (rotation * _offset);
            transform.LookAt(target);
        }
    }
    
    
    private void ObstaclesBetweenTarget()
    {
        /*collisione ostacoli non funziona*/
        Quaternion lookRotation = Quaternion.Euler(transform.eulerAngles);
        Vector3 lookDirection = lookRotation * Vector3.forward;
        float distance = Vector3.Distance(transform.position, target.position);
      
        Debug.DrawRay(target.position, -lookDirection * distance, Color.green);
        if (Physics.Raycast(target.position, -lookDirection, out RaycastHit hit, distance))
        {
            Debug.Log(hit.collider.name);
            if (!hit.collider.tag.Equals("Player") && !hit.collider.tag.Equals("Respawn"))
            {
                Debug.Log("collisione con " + hit.collider.name + " " + hit.distance + " " + hit.point);
                _offset = target.position - hit.point;
                zoomPosition = true;
            }
                
        } 
        if (zoomPosition)
        {
            if (Physics.Raycast(target.position, -lookDirection, out RaycastHit hit2, initialDistance))
            {
                Debug.Log("collido ancora con " + hit2.collider.name);
            }
        }
        

    }
}
