using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTime : MonoBehaviour
{
    string time;
    private Timer Timer;   


    void Start()
    {

        Timer = FindAnyObjectByType<Timer>();
        

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            //Cogemos el tiempo de la cuenta atras que tenemos en el momento
            time=Timer.getTime();
            //Debug.Log("Tiempo: " + time);
            //Guardamos el tiempo
            Timer.saveTimes(time);
            //Destruimos el objeto del checkpoint para que no puedes volver a registrar tiempo si vuelves para atras
            //Destroy(this.gameObject);

        }
        
    }
}
