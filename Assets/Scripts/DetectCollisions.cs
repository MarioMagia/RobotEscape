using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectCollisions : NetworkBehaviour
{
    [SerializeField]
    private GameObject endGamePanel;
    private int playersWaiting = 0;
    private GameObject player;
    [SerializeField] private string checkpointId;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            AddPlayersWaitingRpc();
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus && endGamePanel.activeSelf)
        {
            FindObjectOfType<PlayerInput>().actions.FindActionMap("UI").Disable();
            FindObjectOfType<PlayerInput>().actions.FindActionMap("UI").Enable();
            Debug.Log("FOCUS");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
        if (playersWaiting > 1)
        {
            if (PlayerPrefs.GetString("MODO").ToLower() == "time trial")
            {
                Timer timer = FindAnyObjectByType<Timer>();
                string time = timer.getTime();
                timer.saveTimes(time, checkpointId);
                ArrayList times = timer.GetTimes();

                // Initialize a new list to store formatted times
                List<Dictionary<string, string>> formattedTimesList = new List<Dictionary<string, string>>();

                for (int i = 0; i < times.Count; i++)
                {
                    var kvp = (KeyValuePair<string, string>)times[i];
                    if (i == 0)
                    {
                        // Add the first time entry as is
                        formattedTimesList.Add(new Dictionary<string, string>
                    {
                        { "id", kvp.Key },
                        { "time", TimeFormatter.FormatTime(int.Parse(kvp.Value)) }
                    });
                    }
                    else
                    {
                        int previousTimeSeconds = int.Parse(((KeyValuePair<string, string>)times[i - 1]).Value);
                        int currentTimeSeconds = int.Parse(kvp.Value);
                        int differenceSeconds = currentTimeSeconds - previousTimeSeconds;

                        formattedTimesList.Add(new Dictionary<string, string>
                    {
                        { "id", kvp.Key },
                        { "time", TimeFormatter.FormatTime(differenceSeconds) }
                    });
                    }
                }

                // Convert formattedTimesList back to ArrayList
                ArrayList formattedTimes = new ArrayList();
                foreach (var item in formattedTimesList)
                {
                    formattedTimes.Add(new KeyValuePair<string, string>(item["id"], item["time"]));
                }

                FindObjectOfType<Match>().EndStage(formattedTimes);
            }

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
