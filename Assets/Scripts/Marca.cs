using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public struct Marca : INetworkSerializeByMemcpy
{


    public Vector3 posicion { get; private set; }
    public ulong JugadorCreador { get; private set; }
    public ulong JugadorAsociado { get; private set; }
     public Marca( Vector3 position, ulong Jugador1, ulong Jugador2)
    {
        posicion = position;
        JugadorCreador = Jugador1;
        JugadorAsociado = Jugador2;
    }


}
