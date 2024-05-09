using System;
using System.Collections;
using Cinemachine;
using StarterAssets;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Assumes client authority
/// </summary>
[DefaultExecutionOrder(1)] // after server component
public class ClientPlayerMove : NetworkBehaviour
{
    [SerializeField]
    ServerPlayerMove m_ServerPlayerMove;

    [SerializeField]
    CharacterController m_CharacterController;

    [SerializeField]
    ThirdPersonController m_ThirdPersonController;

    [SerializeField]
    CapsuleCollider m_CapsuleCollider;

    [Header("Camera Follow")]
    [SerializeField]
    Transform m_CameraFollow;

    [SerializeField]
    PlayerInput m_PlayerInput;

    [SerializeField]
    GameObject pauseScreen;

    [SerializeField]
    GameObject endGamePanel;

    [SerializeField]
    DetectMarks detect;

    RaycastHit[] m_HitColliders = new RaycastHit[4];

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // ThirdPersonController & CharacterController are enabled only on owning clients. Ghost player objects have
        // these two components disabled, and will enable a CapsuleCollider. Per the CharacterController documentation: 
        // https://docs.unity3d.com/Manual/CharacterControllers.html, a Character controller can push rigidbody
        // objects aside while moving but will not be accelerated by incoming collisions. This means that a primitive
        // CapsuleCollider must instead be used for ghost clients to simulate collisions between owning players and 
        // ghost clients.
        m_ThirdPersonController.enabled = false;
        m_CapsuleCollider.enabled = false;
        m_CharacterController.enabled = false;
        endGamePanel = Instantiate(endGamePanel);
        endGamePanel.SetActive(false);
        pauseScreen = Instantiate(pauseScreen);
    }

    public GameObject GetEndGamePanel()
    {
        return endGamePanel;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        enabled = IsClient;
        if (!IsOwner)
        {
            enabled = false;
            m_CharacterController.enabled = false;
            m_CapsuleCollider.enabled = true;
            return;
        }
        m_PlayerInput.enabled = true;
        m_ThirdPersonController.enabled = true;
        m_CharacterController.enabled = true;
        var cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = m_CameraFollow;
        if (IsServer)
        {
            NetworkManager.OnClientConnectedCallback += OnClientConnected;
        }
    }

    private void OnClientConnected(ulong obj)
    {
        if (NetworkManager.ConnectedClientsList.Count == 2)
        {
            if (PlayerPrefs.GetString("MODO").ToLower() != "history")
            {
                IniciarRpc();
            }
            else
            {
                Iniciar2Rpc();
            }
        }
    }
    [Rpc(SendTo.ClientsAndHost)]
    public void IniciarRpc()
    {
        FindAnyObjectByType<Timer>().Inicio();
    }
    [Rpc(SendTo.ClientsAndHost)]
    public void Iniciar2Rpc()
    {
        FindAnyObjectByType<Timer>().historyMode();
    }

    void OnPickUp()
    {
        if (m_ServerPlayerMove.isObjectPickedUp.Value)
        {
            m_ServerPlayerMove.DropObjectServerRpc();
        }
        else
        {

            var hits = Physics.BoxCastNonAlloc(transform.position,
                Vector3.one,
                transform.forward,
                m_HitColliders,
                Quaternion.identity,
                1f,
                LayerMask.GetMask(new[] { "PickupItems" }),
                QueryTriggerInteraction.Ignore);
            if (hits > 0)
            {
                var objeto = m_HitColliders[0].collider.gameObject.GetComponent<ServerObject>();
                if (objeto != null)
                {
                    var netObj = objeto.NetworkObjectId;
                    m_ServerPlayerMove.PickupObjectServerRpc(netObj);
                }
            }
        }
    }
    void OnTakeMark()
    {
        Debug.Log("TOCAAAA");
        detect.TM();


    }

    void OnPauseMenu()
    {
        Debug.Log("Pause input");
        int pauseState = pauseScreen.GetComponent<PauseMenu>().changePauseState();
        if (pauseState == 1)
        {
            m_PlayerInput.actions.FindActionMap("Player").Disable();
            m_PlayerInput.actions.FindActionMap("UI").Enable();
        }
        else
        {

            m_PlayerInput.actions.FindActionMap("UI").Disable();
            m_PlayerInput.actions.FindActionMap("Player").Enable();
        }
    }


}
