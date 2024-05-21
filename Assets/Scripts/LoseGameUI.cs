using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
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

    private void SalirMainMenu()
    {
        FindAnyObjectByType<ProjectSceneManager>().LoadNetworkSceneRpc("MainMenu");
        NetworkManager.Singleton.Shutdown();
    }

    private void ReiniciarNivel()
    {
        FindAnyObjectByType<ProjectSceneManager>().LoadNetworkSceneRpc("LevelTutorial");
    }

    private void VolverLobby()
    {

        FindAnyObjectByType<ProjectSceneManager>().LoadNetworkSceneRpc("Lobby");
        NetworkManager.Singleton.Shutdown();
    }
}
