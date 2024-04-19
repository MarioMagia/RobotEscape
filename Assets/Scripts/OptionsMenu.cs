using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{


    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;
    public Dropdown renderDropdown;
    public TMP_Dropdown controlsDropdown;
    public GameObject keyboardControlsPanel;
    public GameObject gamepadControlsPanel;



    Resolution[] resolutions;


    private void Awake()
    {

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }



    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        //GetComponent<CanvasScaler>().referenceResolution = new Vector2(resolution.width, resolution.height);
    }
    public void SetVolumeMusic(float volumeMusic)
    {
        //Seteamos el audiomixer de Musica con el volumen que escogemos con la barra de volumen
        audioMixer.SetFloat("volumeMusic", volumeMusic);

        //Guardamos el volumen de la musica que acabamos de configurar en Playerprefs para poder coger ese valor en cualquier otra scene o al iniciar el juego
        PlayerPrefs.SetFloat("volumeMusic", volumeMusic);
    }

    public void SetVolumeSFX(float volumeSFX)
    {
        //Seteamos el audiomixer de los SFX con el volumen que escogemos con la barra de volumen
        audioMixer.SetFloat("volumeSFX", volumeSFX);

        //Guardamos el volumen de los SFX que acabamos de configurar en Playerprefs para poder coger ese valor en cualquier otra scene o al iniciar el juego
        PlayerPrefs.SetFloat("volumeSFX", volumeSFX);
    }


    public void Controls()
    {
        // Coge el valor que se ha escogido del dropdown de control(Keyboard = 0, Gamepad = 1)
        controlsDropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(controlsDropdown);
        });
        keyboardControlsPanel.SetActive(true); //Al iniciar muestra los controles de keyboard
        gamepadControlsPanel.SetActive(false);
        controlsDropdown.value = 0;
        controlsDropdown.Select();//Muestra el tipo de control que se ha selecionado

    }

    // Coge el valor que se ha escogido del dropdown de control y muestra el selecionado
    void DropdownValueChanged(TMP_Dropdown change)
    {
        if (change.value == 0) // Keyboard Controls selected
        {
            gamepadControlsPanel.SetActive(false);
            keyboardControlsPanel.SetActive(true);
        }
        else if (change.value == 1) // Gamepad Controls selected
        {
            keyboardControlsPanel.SetActive(false);
            gamepadControlsPanel.SetActive(true);
        }
    }

}
