using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class IceBehaviour : NetworkBehaviour
{

    public GameObject plane;
    private AudioSource audioSource;

    int playerCount = 0;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.loop = true; // Ensure the audio source is set to loop
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
        if (playerCount == 1)
        {
            // Start playing the sound when the first player enters
            audioSource.Play();
        }
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
        if (playerCount == 0)
        {
            // Stop playing the sound when the last player exits
            audioSource.Stop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerOutIceRpc();
        }
    }
}
