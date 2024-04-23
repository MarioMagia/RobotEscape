using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] private GameObject keyboardControlsPanel;
    [SerializeField] private GameObject gamepadControlsPanel;
    [SerializeField] private TMP_Dropdown controlsDropdown;
    // Start is called before the first frame update
    void Start()
    {
        Control();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Control()
    {
        // Coge el valor que se ha escogido del dropdown de control(Keyboard = 0, Gamepad = 1)
        controlsDropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(controlsDropdown);
        });
        keyboardControlsPanel.SetActive(true); //Al iniciar muestra los controles de keyboard
        gamepadControlsPanel.SetActive(false);
        controlsDropdown.value = 0;
        controlsDropdown.Select();//Muestra el tipo de control que se ha selecionado

    }

    // Coge el valor que se ha escogido del dropdown de control y muestra el selecionado
    void DropdownValueChanged(TMP_Dropdown change)
    {
        if (change.value == 0) // Keyboard Controls selected
        {
            gamepadControlsPanel.SetActive(false);
            keyboardControlsPanel.SetActive(true);
        }
        else if (change.value == 1) // Gamepad Controls selected
        {
            keyboardControlsPanel.SetActive(false);
            gamepadControlsPanel.SetActive(true);
        }
    }


}
