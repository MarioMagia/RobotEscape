using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ClientObject : NetworkBehaviour
{
    ServerObject serverObject;
    // Start is called before the first frame update
    void Awake()
    {
        serverObject = GetComponent<ServerObject>();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        enabled = IsClient;
    }

}
