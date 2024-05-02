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
    [SerializeField] private Canvas canvaPreLobby;
    [SerializeField] private Canvas canvaLobby;

    private void Awake()
    {
        botonCrearLobby.onClick.AddListener(()=> { TestLobby.Instance.CreateLobby("Lobby", 2); });
        botonUnirLobby.onClick.AddListener(() => { TestLobby.Instance.JoinLobby(codigo.text); }) ;
    }
    public void UnirLobby()
    {
        canvaPreLobby.gameObject.SetActive(false);
        canvaLobby.gameObject.SetActive(true);
    }
    public void FallodeConexion()
    {
        canvaPreLobby.gameObject.SetActive(true);
        canvaLobby.gameObject.SetActive(false);
    }
}
