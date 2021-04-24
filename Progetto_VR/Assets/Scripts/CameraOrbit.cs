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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void LateUpdate()
    {
        /*
         * Muove la camera considerando i movimenti del mouse e la sensibilità.
         * Impone dei limiti sulla rotazione intorno all'asse x.
         * 
         */
        
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
    }
    
}
