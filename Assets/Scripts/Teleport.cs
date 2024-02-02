using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;




public class Teleport : MonoBehaviour
{

    public GameObject Marca;
    public bool crearMarca = false;
    public Vector3 posicionDeLaMarca;
    public GameObject nuevaMarca;



    void Update()
    {
        if (Keyboard.current.mKey.isPressed && !crearMarca)
        {
            CrearMarca();
            crearMarca = true;
        }

        if (Keyboard.current.tKey.isPressed && crearMarca)
        {

            Teleportarse();
            

        }
    }


    void CrearMarca()
    {
        // Obt�n la posici�n del jugador
        Vector3 posicionJugador = transform.position;

        // Crea la "Marca" en la posici�n del jugador
        nuevaMarca = Instantiate(Marca, posicionJugador, Quaternion.identity);

        // Guarda la posici�n de la marca en la variable
        posicionDeLaMarca = nuevaMarca.transform.position;

    }

    void Teleportarse()
    {
        // Teleporta al jugador a la posici�n almacenada en posicionDeLaMarca
        transform.position = posicionDeLaMarca;
       
        
        // Destruye el objeto nuevaMarca
        if (nuevaMarca != null)
        {
            Destroy(nuevaMarca);
        }
       

        // Llama a CambiarMarca despu�s de un peque�o retraso (0.1 segundos)
        Invoke("CambiarMarca", 0.1f);


    }

    void CambiarMarca() {
        crearMarca = false;
    }
}

