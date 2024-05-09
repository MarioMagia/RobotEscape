using System;
using Cinemachine;
using StarterAssets;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectCollisions : NetworkBehaviour
{
    private int playersWaiting = 0;
    private GameObject player;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            AddPlayersWaitingRpc();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RemovePlayersWaitingRpc();
        }
            
    }

    [Rpc(SendTo.Server)]
    private void AddPlayersWaitingRpc()
    {
        playersWaiting++;
        if(playersWaiting > 1)
        {
            FinishLevelRpc();
        }
    }

    [Rpc(SendTo.Server)]
    private void RemovePlayersWaitingRpc()
    {
        playersWaiting--;
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void FinishLevelRpc()
    {
        ClientPlayerMove playerMove = player.GetComponent<ClientPlayerMove>();
        playerMove.gameFinish = true;
        GameObject endGamePanel = playerMove.GetEndGamePanel();
        PlayerInput input = player.GetComponent<PlayerInput>();
        if (endGamePanel != null)
        {
            Debug.Log("Existe");
            endGamePanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Debug.Log("No existe");
        }
        input.actions.FindActionMap("Player").Disable();
        input.actions.FindActionMap("UI").Enable();
        
        
    }
}
