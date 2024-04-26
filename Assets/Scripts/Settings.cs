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
   [SerializeField] private Font nuevaFont;


    // Start is called before the first frame update
    void Start()
    {
        // Encuentra todos los objetos en la escena que tienen un componente de texto
        Text[] textos = FindObjectsOfType<Text>();

        // Recorre todos los objetos de texto encontrados y cambia su tipo de texto
        foreach (Text texto in textos)
        {
            // Asigna el nuevo tipo de texto
            texto.font = nuevaFont;
        }

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
