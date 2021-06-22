using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    private AudioSource _audioSource;
    // Start is called before the first frame update

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        QualitySettings.SetQualityLevel(2);
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Open()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameObject.SetActive(true);
        _audioSource.Play();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void SetQuality(int value)
    {
        QualitySettings.SetQualityLevel(value);
    }
}
