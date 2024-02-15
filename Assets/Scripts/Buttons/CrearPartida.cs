using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrearPartida : MonoBehaviour
{
    [SerializeField]
    private Button btn;
    private LobbyManager lobby;
    public void setupBtn()
    {
        string param = "bar";
        btn.GetComponent<Button>().onClick.AddListener(delegate { CrearLobby(param,2); });
    }

    public void CrearLobby(string name, int maxJug)
    {
        lobby.CreateLobby(name, maxJug, true, LobbyManager.Level.lvl1);
    }
    private void Awake()
    {
        lobby = GetComponent<LobbyManager>();
    }

}
