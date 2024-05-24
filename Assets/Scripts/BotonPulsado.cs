using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonPulsado : MonoBehaviour
{
    // Referencia al componente Toggle
    private Toggle toggle;

    // Color cuando el botón está activado
    public Color colorActivado;

    // Color por defecto cuando el botón está desactivado
    private Color colorPorDefecto;

    void Start()
    {
        // Obtener la referencia al componente Toggle
        toggle = GetComponent<Toggle>();

        // Guardar el color por defecto del botón
        colorPorDefecto = GetComponent<Image>().color;
    }

    void Update()
    {
        // Verificar si el botón está activado
        if (toggle.isOn)
        {
            // Cambiar el color del botón al color activado
            GetComponent<Image>().color = colorActivado;
        }
        else
        {
            // Restaurar el color por defecto del botón
            GetComponent<Image>().color = colorPorDefecto;
        }
    }
}
