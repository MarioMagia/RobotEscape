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
        // Obtén la posición del jugador
        Vector3 posicionJugador = transform.position;

        // Crea la "Marca" en la posición del jugador
        nuevaMarca = Instantiate(Marca, posicionJugador, Quaternion.identity);

        // Guarda la posición de la marca en la variable
        posicionDeLaMarca = nuevaMarca.transform.position;

    }

    void Teleportarse()
    {
        // Teleporta al jugador a la posición almacenada en posicionDeLaMarca
        transform.position = posicionDeLaMarca;
       
        
        // Destruye el objeto nuevaMarca
        if (nuevaMarca != null)
        {
            Destroy(nuevaMarca);
        }
       

        // Llama a CambiarMarca después de un pequeño retraso (0.1 segundos)
        Invoke("CambiarMarca", 0.1f);


    }

    void CambiarMarca() {
        crearMarca = false;
    }
}

