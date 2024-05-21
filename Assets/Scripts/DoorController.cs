using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject puertaInvisible;
    AudioSource audioSource;
    public bool haspuertainvisible = true;

    [SerializeField] private AudioClip sonido;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }
    public void Abrir()
    {
        animator.SetBool("isOpen", true);
        PlaySoundAtSpeed(1.3f);
        if (haspuertainvisible)
        {
            puertaInvisible.SetActive(false);
        }
    }
    public void Cerrar()
    {
        animator.SetBool("isOpen", false);
        PlaySoundAtSpeed(1.3f);
        if (haspuertainvisible)
        {
            puertaInvisible.SetActive(true);
        }
    }

    private void PlaySoundAtSpeed(float speed)
    {
        audioSource.clip = sonido;
        audioSource.pitch = speed;
        audioSource.volume = 0.3f;
        audioSource.Play();
    }
}
