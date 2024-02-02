using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Marca
{
    public GameObject objeto { get; private set; }
    public ulong JugadorCreador { get; private set; }
    public ulong JugadorAsociado { get; private set; }
     public Marca(GameObject objeto, ulong Jugador1, ulong Jugador2)
    {
        this.objeto = objeto;
        JugadorCreador = Jugador1;
        JugadorAsociado = Jugador2;
    }


}
