using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
