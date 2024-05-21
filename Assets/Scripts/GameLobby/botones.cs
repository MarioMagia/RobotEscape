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
    [SerializeField] private Canvas canvaAlerta;

    private void Awake()
    {
        botonCrearLobby.onClick.AddListener(()=> {
            canvaAlerta.GetComponent<AlertController>().ShowAlert("Creando Lobby..."); TestLobby.Instance.CreateLobby(); });
        botonUnirLobby.onClick.AddListener(() => { TestLobby.Instance.JoinLobby(codigo.text); }) ;
    }
    public void UnirLobby()
    {
        canvaPreLobby.gameObject.SetActive(false);
        canvaLobby.gameObject.SetActive(true);
    }
    public void FallodeConexion(string mensaje = "Error")
    {
        canvaAlerta.GetComponent<AlertController>().ShowAlert(mensaje);
        canvaPreLobby.gameObject.SetActive(true);
        canvaLobby.gameObject.SetActive(false);
    }
    public void AlertasAcciones(string mensaje = "Esperando")
    {
        canvaAlerta.GetComponent<AlertController>().ShowAlert(mensaje);
    }
}
