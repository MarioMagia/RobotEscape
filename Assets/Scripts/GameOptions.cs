using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameOptions : NetworkBehaviour
{
    public bool TakeTPs;
    public bool AllowTPs;
    public float TP_CD = 1f;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
    }
}

