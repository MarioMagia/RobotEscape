using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerPlate : MonoBehaviour
{
    public PressAllPlatesScript platesScript;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            platesScript.triggerActivated(this.gameObject);
        }
    }
}
