using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowControls : MonoBehaviour

{ 
   
    
    public GameObject textParent;
    public GameObject textParent1;
    public GameObject textParent2;
    public GameObject textParent3;

    

    private void OnTriggerEnter(Collider other)
    {
        

        if (CompareTag("Checkpoint"))
        {
            
            if (textParent.activeSelf) {
                textParent.SetActive(false);
                textParent1.SetActive(true);
            }

            else {
                textParent.SetActive(true);
                textParent1.SetActive(false);
            }

        }
        else if (CompareTag("Checkpoint1"))
        {
            if (textParent1.activeSelf)
            {
                textParent1.SetActive(false);
                textParent2.SetActive(true);
            }
            else
            {
                textParent1.SetActive(true);
                textParent2.SetActive(false);
            }

        }
        else if (CompareTag("Checkpoint2"))
        {
            if (textParent2.activeSelf)
            {
                textParent2.SetActive(false);
                textParent3.SetActive(true);
            }
            else {
                textParent2.SetActive(true);
                textParent3.SetActive(false);
            }

        }
    }
}
