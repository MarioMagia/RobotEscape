using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFont : MonoBehaviour
{
   
    [SerializeField] private Font nuevaFontText;
    [SerializeField] private TMP_FontAsset nuevaFontTextMesh;



    void Start()
    {
        // Encuentra todos los objetos en la escena que tienen un componente de TextMeshProUGUI(aunque esten inactivos)
        TextMeshProUGUI[] textMeshPros = FindObjectsOfType<TextMeshProUGUI>(true);


        // Recorre todos los objetos de TextMeshProUGUI encontrados y cambia su fuente
        foreach (TextMeshProUGUI textMeshPro in textMeshPros)
        {
            // Asigna la nueva fuente
            textMeshPro.font = nuevaFontTextMesh;
        }

        // Encuentra todos los objetos en la escena que tienen un componente de texto(aunque esten inactivos)
        Text[] textos = FindObjectsOfType<Text>(true);

        // Recorre todos los objetos de texto encontrados y cambia su tipo de texto
        foreach (Text texto in textos)
        {
            // Asigna el nuevo tipo de texto
            texto.font = nuevaFontText;
        }       

    }

   
}
