using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GetTime : NetworkBehaviour
{
    string time;
    private Timer Timer;

    [SerializeField] private string checkpointName;

    int playerCount = 0;


    void Start()
    {

        Timer = FindAnyObjectByType<Timer>();
        

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            playerChechpointTimeRpc();

            //Debug.Log("Tiempo: " + time);
            //Guardamos el tiempo
            
            //Destruimos el objeto del checkpoint para que no puedes volver a registrar tiempo si vuelves para atras
            //Destroy(this.gameObject);

        }
        
    }

    [Rpc(SendTo.Owner)]
    private void playerChechpointTimeRpc()
    {
        playerCount++;
        if (playerCount > 1)
        {
            //Cogemos el tiempo de la cuenta atras que tenemos en el momento
            time = Timer.getTime();
            Timer.saveTimes(time,checkpointName);
            destoryCheckpointRpc();
        }
    }

    [Rpc(SendTo.Server)]
    private void destoryCheckpointRpc()
    {
        //Buscamos el componente Networkobject dentro del checkpoint teleport
        NetworkObject networkObject = this.gameObject.GetComponent<NetworkObject>();
        //Lo eliminamos
        networkObject.Despawn(true);

    }

}
