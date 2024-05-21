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

    private void OnClientConnected()
    {
        if (NetworkManager.ConnectedClientsList.Count == 2)
        {
            if (PlayerPrefs.GetString("MODO").ToLower() != "history")
            {
                IniciarRpc();
            }
            else
            {
                Iniciar2Rpc();
            }
        }
    }
    [Rpc(SendTo.ClientsAndHost)]
    public void IniciarRpc()
    {
        FindAnyObjectByType<Timer>().Inicio();
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void Iniciar2Rpc()
    {
        FindAnyObjectByType<Timer>().historyMode();
    }
    private void EsceneCargada(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        if(IsHost && sceneName != "MainMenu")
        {
            foreach(ulong id in clientsCompleted)
            {
                GameObject jugador = Instantiate(playerPrefab);
                jugador.GetComponent<NetworkObject>().SpawnAsPlayerObject(id,true);
            }
            OnClientConnected();
        }
    }
}
