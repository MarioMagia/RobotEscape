using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectMarks : MonoBehaviour
{
    public GameObject Detector;
    public float alturaObjeto = 0.1f;  // Altura a la que se mantiene el objeto respecto a la mano


    private GameObject Mark = null;
    public CharacterController characterController;

    void Update()

    {

        if (Mark != null)
        {
            Mark.transform.rotation = Quaternion.Euler(Vector3.zero);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Marca"))
        {
            if (Keyboard.current.eKey.isPressed && Mark == null)
            {
                CogerObjeto(other.gameObject);
            }
        }
    }

    private void CogerObjeto(GameObject objeto)
    {
        objeto.GetComponent<Rigidbody>().useGravity = false;
        objeto.GetComponent<Rigidbody>().isKinematic = false;
        objeto.GetComponent<Collider>().isTrigger = false;
        //Configura el collider como trigger para evitar colisiones mientras está en la mano

        //objeto.transform.position = manoCoger.transform.position + Vector3.up * alturaObjeto;
        objeto.GetComponent<Collider>().enabled = true;
        objeto.transform.SetParent(Detector.gameObject.transform);
        Mark = objeto;
    }

}
