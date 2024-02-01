using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;




public class Teleport : MonoBehaviour
{

    public GameObject Marca;
    public bool crearMarca=false;
    

  
    void Update()
    {
        if (Keyboard.current.mKey.isPressed && !crearMarca)
        {
            CrearMarca();
            crearMarca = true;


        }
    }


    void CrearMarca()
    {
        // Obt�n la posici�n del jugador
        Vector3 posicionJugador = transform.position;

        // Crea la "Marca" en la posici�n del jugador
        GameObject nuevaMarca = Instantiate(Marca, posicionJugador, Quaternion.identity);

     

       

    }
}

