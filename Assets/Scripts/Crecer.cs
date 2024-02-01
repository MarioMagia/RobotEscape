using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Crecer : MonoBehaviour
    
{

    public float escalaCrecimiento = 2f;


    public bool crecido = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (crecido && Keyboard.current.eKey.isPressed)
        {
            // Si ya ha crecido y se presiona la tecla nuevamente, reiniciar el temporizador
            ReiniciarTemporizador();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Crecer"))
        {
            if (Keyboard.current.eKey.isPressed && !crecido )
            {
                CrecerPersonaje();
            }
        }
    }

    void CrecerPersonaje()
    {
        // Escala el objeto actual
        transform.localScale *= escalaCrecimiento;
        crecido = true;
    
    // Inicia el temporizador para revertir el crecimiento después de 5 segundos
    Invoke("RevertirCrecimiento", 5f);
}

void ReiniciarTemporizador()
{
    // Cancela la llamada a la función de revertir crecimiento
    CancelInvoke("RevertirCrecimiento");

    // Reinicia el temporizador
    Invoke("RevertirCrecimiento", 5f);
}

void RevertirCrecimiento()
{
    // Revierte el crecimiento y restablece la bandera
    transform.localScale /= escalaCrecimiento;
    crecido = false;
}
}
