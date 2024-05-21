using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawneoPlayers : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject playerPrefab; // Prefab del jugador

    public override void OnNetworkSpawn()
    {
        DontDestroyOnLoad(this.gameObject);
        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += EsceneCargada;
    }
    [Rpc(SendTo.ClientsAndHost)]
    private void InicioTimersRpc()
    {
        Debug.Log("Iniciando");
        if (PlayerPrefs.GetString("MODO").ToLower() != "history")
        {
            Debug.Log("Iniciando1");
            FindAnyObjectByType<Timer>().Inicio();
        }
        else
        {
            Debug.Log("Iniciando2");
            FindAnyObjectByType<Timer>().historyMode();
        }

    }
    private void EsceneCargada(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        Debug.Log("Cargado");
        if (IsHost && sceneName != "MainMenu")
        {
            foreach (ulong id in clientsCompleted)
            {
                GameObject jugador = Instantiate(playerPrefab);
                jugador.GetComponent<NetworkObject>().SpawnAsPlayerObject(id, true);
            }
            InicioTimersRpc();
        }
    }
}
