using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float minXLook;
    [SerializeField] private float maxXlook;
    [SerializeField] private Transform cameraAnchor;

    [SerializeField] private bool invertXRotation;
    private float curXRot;

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform camera;

    private bool zoomPosition;
    private Vector3 previousPosition;

    private float initialDistance;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        zoomPosition = false;
        initialDistance = Vector3.Distance(target.position, camera.position);
    }
    
    private void LateUpdate()
    {
        /*
         * Muove la camera considerando i movimenti del mouse e la sensibilità.
         * Impone dei limiti sulla rotazione intorno all'asse x.
         * 
         */
        if (!GameEvent.isChoosingTransformation)
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            transform.eulerAngles += Vector3.up * x * lookSensitivity;
        
            if (invertXRotation)
                curXRot += y * lookSensitivity;
            else
                curXRot -= y * lookSensitivity;

            curXRot = Mathf.Clamp(curXRot, minXLook, maxXlook);

            Vector3 clampedAngle = cameraAnchor.eulerAngles;
            clampedAngle.x = curXRot;

            cameraAnchor.eulerAngles = clampedAngle;
        
            ObstaclesBetweenTarget();
        }
        
    }

    
    private void ObstaclesBetweenTarget()
    {
        /*
         *  potrebbe andare meglio, da chiedere al prof
         * 
         */
        
        
        float dist = Vector3.Distance(camera.position, target.transform.position);
        Quaternion lookRotation = Quaternion.Euler(camera.eulerAngles);
        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = target.position - lookDirection * dist;
        
        if (Physics.Raycast(target.position, -lookDirection, out RaycastHit hit, dist))
        {
            if (!zoomPosition)
            {
                previousPosition = lookPosition;
            }

            if (!hit.collider.tag.Equals("Respawn"))
            {
                //Debug.Log(hit.collider.name);
                lookPosition = target.position - lookDirection * hit.distance;
                zoomPosition = true;
            }
        }
        else if (zoomPosition)
        {
            //float dist2 = Vector3.Distance(previousPosition, target.transform.position);
            if (!Physics.Raycast(target.position, -lookDirection, out RaycastHit hit2, initialDistance))
            {
                previousPosition = lookPosition;
                
                lookPosition = target.position - lookDirection * initialDistance;
                zoomPosition = false;
                
            }
                    
            //Debug.Log(previousPosition);
            //Debug.Log("uscito");
        }
        
        
        camera.SetPositionAndRotation(lookPosition, lookRotation);

    }
    
    
}
