using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class botones : MonoBehaviour
{

    [SerializeField]private Button botonCrearLobby;
    [SerializeField]private Button botonUnirLobby;
    [SerializeField] private TMP_InputField codigo;

    private void Awake()
    {
        botonCrearLobby.onClick.AddListener(()=> { TestLobby.Instance.CreateLobby("Lobby", 2); });
        botonUnirLobby.onClick.AddListener(() => { TestLobby.Instance.JoinLobby(codigo.text); }) ;
    }
}
