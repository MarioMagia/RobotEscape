using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionCajaMovil : MonoBehaviour

    
{
    public CharacterController characterController;
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        characterController.Move(Vector3.zero);
    }

}
