using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject puertaInvisible;
    AudioSource audioSource;

    [SerializeField] private AudioClip sonido;


    private void Awake()
    {
        audioSource.GetComponent<AudioSource> ();
    }
    public void Abrir()
    {
        animator.SetBool("isOpen", true);
        audioSource.PlayOneShot(sonido);
        puertaInvisible.SetActive(false);
    }
    public void Cerrar()
    {
        animator.SetBool("isOpen", false);
        audioSource.PlayOneShot(sonido);
        puertaInvisible.SetActive(true);
    }
}
