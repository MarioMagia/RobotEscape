using LobbyRelaySample.ngo;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;
using UnityEngine;
using System;
using Unity.Services.Lobbies.Models;
using LobbyRelaySample;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

public class INICIO : MonoBehaviour
{
    // Start is called before the first frame update
    public static async Task<string> setup(int max)
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(max);
        var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        PlayerPrefs.SetString("joinCode", joinCode);
        PlayerPrefs.Save();
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));
        return NetworkManager.Singleton.StartHost() ? joinCode : null;
    }

    public static async Task<bool> setup2(string code)
    {
        var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode: code);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));
        return !string.IsNullOrEmpty(code) && NetworkManager.Singleton.StartClient();

    }
}
