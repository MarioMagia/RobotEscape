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
        if (other.gameObject.CompareTag("Player"))
        {
            playerOnIceRpc();
        }
    }

    [Rpc(SendTo.Owner)]
    private void playerOnIceRpc()
    {
        playerCount++;
        if (playerCount > 1)
        {
            breakGlassRpc();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void breakGlassRpc()
    {
        StartCoroutine(enableObject());
    }

    
    IEnumerator enableObject()
    {
        plane.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        plane.SetActive(true);
    }

    [Rpc(SendTo.Owner)]
    private void playerOutIceRpc()
    {
        playerCount--;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerOutIceRpc();
        }
    }
}
