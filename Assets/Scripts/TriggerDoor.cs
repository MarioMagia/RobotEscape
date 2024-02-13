using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TriggerDoor : MonoBehaviour
{
    [SerializeField] private string Tag = "Player";
    [SerializeField] private GameObject puerta;
    [SerializeField] private bool CollisionActivation;


    private void OnTriggerEnter(Collider other)
    {
        if (CollisionActivation) return;
        if (other.gameObject.CompareTag(Tag))
        {
            puerta.GetComponent<DoorController>().Abrir();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!CollisionActivation) return; 
        if (collision.gameObject.CompareTag(Tag))
        {
            puerta.GetComponent<DoorController>().Abrir();
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (!CollisionActivation) return;
        if (collision.gameObject.CompareTag(Tag))
        {
            puerta.GetComponent<DoorController>().Abrir();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (CollisionActivation) return;
        if (other.gameObject.CompareTag(Tag))
        {
            puerta.GetComponent<DoorController>().Cerrar();
        }
    }

}

    
        

