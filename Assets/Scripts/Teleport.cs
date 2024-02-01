using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Teleport : MonoBehaviour
{

    public GameObject Marca;
    

  
    void Update()
    {
        if (Keyboard.current.mKey.isPressed)
        {
            CrearMarca();


        }
    }


    void CrearMarca()
    {
        // Obt�n la posici�n del jugador
        Vector3 posicionJugador = transform.position;

        // Ajusta la posici�n del objeto "Marca" para que sea m�s visible (ajusta seg�n tus necesidades)
        posicionJugador += new Vector3(0, 1, 0); // Ajusta la altura si es necesario

        // Crea la "Marca" en la posici�n del jugador
        GameObject nuevaMarca = Instantiate(Marca, posicionJugador, Quaternion.identity);

        // Aseg�rate de que la instancia tenga la misma escala que el prefab
        nuevaMarca.transform.localScale = Marca.transform.localScale;

    }
}

