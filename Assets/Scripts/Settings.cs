using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
   [SerializeField] private AudioMixer audioMixer;
   [SerializeField] private Slider musicSlider;
   [SerializeField] private Slider SFXSlider;
   [SerializeField] private Font nuevaFontText;
   [SerializeField] private TMP_FontAsset nuevaFontTextMesh;



    void Start()
    {
        // Encuentra todos los objetos en la escena que tienen un componente de TextMeshProUGUI(aunque esten inactivos)
        TextMeshProUGUI[] textMeshPros = FindObjectsOfType<TextMeshProUGUI>(true);

       

        // Recorre todos los objetos de TextMeshProUGUI encontrados y cambia su fuente
        foreach (TextMeshProUGUI textMeshPro in textMeshPros)
        {
            // Asigna la nueva fuente
            textMeshPro.font = nuevaFontTextMesh;
        }

        // Encuentra todos los objetos en la escena que tienen un componente de texto(aunque esten inactivos)
        Text[] textos = FindObjectsOfType<Text>(true);

        // Recorre todos los objetos de texto encontrados y cambia su tipo de texto
        foreach (Text texto in textos)
        {
            // Asigna el nuevo tipo de texto
            texto.font = nuevaFontText;
        }

        




        //Cogemos el volumen de la musica que tenemos guardado en PlayerPrefs    
        float volumeMusic = PlayerPrefs.GetFloat("volumeMusic");

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
