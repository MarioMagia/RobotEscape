using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowControls : MonoBehaviour
{

    
    public GameObject playerHUD;    
    
    private GameObject textParent;
    private GameObject textParent1;
    private GameObject textParent2;
    private GameObject textParent3;

    public void Start()
    {
       textParent = playerHUD.transform.Find("textParent").gameObject;
       textParent1 = playerHUD.transform.Find("textParent1").gameObject;
       textParent2 = playerHUD.transform.Find("textParent1").gameObject;
       textParent3 = playerHUD.transform.Find("textParent1").gameObject;
}

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
