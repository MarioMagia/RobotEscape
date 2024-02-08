using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class CogerObjetos : NetworkBehaviour
{
    public GameObject manoCoger;
    public float alturaObjeto = 1.8f;  // Altura a la que se mantiene el objeto respecto a la mano
    public float distancia = 1.5f;  // Altura a la que se mantiene el objeto respecto a la mano


    private GameObject objetoCogido = null;
    public CharacterController characterController;

    void Update()

    {
      
        if (objetoCogido != null)        {
           

            // Calcula la posici�n del objeto para colocarlo m�s adelante del personaje
            Vector3 nuevaPosicion = manoCoger.transform.position + manoCoger.transform.forward * distancia + Vector3.up * alturaObjeto;

            // Asigna la nueva posici�n al objeto
            objetoCogido.transform.position = nuevaPosicion;

          

            // Evita que el objeto rote con la c�mara
            objetoCogido.transform.rotation = Quaternion.Euler(Vector3.zero);

        }

        if (Keyboard.current.vKey.isPressed && objetoCogido != null)
        {
            LiberarObjeto();
        }

        // Verifica si hay una colisi�n en la direcci�n de movimiento del personaje
        /*if (HayColisionEnDireccion())
        {
            // Si hay colisi�n, impide que el personaje avance en esa direcci�n
            characterController.Move(Vector3.zero);
        }*/
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("ObjetoMovil") || other.gameObject.CompareTag("Stone"))
        {
            if (Keyboard.current.cKey.isPressed && objetoCogido == null)
            {
                CogerObjeto(other.gameObject);
            }
        }
    }

    private void CogerObjeto(GameObject objeto)
    {
        objeto.GetComponent<Rigidbody>().useGravity = false;
        objeto.GetComponent<Rigidbody>().isKinematic = false;
        objeto.GetComponent<Collider>().isTrigger = true;
        //Configura el collider como trigger para evitar colisiones mientras est� en la mano

        //objeto.transform.position = manoCoger.transform.position + Vector3.up * alturaObjeto;
        objeto.GetComponent<Collider>().enabled = true;
        setObjectParentRpc(objeto, manoCoger.gameObject);  
        objetoCogido = objeto;
    }

    [Rpc(SendTo.Server)]
    private void setObjectParentRpc(GameObject objeto, GameObject parent)
    {
        objeto.transform.SetParent(parent.transform);
    }
    private void LiberarObjeto()
    {
        objetoCogido.GetComponent<Rigidbody>().useGravity = true;
        //objetoCogido.GetComponent<Rigidbody>().isKinematic = false;        
        objetoCogido.GetComponent<Collider>().isTrigger = false;
        setObjectParentRpc(objetoCogido.gameObject, null);
        objetoCogido = null;
    }

    /*Comprueba si el objeto cogido colisiona con algo*/
    private bool HayColisionEnDireccion()
    {
        // Verificar si el componente CharacterController est� presente
        if (characterController == null || objetoCogido == null)
        {
            Debug.LogWarning("No se encontr� el componente CharacterController o el objetoCogido.");
            return false;
        }

        // Obt�n la posici�n y direcci�n de movimiento del personaje
        Vector3 posicionPersonaje = manoCoger.transform.position;
        Vector3 direccionMovimiento = transform.forward;

        // Lanza un rayo en la direcci�n de movimiento y verifica si hay colisi�n
        RaycastHit[] hits = Physics.RaycastAll(posicionPersonaje, direccionMovimiento, Mathf.Infinity);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject != objetoCogido)
            {
                return true;
            }
        }

        // Si no hay colisi�n o la colisi�n es con el objetoCogido, devuelve false
        return false;
    }

   

}
