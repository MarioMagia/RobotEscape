using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;



namespace StarterAssets
{
    public class Teleport : NetworkBehaviour
    {

        public GameObject marcaPrefab;
        public static List<Marca> marks = new List<Marca>();
        public static List<MarcaObject> markObjects = new List<MarcaObject>();


        public void CrearMarca()
        {
            // Obtén la posición del jugador
            Vector3 posicionJugador = transform.position;
            // Crea la "Marca" en la posición del 
            if (HasMarks())
            {
                BorrarMarca(true);
            }
            SpawnMarkServerRpc(posicionJugador);
        }

        public void BorrarMarca(bool cual)
        {
            Marca? mark = cual ? SearchForCreatorMark() : SearchForOwnerMark(false);
            if (mark != null)
            {
                DespawnMarkServerRpc(mark.Value);
                RemoveMarkClientRpc(mark.Value);
            }
        }

        public void TakeMark(Marca mark)
        {
            for (int i = 0; i < marks.Count; i++)
            {
                if (marks[i].Equals(mark))
                {
                    ChangeMarkObjectRpc(new Marca(marks[i].posicion, marks[i].JugadorCreador, NetworkManager.LocalClientId), marks[i]);
                    ChangeMarkClientRpc(new Marca(marks[i].posicion, marks[i].JugadorCreador, NetworkManager.LocalClientId), marks[i]);
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
    
        public Marca? SearchForOwnerMark(bool IgualCreador)
        {
            foreach (Marca marca in marks)
            {
                if (marca.JugadorAsociado == NetworkManager.LocalClientId && (IgualCreador ? marca.JugadorCreador == NetworkManager.LocalClientId : marca.JugadorCreador != NetworkManager.LocalClientId))
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

            GameObject nuevaMarca = Instantiate(marcaPrefab, posicionJugador, Quaternion.identity);
            nuevaMarca.GetComponent<NetworkObject>().Spawn(true);
            Marca mark = new Marca(nuevaMarca.transform.position, rpcParams.Receive.SenderClientId, rpcParams.Receive.SenderClientId);
            markObjects.Add(new MarcaObject(nuevaMarca, mark));
            AddNewMarkClientRpc(mark);
        }

        [Rpc(SendTo.Everyone)]
        public void AddNewMarkClientRpc(Marca mark)
        {
            marks.Add(mark);
        }

        [Rpc(SendTo.Everyone)]
        public void ChangeMarkClientRpc(Marca NewMark, Marca OldMark)
        {
            int index = marks.FindIndex(x => x.Equals(OldMark));
            marks[index] = NewMark;
        }

        [Rpc(SendTo.Server)]
        public void ChangeMarkObjectRpc(Marca NewMark, Marca OldMark)
        {
            int index = markObjects.FindIndex(x => x.marca.Equals(OldMark));
            markObjects[index].marca = NewMark;
        }

        [Rpc(SendTo.Everyone)]
        public void RemoveMarkClientRpc(Marca mark)
        {
            marks.Remove(mark);
        }

        [Rpc(SendTo.Server)]
        public void DespawnMarkServerRpc(Marca mark)
        {
            MarcaObject markToRemove = null;

            foreach (MarcaObject marcaObject in markObjects)
            {
                if (marcaObject.marca.Equals(mark))
                {
                    markToRemove = marcaObject;
                    // Verificar si el GameObject asociado a la marca aún existe
                    if (marcaObject.objeto != null)
                    {
                        // Acceder al componente NetworkObject y despawnearlo
                        NetworkObject networkObject = marcaObject.objeto.GetComponent<NetworkObject>();
                        if (networkObject != null)
                        {
                            networkObject.Despawn(true);
                        }
                    }
                    break;
                }
            }

            // Verificar si se encontró un objeto para eliminar
            if (markToRemove != null)
            {
                markObjects.Remove(markToRemove);
            }
        }




        public Vector3 GivePos(bool cual)
        {

            Marca? mark = SearchForOwnerMark(cual);
            if (mark != null)
            {
                return mark.Value.posicion;
            }
            else
            {
                return Vector3.zero;
            }
        }
        public Marca GetMarca(Vector3 gameObject)
        {
            foreach(Marca marca in marks)
            {
                if (marca.posicion == (gameObject))
                {
                    return marca;
                }
            }
            return new Marca(Vector3.zero, unchecked((ulong)-1), unchecked((ulong)-1));
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