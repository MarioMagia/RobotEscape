using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        // Assuming the Button component is on the same GameObject
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        // Debug to check if this method is triggered
        Debug.Log("Button clicked. Trying to play sound.");

        // Play the click sound
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
            Debug.Log("Sound played.");
        }
        else
        {
            Debug.Log("AudioClip or AudioSource is missing or null.");
        }
    }
}
