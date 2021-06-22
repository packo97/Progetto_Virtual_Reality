using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource musicsource;
    [SerializeField] string musicPath;

    [SerializeField] private Sprite audio;
    [SerializeField] private Sprite audioOff;
    [SerializeField] private Sprite music;
    [SerializeField] private Sprite musicOff;

    [SerializeField] private GameObject musicButton;

    [SerializeField] private GameObject soundButton;

    AudioClip musicClip;
    bool effectPaused=false;
    bool musicPaused=true;
    
    // Start is called before the first frame update
    void Start()
    {
        musicsource.ignoreListenerVolume= true;
        musicsource.ignoreListenerPause=true;
        musicClip= (AudioClip) Resources.Load(musicPath);
        PlayMusic();
    }

    public void SoundMute(){
    
        /*
         *  attivo/disattivo gli effetti sonori.
         *  cambio in accordo l'immagine visualizzata.
         * 
         */
        
        if(effectPaused)
            soundButton.GetComponent<Image>().sprite=audio;
        else
            soundButton.GetComponent<Image>().sprite=audioOff;
        AudioListener.pause=!effectPaused;
        effectPaused=!effectPaused;
    }

    public void SoundVolume(float volume){
        
        /*
         * Regolo il volume degli effetti sonori
         */
        
        AudioListener.volume=volume;
    }

    public void PlayMusic(){
        
        /*
         *  attivo/disattivo la musica.
         *  cambio in accordo l'immagine visualizzata.
         * 
         */
        
        musicsource.clip=musicClip;
        if(musicPaused){
            musicsource.Play();
            musicPaused=!musicPaused;
            musicButton.GetComponent<Image>().sprite=music;
        }
        else{
            musicsource.Stop();
            musicPaused=!musicPaused;
            musicButton.GetComponent<Image>().sprite=musicOff;
        }
    }

    public void MusicVolume(float volume){
        
        /*
         *   Regolo il volume della musica
         */
        
        musicsource.volume=volume;
    }
}
