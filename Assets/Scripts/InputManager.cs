/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public   StarterAssetsInputs inputActions;
    public InputActionMap uiActionMap;
    public InputActionMap playerActionMap;
    public static event Action<InputActionMap> actionMapChange;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        ToggleActionMap(inputActions.Player);
        Resume();
    }

    // Update is called once per frame
    public static void ToggleActionMap(InputActionMap actionMap)
    {
        if (actionMap.enabled)
            return;
        inputActions.Disable();
        actionMapChange?.Invoke(actionMap);
        actionMap.Enable();
    }
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame) {
            //Pressing escape while in game
            if (inputActions.Player.enabled)
            {
                Show();
                ToggleActionMap(inputActions.inMenu);
            }
            //Pressing escape while in menu
            else if (inputActions.inMenu.enabled) {
                Resume();
                ToggleActionMap(inputActions.Player);
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
    }


    void Show()
    {
        pauseMenuUI.SetActive(true);
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
*/