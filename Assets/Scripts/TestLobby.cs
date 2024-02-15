using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using System;

public class TestLobby : MonoBehaviour
{
    private Lobby lobbyCreado;
    private Lobby lobbyUnido;
    private float palpito;
    private float actualizacionLobby;
    public string nombreJug;

    // Start is called before the first frame update
    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log(AuthenticationService.Instance.PlayerId);
        };
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    private void Update()
    {
        LobbyPalpito();
        LobbyActualizacion();
    }
    public async void CreateLobby(string nombreLobby, int maxJug, string nivel)
    {
        try
        {
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = true,
                Player = GetPlayer(),
                Data = new Dictionary<string, DataObject>
                {
                    {"Nivel", new DataObject(DataObject.VisibilityOptions.Public, nivel) }
                }
            };
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(nombreLobby, maxJug, createLobbyOptions);
            lobbyCreado = lobby;
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

                Lobby lobby = await LobbyService.Instance.GetLobbyAsync(lobbyCreado.Id);
                lobbyUnido = lobby;
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
