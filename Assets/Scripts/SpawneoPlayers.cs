using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawneoPlayers : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private GameObject playerPrefab; // Prefab del jugador

    // Método para spawnear el jugador

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public override void OnNetworkSpawn()
    {
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += EsceneCargada;
    }

    private void EsceneCargada(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        if(IsHost && sceneName != "MainMenu")
        {
            foreach(ulong id in clientsCompleted)
            {
                GameObject jugador = Instantiate(playerPrefab);
                jugador.GetComponent<NetworkObject>().SpawnAsPlayerObject(id);
            }
        }
    }
}
