using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject puertaInvisible;
    public void Abrir()
    {
        animator.SetBool("isOpen", true);
        puertaInvisible.SetActive(false);
    }
    public void Cerrar()
    {
        animator.SetBool("isOpen", false);
        puertaInvisible.SetActive(true);
    }
}
