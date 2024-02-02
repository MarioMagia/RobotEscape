using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;



namespace StarterAssets
{
    public class Teleport : NetworkBehaviour
    {

        public GameObject Marca;
        public bool crearMarca = false;
        public Vector3 posicionDeLaMarca;
        public ArrayList marks = new ArrayList();
        public GameObject nuevaMarca;


        public GameObject CrearMarca()
        {
            // Obtén la posición del jugador
            Vector3 posicionJugador = transform.position;
            // Crea la "Marca" en la posición del 
            if (!HasMarksServerRpc())
            {
                MarcarServerRpc(posicionJugador);
            }
            else
            {
                nuevaMarca = GetMarksServerRpc();
            }
            return nuevaMarca;

            // Guarda la posición de la marca en la variable
        }

        [ServerRpc]
        public bool HasMarksServerRpc(ServerRpcParams serverRpcParams = default)
        {
            foreach (Marca marca in marks)
            {
                if (marca.JugadorCreador == serverRpcParams.Receive.SenderClientId)
                {
                    return true;
                }
            }
            return false;
        }
        [ServerRpc]
        public GameObject GetMarksServerRpc(ServerRpcParams serverRpcParams = default)
        {
            foreach (Marca marca in marks)
            {
                if (marca.JugadorCreador == serverRpcParams.Receive.SenderClientId)
                {
                    return marca.objeto;
                }
            }
            return null;
        }

        [ServerRpc]
        public void MarcarServerRpc(Vector3 posicionJugador, ServerRpcParams  serverRpcParams = default)
        {
            // Crea la "Marca" en la posición del jugador
            
            nuevaMarca = Instantiate(Marca, posicionJugador, Quaternion.identity);
            nuevaMarca.GetComponent<NetworkObject>().Spawn(true);
            marks.Add(new Marca(nuevaMarca, serverRpcParams.Receive.SenderClientId, serverRpcParams.Receive.SenderClientId));
            //posicionDeLaMarca = nuevaMarca.transform.position;
        }
        

        [ServerRpc]
        public void DespawnMarkServerRpc(ServerRpcParams serverRpcParams = default)
        {
            crearMarca = false;
            // Teleporta al jugador a la posición almacenada en posicionDeLaMarca
            foreach (Marca marca in marks) {
                if(marca.JugadorCreador == serverRpcParams.Receive.SenderClientId)
                {
                    marca.objeto.GetComponent<NetworkObject>().Despawn(true);
                }
            }
            // Llama a CambiarMarca después de un pequeño retraso (0.1 segundos)

        }
        [ServerRpc]
        public Vector3 GivePosServerRpc(ServerRpcParams serverRpcParams = default)
        {
            
            foreach (Marca marca in marks)
            {
                if (marca.JugadorAsociado == serverRpcParams.Receive.SenderClientId)
                {
                    return marca.objeto.transform.position;
                }
            }
            return new Vector3(0,0,0);
        }
    }

}