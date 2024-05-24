using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonPulsado : MonoBehaviour
{
    // Referencia al componente Toggle
    private Toggle toggle;

    // Color cuando el bot�n est� activado
    public Color colorActivado;

    // Color por defecto cuando el bot�n est� desactivado
    private Color colorPorDefecto;

    void Start()
    {
        // Obtener la referencia al componente Toggle
        toggle = GetComponent<Toggle>();

        // Guardar el color por defecto del bot�n
        colorPorDefecto = GetComponent<Image>().color;
    }

    void Update()
    {
        // Verificar si el bot�n est� activado
        if (toggle.isOn)
        {
            // Cambiar el color del bot�n al color activado
            GetComponent<Image>().color = colorActivado;
        }
        else
        {
            // Restaurar el color por defecto del bot�n
            GetComponent<Image>().color = colorPorDefecto;
        }
    }
}
