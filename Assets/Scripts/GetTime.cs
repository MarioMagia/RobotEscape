using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GetTime : MonoBehaviour
{
    string time;
    private Timer Timer;

    int playerCount = 0;


    void Start()
    {

        Timer = FindAnyObjectByType<Timer>();
        

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            playerChechpointTime();

            //Debug.Log("Tiempo: " + time);
            //Guardamos el tiempo
            
            //Destruimos el objeto del checkpoint para que no puedes volver a registrar tiempo si vuelves para atras
            //Destroy(this.gameObject);

        }
        
    }

    [Rpc(SendTo.Owner)]
    private void playerChechpointTime()
    {
        playerCount++;
        if (playerCount > 1)
        {
            //Cogemos el tiempo de la cuenta atras que tenemos en el momento
            time = Timer.getTime();
            Timer.saveTimes(time);
            destoryCheckpoint();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void destoryCheckpoint()
    {
        Destroy(this.gameObject);

    }

}
