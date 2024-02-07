using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
using UnityEngine.Windows;
#endif


[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM 
    [RequireComponent(typeof(PlayerInput))]
#endif
public class PauseMenu : MonoBehaviour
{
    public static bool menuShown = false;
    public GameObject pauseMenuUI;
    public StarterAssetsInputs _input;
    private PlayerInput _playerInput;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_input.pauseMenu) {
            _input.pauseMenu = false;
            if (menuShown)
            {
                Resume();
            }
            else {
                Show();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        menuShown = false;
    }


    void Show() {
        pauseMenuUI.SetActive(true);
        menuShown = true;
    }
    public void LoadSettings() 
    {
        Debug.Log("Load Settings");
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game");
    }
}
