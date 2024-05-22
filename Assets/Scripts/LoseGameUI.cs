using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseGameUI : MonoBehaviour
{
    [SerializeField] private Button volverLobby;
    [SerializeField] private Button reiniciarNivel;
    [SerializeField] private Button salirMainMenu;
    // Start is called before the first frame update
    void Start()
    {
        volverLobby.onClick.AddListener(VolverLobby);
        reiniciarNivel.onClick.AddListener(ReiniciarNivel);
        salirMainMenu.onClick.AddListener(SalirMainMenu);
        
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            FindObjectOfType<PlayerInput>().actions.FindActionMap("UI").Disable();
            FindObjectOfType<PlayerInput>().actions.FindActionMap("UI").Enable();
            Debug.Log("FOCUS");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private void SalirMainMenu()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void ReiniciarNivel()
    {
        NetworkManager.Singleton.SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    private void VolverLobby()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
        NetworkManager.Singleton.Shutdown();
    }
}
