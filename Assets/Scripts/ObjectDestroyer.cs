using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {
            collision.gameObject.GetComponent<ServerObjectPhysics>().ResetSpawnPoint();
        }
    }
}
