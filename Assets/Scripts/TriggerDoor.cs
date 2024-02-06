using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TriggerDoor : MonoBehaviour
{

    [SerializeField] private Animator MoveDoor = null;
    [SerializeField] private string DoorOpen = "DoorOpen";
    [SerializeField] private string DoorClose = "DoorClose";
    [SerializeField] private string Tag = "Player";

    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Tag))
        {
            Debug.Log("Detecta Stone");
            MoveDoor.Play(DoorOpen, 0, 0.0f);
        }

    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag(Tag))
        {
            Debug.Log("Detecta sale la Stone");
            MoveDoor.Play(DoorClose, 0, 0.0f);
        }
    }

}

    
        

