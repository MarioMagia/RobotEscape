using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject puertaInvisible;
    public void Abrir()
    {
        animator.Play("DoorOpen", 0, 0.0f);
        puertaInvisible.SetActive(false);
    }
    public void Cerrar()
    {
        animator.Play("DoorClose", 0, 0.0f);
        puertaInvisible.SetActive(true);
    }
}
