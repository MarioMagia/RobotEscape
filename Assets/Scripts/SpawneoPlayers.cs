using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Networking.Transport;
using Unity.Services.Lobbies.Models;
using UnityEditor.PackageManager;
using UnityEngine;

public class SpawneoPlayers : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject playerPrefab; // Prefab del jugador

    // Método para spawnear el jugador
    
    public void SpawnPlayer(ulong connection)
    {
        GameObject hostObject = Instantiate(playerPrefab);
        if(IsServer)
        {
            hostObject.GetComponent<NetworkObject>().Spawn();
        }
        if(!IsServer)
        {
            SpawnPlayerRpc();
        }

    }
    [Rpc(SendTo.NotMe)]
    public void SpawnPlayerRpc()
    {
        SpawnPlayer(NetworkManager.Singleton.LocalClientId);
    }
}
