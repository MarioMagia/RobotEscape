using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class NetworkButtons : MonoBehaviour
{
    private string IP = "Client IP";
    private ushort serverPort = 7777;

    private void OnGUI()
    {

        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            
            if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
            if (GUILayout.Button("Client") && !string.IsNullOrEmpty(IP) &&!IP.Equals("Client IP"))
            {
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
                    IP,  // IP que entra por el input
                    serverPort // Puerto server
                );
                NetworkManager.Singleton.StartClient();
            }
            IP = GUILayout.TextField(IP, 25);
        }

        GUILayout.EndArea();
    }

    // private void Awake() {
    //     GetComponent<UnityTransport>().SetDebugSimulatorParameters(
    //         packetDelay: 120,
    //         packetJitter: 5,
    //         dropRate: 3);
    // }
}