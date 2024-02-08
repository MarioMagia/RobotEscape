using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{

    public string Tag = "Player";
    public GameObject FinishText;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag))
        {
            FinishText.SetActive(true);


        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Tag))
        {

            FinishText.SetActive(false);


        }

    }
}

