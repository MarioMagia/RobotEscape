using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class ServerPlayerMove : NetworkBehaviour
{
    public NetworkVariable<bool> isObjectPickedUp = new NetworkVariable<bool>();

    NetworkObject m_PickedUpObject;

    [SerializeField]
    Vector3 m_LocalHeldPosition;

    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            enabled = false;
            return;
        }

        OnServerSpawnPlayer();

        base.OnNetworkSpawn();
    }

    void OnServerSpawnPlayer()
    {
        var spawnPosition = Vector3.zero;
        transform.position = spawnPosition;
    }

    [Rpc(SendTo.Server)]
    public void PickupObjectServerRpc(ulong objToPickupID)
    {
        NetworkManager.SpawnManager.SpawnedObjects.TryGetValue(objToPickupID, out var objectToPickup);
        if (objectToPickup == null || objectToPickup.transform.parent != null) return; // object already picked up, server authority says no

        if (objectToPickup.TryGetComponent(out NetworkObject networkObject) && networkObject.TrySetParent(transform))
        {
            m_PickedUpObject = networkObject;
            objectToPickup.transform.localPosition = m_LocalHeldPosition;
            isObjectPickedUp.Value = true;
        }
    }

    void ObjectDespawned()
    {
        m_PickedUpObject = null;
        isObjectPickedUp.Value = false;
    }

    [Rpc(SendTo.Server)]
    public void DropObjectServerRpc()
    {
        if (m_PickedUpObject != null)
        {
            m_PickedUpObject.transform.parent = null;
            m_PickedUpObject = null;

        }
        isObjectPickedUp.Value = false;
    }
}
