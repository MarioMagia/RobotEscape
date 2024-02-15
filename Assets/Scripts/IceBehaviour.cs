using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class IceBehaviour : NetworkBehaviour
{

    public GameObject plane;

    int playerCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("a");
        if (other.gameObject.CompareTag("Player"))
        {
            playerOnIceRpc();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void playerOnIceRpc()
    {
        playerCount++;
        Debug.Log("Players in ice: " + playerCount);
        if (playerCount > 1)
        {
            
            StartCoroutine(enableObject());
        }
    }

    IEnumerator enableObject()
    {
        Debug.Log("Spawning plane...");
        plane.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        plane.SetActive(true);
        Debug.Log("Spawned plane");
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void playerOutIceRpc()
    {
        playerCount--;
        Debug.Log("Players in ice: " + playerCount);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerOutIceRpc();
        }
    }
}
