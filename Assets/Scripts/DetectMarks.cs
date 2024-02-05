using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectMarks : MonoBehaviour
{
    public GameObject Detector;
    public float alturaObjeto = 0.1f;  // Altura a la que se mantiene el objeto respecto a la mano

    public CharacterController characterController;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Teleport"))
        {
            
        }
    }

}
