using LobbyRelaySample.ngo;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;
using UnityEngine;

public static class INICIO
{
    public static async void setup()
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(5);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));
        var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        NetworkManager.Singleton.StartHost();
    }
    public static void ini_print()
    {
        Debug.Log("Hello World");
    }
}
