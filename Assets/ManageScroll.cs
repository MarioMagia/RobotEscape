using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManageScroll : MonoBehaviour
{
    public GameObject cellPrefab;
    // Start is called before the first frame update
    void Start()
    {
        string[] controlNames = GetControlNames();


        // Create UI elements for each control
        foreach (string controlName in controlNames)
        {
            GameObject obj = Instantiate(cellPrefab);
            obj.transform.SetParent(this.gameObject.transform, false);
            obj.transform.GetChild(0).GetComponent<TMP_Text>().text = controlName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string[] GetControlNames()
    {
        // Example array of control names
        return new string[] { "Forward", "Left", "Backwards", "Right", "Jump", "Sprint", "Pick Up", "Crouch", "Mark", "Teleport 1", "Teleport 2", "Pause Menu" };
    }
}
