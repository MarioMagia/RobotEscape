using StarterAssets;
using System;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using TMPro;
#endif


public class PauseMenu : MonoBehaviour
{
    public static bool menuShown = false;
    public static bool settingShown = false;
    public static bool controlsShown = false;

    public GameObject pauseMenuUI;

    public GameObject settingsMenuUI;

    public GameObject controlsMenuUI;

    [SerializeField] private AudioMixer audioMixer;

    public Dropdown resolutionDropdown;
    public GameObject darkLayer;

    public Button resumeButton;
    public Dropdown renderDropdown;

    public TMP_Dropdown controlsDropdown;
    public GameObject keyboardControlsPanel;
    public GameObject gamepadControlsPanel;

    public PlayerInput _playerInput;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    Resolution[] resolutions;


    private void Awake()
    {
        //Configuramos el slider de la musica al iniciar para que se vea en el punto donde lo guardamos
        musicSlider.value = PlayerPrefs.GetFloat("volumeMusic");

        //Configuramos el slider de los SFX al iniciar  para que se vea en el punto donde lo guardamos
        SFXSlider.value = PlayerPrefs.GetFloat("volumeSFX"); 
        


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
    public int changePauseState()
    {
        //0 - pauseOut
        //1 - pauseIn
        if (controlsShown && (keyboardControlsPanel.activeInHierarchy || gamepadControlsPanel.activeInHierarchy))
        {
            QuitScreen("controls");
            return 1;
        }
        else if (settingShown || controlsShown)
        {
            Cursor.lockState = CursorLockMode.None;

            if (settingShown)
            {
                QuitScreen("settings");

            }
            else if (controlsShown) {
                QuitScreen("controls");

            }
            return 1;

        }
        else if (menuShown)
        {

            Cursor.lockState = CursorLockMode.Locked;
            Hide();
            Cursor.visible = false;
            return 0;
        }
        else
        {
            resumeButton.Select();
            Cursor.lockState = CursorLockMode.None;
            Show();
            Cursor.visible = true;
            return 1;
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        darkLayer.SetActive(false);
        menuShown = false;
        _playerInput.actions.FindActionMap("UI").Disable();
        _playerInput.actions.FindActionMap("Player").Enable();
        Cursor.visible = false;

        // input_Manager.Resume();
    }

    void Hide() {
        pauseMenuUI.SetActive(false);
        darkLayer.SetActive(false);
        menuShown = false;
    }
    void Show() {
        pauseMenuUI.SetActive(true);
        menuShown = true;
        darkLayer.SetActive(true);
    }
    public void LoadScreen(string screen) 
    {
        if (screen == "settings")
        {
            settingsMenuUI.SetActive(true);
            pauseMenuUI.SetActive(false);
            settingShown = true;
            renderDropdown.Select();
        }
        else if (screen == "controls") {
            controlsMenuUI.SetActive(true);
            pauseMenuUI.SetActive(false);
            controlsShown = true;
            // Subscribe to the OnValueChanged event of the dropdown
            controlsDropdown.onValueChanged.AddListener(delegate {
                DropdownValueChanged(controlsDropdown);
            });
            keyboardControlsPanel.SetActive(true);
            gamepadControlsPanel.SetActive(false);
            controlsDropdown.value = 0;
            controlsDropdown.Select();
        }

    }
    public void QuitScreen(string screen) {
        if (screen == "settings")
        {
            settingsMenuUI.SetActive(false);
            pauseMenuUI.SetActive(true);
            settingShown = false;
        }
        else if (screen == "controls")
        {
            controlsMenuUI.SetActive(false);
            pauseMenuUI.SetActive(true);
            controlsShown = false;
            
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
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

}
