using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;



namespace StarterAssets
{
    public class Teleport : NetworkBehaviour
    {

        public GameObject marca;
        public bool crearMarca = false;
        public Vector3 posicionDeLaMarca;
        public List<Marca> marks = new List<Marca>();
        public List<MarcaObject> markObjects = new List<MarcaObject>();
        public GameObject nuevaMarca;
        public NetworkObject currentMark;


        public void CrearMarca()
        {
            // Obtén la posición del jugador
            Vector3 posicionJugador = transform.position;
            // Crea la "Marca" en la posición del 
            if (HasMarks())
            {
                BorrarMarca();
            }
            SpawnMarkServerRpc(posicionJugador);

            // Guarda la posición de la marca en la variable
        }

        public void BorrarMarca()
        {
            Marca? mark = SearchForCreatorMark();
            if (mark != null)
            {
                DespawnMarkServerRpc(mark.Value);
                RemoveMarkRpc(mark.Value);
            }
        }

        public void TakeMark(Marca mark)
        {
            for (int i = 0; i < marks.Count; i++)
            {
                if (marks[i].Equals(mark))
                {
                    ChangeMarkRpc(new Marca(marks[i].posicion, marks[i].JugadorCreador, NetworkManager.LocalClientId), marks[i]);
                }
            }
        }

        public Marca? SearchForCreatorMark()
        {
            foreach (Marca marca in marks)
            {
                if (marca.JugadorCreador == NetworkManager.LocalClientId)
                {
                    return marca;
                }
            }
            return null;
        }

        public Marca? SearchForOwnerMark()
        {
            foreach (Marca marca in marks)
            {
                if (marca.JugadorAsociado == NetworkManager.LocalClientId)
                {
                    return marca;

                }
            }
            return null;
        }   

        public bool HasMarks()
        {
            
            return SearchForCreatorMark() != null ? true : false;
        }

        [Rpc(SendTo.Server)]
        public void SpawnMarkServerRpc(Vector3 posicionJugador, RpcParams rpcParams = default)
        {
            // Crea la "Marca" en la posición del jugador
            
            nuevaMarca = Instantiate(marca, posicionJugador, Quaternion.identity);
            nuevaMarca.GetComponent<NetworkObject>().Spawn(true);
            //posicionDeLaMarca = nuevaMarca.transform.position;
            Marca mark = new Marca(nuevaMarca.transform.position, rpcParams.Receive.SenderClientId, rpcParams.Receive.SenderClientId);
            markObjects.Add(new MarcaObject(nuevaMarca, mark));
            AddNewMarkRpc(mark);
        }

        [Rpc(SendTo.Everyone)]
        public void AddNewMarkRpc(Marca mark)
        {
            marks.Add(mark);
        }

        [Rpc(SendTo.Everyone)]
        public void ChangeMarkRpc(Marca NewMark, Marca OldMark)
        {
            for(int i = 0; i < marks.Count; i++)
            {
                if (marks[i].Equals(OldMark))
                {
                    marks[i] = NewMark;
                }
            }
        }

        [Rpc(SendTo.Everyone)]
        public void RemoveMarkRpc(Marca mark)
        {
            marks.Remove(mark);
        }

        [Rpc(SendTo.Server)]
        public void DespawnMarkServerRpc(Marca mark, RpcParams serverRpcParams = default)
        {
            MarcaObject marktoremove = null;
            foreach (MarcaObject marcaObject in markObjects)
            {
                if (marcaObject.marca.Equals(mark))
                {
                    marktoremove = marcaObject;
                    marcaObject.objeto.GetComponent<NetworkObject>().Despawn(true);
                }
            }
            if (marktoremove != null) { 

                markObjects.Remove(marktoremove);
            }
            // Llama a CambiarMarca después de un pequeño retraso (0.1 segundos)

        }

        public Vector3 GivePos()
        {

            Marca? mark = SearchForOwnerMark();
            if (mark != null)
            {
                return mark.Value.posicion;
            }
            else
            {
                return Vector3.zero;
            }
        }
    }

}
public class MarcaObject
{
    public GameObject objeto;
    public Marca marca;
    public MarcaObject(GameObject obj, Marca mark)
    {
        objeto = obj;
        marca = mark;
    }
}