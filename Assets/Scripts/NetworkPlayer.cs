using Unity.Netcode;
using UnityEngine;

/// <summary>
/// An example network serializer with both server and owner authority.
/// Love Tarodev
/// </summary>
public class PlayerTransform : NetworkBehaviour
{
    [SerializeField] private Transform ObjetoCreadoPrefab;
    private Transform ObjetoNuevo;
    private NetworkVariable<int> numeroRandom = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        numeroRandom.OnValueChanged += (int antes, int despues) =>
        {
            Debug.Log(numeroRandom.Value);
        };
    }

    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            ObjetoNuevo = Instantiate(ObjetoCreadoPrefab);
            ObjetoNuevo.GetComponent<NetworkObject>().Spawn(true);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ObjetoNuevo.GetComponent<NetworkObject>().Despawn(true);
        }
        Vector3 movDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) movDir.z = +1f;
        if (Input.GetKey(KeyCode.A)) movDir.x = -1f;
        if (Input.GetKey(KeyCode.S)) movDir.z = -1f;
        if (Input.GetKey(KeyCode.D)) movDir.x = +1f;
        if (Input.GetKeyDown(KeyCode.Space)) movDir.y = +5f;

        float moveSpeed = 20f;
        transform.position += moveSpeed * movDir * Time.deltaTime;
    }
}