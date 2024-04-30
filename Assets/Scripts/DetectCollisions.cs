using System;
using Cinemachine;
using StarterAssets;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectCollisions : MonoBehaviour
{

    private GameObject endGamePanel;

    [SerializeField]
    PlayerInput m_PlayerInput;
    void Start()
    {   
        // Find the ClientPlayerMove script and get the instantiated endGamePanel
        ClientPlayerMove clientPlayerMove = FindObjectOfType<ClientPlayerMove>();
        if (clientPlayerMove != null)
        {
            endGamePanel = clientPlayerMove.GetEndGamePanel();
        }
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Meta")) {
            if (endGamePanel != null)
            {
                Debug.Log("Existe");
                endGamePanel.SetActive(true);
                GameManager.Instance.SetGameState(GameManager.GameState.End);
            }
            else {
                Debug.Log("No existe");
            }
            m_PlayerInput.actions.FindActionMap("Player").Disable();
            m_PlayerInput.actions.FindActionMap("UI").Enable();
        }
    }
}
