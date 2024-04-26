using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using System;
using Random = System.Random;
using TMPro;

public class TestLobby : MonoBehaviour
{

    public static TestLobby Instance;
    public Lobby lobbyCreado;
    private Lobby lobbyUnido;
    private float palpito;
    private float actualizacionLobby;
    private string nombreJug = "Player";
    private string empezado;
    [SerializeField] private TMP_Text codeText;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    private async void Start()
    {
        Random random = new Random();
        InitializationOptions initializationOptions = new InitializationOptions();
        initializationOptions.SetProfile("Player"+random.Next(10));

        await UnityServices.InitializeAsync(initializationOptions);

        AuthenticationService.Instance.SignedIn += () => {
            // do nothing
            Debug.Log("Signed in! " + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    private void Update()
    {
        LobbyPalpito();
        LobbyActualizacion();
    }
    public async void CreateLobby(string nombreLobby, int maxJug)
    {
        string nivel = "lvl2";
        empezado = "no";
        try
        {
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = true,
                Player = GetPlayer(),
                Data = new Dictionary<string, DataObject>
                {
                    {"Nivel", new DataObject(DataObject.VisibilityOptions.Public, nivel) },
                    {"Empezado", new DataObject(DataObject.VisibilityOptions.Public, empezado) }
                }
            };
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(nombreLobby, maxJug, createLobbyOptions);
            lobbyCreado = lobby;
            Debug.Log("LOBY CODIGO : " + lobbyCreado.LobbyCode);
            codeText.SetText(lobbyCreado.LobbyCode);
            Debug.Log("HOST CODIGO : " + lobbyCreado.HostId);
            lobbyUnido = lobbyCreado;
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    private async void LobbyPalpito()
    {
        if (lobbyCreado != null)
        {
            palpito -= Time.deltaTime;
            if (palpito < 0F)
            {
                float maxPal = 15;
                palpito = maxPal;

                await LobbyService.Instance.SendHeartbeatPingAsync(lobbyCreado.Id);
            }
        }
    }
    private async void LobbyActualizacion()
    {
        if (lobbyUnido != null)
        {
            
            actualizacionLobby -= Time.deltaTime;
            if (actualizacionLobby < 0F)
            {
                float maxPal = 1.1f;
                actualizacionLobby = maxPal;
                Lobby lobby = await LobbyService.Instance.GetLobbyAsync(lobbyUnido.Id);
                lobbyUnido = lobby;
                if (empezado == "si" && lobbyUnido.HostId != AuthenticationService.Instance.PlayerId)
                {
                    Debug.Log("ERMERESFD");
                    empesar.crearCLient();
                    empezado = "no";
                }
            }
        }
    }
    public async void JoinLobby(string code)
    {
        try
        {
            JoinLobbyByCodeOptions joinLobbyByCode = new JoinLobbyByCodeOptions
            {
                Player = GetPlayer()
            };
            Lobby lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(code);
            lobbyUnido = lobby;
            var callbacks = new LobbyEventCallbacks();
            callbacks.LobbyChanged += OnLobbyChanged;
            await Lobbies.Instance.SubscribeToLobbyEventsAsync(lobbyUnido.Id, callbacks);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }

    private Player GetPlayer()
    {
        return new Player
        {
            Data = new Dictionary<string, PlayerDataObject> {
                        { "NombreJug", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, nombreJug) }
                    }
        };
    }

    public async void ChangeLevel(string nivel)
    {
        try
        {
            var callbacks = new LobbyEventCallbacks();
            callbacks.LobbyChanged += (lobby) =>
            {
                Debug.Log("Lobby changed: " + lobby);
            };
            lobbyCreado = await Lobbies.Instance.UpdateLobbyAsync(lobbyCreado.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject>
            {
                { "Nivel", new DataObject(DataObject.VisibilityOptions.Public, nivel)}
            }
            });
            lobbyUnido = lobbyCreado;
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    public async void ChangePlayerName(string nuevoNombre)
    {
        try
        {
            await LobbyService.Instance.UpdatePlayerAsync(lobbyUnido.Id, AuthenticationService.Instance.PlayerId, new UpdatePlayerOptions
            {
                Data = new Dictionary<string, PlayerDataObject>
            {
                {"Nombre", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, nuevoNombre) }
            }
            });
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    public async void LeaveLobby()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(lobbyUnido.Id, AuthenticationService.Instance.PlayerId);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    private void OnLobbyChanged(ILobbyChanges changes)
    {
        Debug.Log("Lobby changed");
        if (changes.Data.Changed)
        {
            empesar.crearCLient();
        }

    }
    public void setEmpezar()
    {
        empezado = "si";
    }
    public async void Empezado()
    {
        empezado = "si";
        try
        {
            lobbyUnido = await Lobbies.Instance.UpdateLobbyAsync(lobbyUnido.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject>
            {
                { "Empezado", new DataObject(DataObject.VisibilityOptions.Public, empezado)}
            }
            });
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    public async void ChangeHost()
    {
        try
        {
            lobbyCreado = await Lobbies.Instance.UpdateLobbyAsync(lobbyCreado.Id, new UpdateLobbyOptions
            {
                HostId = lobbyUnido.Players[0].Id
            });
            lobbyUnido = lobbyCreado;
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}
