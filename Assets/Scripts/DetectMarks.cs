using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectMarks : MonoBehaviour
{
    private Teleport tp;
    private StarterAssetsInputs inputs;

    private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Teleport"))
            {
                if (inputs.takemark)            {
                    inputs.takemark = false;
                    Marca marca = tp.GetMarca(other.gameObject.transform.position);
                    tp.TakeMark(marca);
                }
            }
        }
    public void Awake()
    {
        inputs = GetComponent<StarterAssetsInputs>();
        tp = GetComponent<Teleport>();
    }

}
