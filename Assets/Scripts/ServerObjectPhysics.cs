using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ServerObjectPhysics : NetworkBehaviour
{
    [SerializeField]
    NetworkTransform m_NetworkTransform;

    [SerializeField]
    Rigidbody m_Rigidbody;
    [SerializeField]
    Vector3 Spawnpooinrttregreg;


    public override void OnNetworkObjectParentChanged(NetworkObject parentNetworkObject)
    {
        SetPhysics(parentNetworkObject == null);
    }
    public void ResetSpawnPoint()
    {
        transform.position = Spawnpooinrttregreg;
    }

    void SetPhysics(bool isEnabled)
    {
        m_Rigidbody.isKinematic = !isEnabled;
        m_Rigidbody.interpolation = isEnabled ? RigidbodyInterpolation.Interpolate : RigidbodyInterpolation.None;
        m_NetworkTransform.InLocalSpace = !isEnabled;
    }
}
