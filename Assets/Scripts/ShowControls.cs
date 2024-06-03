using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowControls : MonoBehaviour

{ 
   
    
    public GameObject textParent;

    

    private void OnTriggerEnter(Collider other)
    {
        if(other != null && other.CompareTag("Player"))
        {
            textParent.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            textParent.SetActive(false);
        }
    }
}
