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
#endif


public class PauseMenu : MonoBehaviour
{
    public static bool menuShown = false;
    public static bool settingShown = false;
    public static bool controlsShown = false;

    public GameObject pauseMenuUI;

    public GameObject settingsMenuUI;

    public GameObject controlsMenuUI;

    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;
    public GameObject darkLayer;

    public Button resumeButton;
    public Dropdown renderDropdown;

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
    void Start()
    {
        resumeButton.Select();

    }
    public int changePauseState()
    {
        //0 - pauseOut
        //1 - pauseIn
        if (settingShown || controlsShown)
        {
            Debug.Log("Settings Out");
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
    }

}
