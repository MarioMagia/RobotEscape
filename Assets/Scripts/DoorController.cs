using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject puertaInvisible;
    public bool haspuertainvisible = true;
    public void Abrir()
    {
        animator.SetBool("isOpen", true);
        if (haspuertainvisible) {
            puertaInvisible.SetActive(false);
        }
    }
    public void Cerrar()
    {
        animator.SetBool("isOpen", false);
        if (haspuertainvisible)
        {
            puertaInvisible.SetActive(true);
        }
    }
}
