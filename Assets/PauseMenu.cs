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

    public GameObject pauseMenuUI;

    public GameObject settingsMenuUI;

    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;
    public GameObject darkLayer;

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

    public void changePauseState()
    {
        if (settingShown)
        {
            Debug.Log("Settings Out");
            Cursor.lockState = CursorLockMode.None;
            QuitSettings();
        }
        else if (menuShown)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Resume();
            Cursor.visible = false;
        }
        else
        {

            Cursor.lockState = CursorLockMode.None;
            Show();
            Cursor.visible = true;
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        menuShown = false;
        darkLayer.SetActive(false);
    }


    void Show() {
        pauseMenuUI.SetActive(true);
        menuShown = true;
        darkLayer.SetActive(true);
    }
    public void LoadSettings() 
    {
        settingsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        settingShown = true;

    }
    public void QuitSettings() {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        settingShown = false;
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
