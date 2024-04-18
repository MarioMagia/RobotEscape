using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
   [SerializeField] private AudioMixer audioMixer;
   [SerializeField] private Slider musicSlider;
   [SerializeField] private Slider SFXSlider;


    // Start is called before the first frame update
    void Start()
    {
        //Cogemos el volumen de la musica que tenemos guardado en PlayerPrefs    
        float volumeMusic= PlayerPrefs.GetFloat("volumeMusic");

        //Configuramos el AudioMixer de musica con el volumen para iniciar el juego con el ultimo valor que guardamos
        audioMixer.SetFloat("volumeMusic", volumeMusic);

        //Configuramos el slider de la musica para que se vea en el punto donde lo guardamos
        musicSlider.value = volumeMusic;
        

        //Cogemos el volumen de los SFX que tenemos guardado en PlayerPrefs   
        float volumeSFX = PlayerPrefs.GetFloat("volumeSFX");

        //Configuramos el AudioMixer de los SFX con el volumen para iniciar el juego con el ultimo valor que guardamos
        audioMixer.SetFloat("volumeSFX", volumeSFX);

        //Configuramos el slider de los SFX para que se vea en el punto donde lo guardamos
        SFXSlider.value = volumeSFX;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
