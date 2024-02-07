using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
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

    void Resume() {
        pauseMenuUI.SetActive(false);
        menuShown = false;
    }
    void Show() {
        pauseMenuUI.SetActive(true);
        menuShown = true;
    }
}
