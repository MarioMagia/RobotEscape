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
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using Unity.Networking.Transport.Relay;

using Unity.Services.Relay;

#if UNITY_EDITOR
using ParrelSync;
#endif
using System.Text;

public class TestLobby : MonoBehaviour
{

    public static TestLobby Instance;
    public Lobby lobbyCreado;
    private Lobby lobbyUnido;
    private float palpito;
    private float actualizacionLobby;
    private string nombreJug;
    private bool ready;
    [SerializeField] private TMP_Text codeText;
    [SerializeField] private TMP_Dropdown level_selection;
    [SerializeField] private TMP_Dropdown mode_selection;
    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text mode;
    [SerializeField] private TMP_Text player1;
    [SerializeField] private TMP_Text player2;
    [SerializeField] private Canvas canvaConnection;
    [SerializeField] private Canvas canvaPreLobby;
    [SerializeField] private Canvas canvaLobby;
    botones botones;
    // Start is called before the first frame update
    private void Awake()
    {
        botones = FindObjectOfType<botones>();
        Instance = this;
    }
    private async void Start()
    {
        Random random = new Random();
        InitializationOptions initializationOptions = new InitializationOptions();
#if UNITY_EDITOR
        initializationOptions.SetProfile(GetCloneNameEnd());
#else
        initializationOptions.SetProfile(PlayerPrefs.GetString("Username"));
#endif
        await UnityServices.InitializeAsync(initializationOptions);

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            AuthenticationService.Instance.SignedIn += () =>
        {
            // do nothing
            Debug.Log("Signed in! " + AuthenticationService.Instance.PlayerId);
        };
            AuthenticationService.Instance.SignInFailed += async (err) =>
            {
                canvaConnection.gameObject.GetComponentInChildren<TMP_Text>().SetText("Failed to connect, trying again...");
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            };

            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        if(AuthenticationService.Instance.IsSignedIn)
        {
            canvaConnection.gameObject.SetActive(false);
            canvaPreLobby.gameObject.SetActive(true);
        }   
        
        botones = FindAnyObjectByType<botones>();
        string name = PlayerPrefs.GetString("Username", NameGenerator.GetName(AuthenticationService.Instance.PlayerId));
        nombreJug = name;
    }
#if UNITY_EDITOR
    static string GetCloneNameEnd()
    {
        //The code below makes it possible for the clone instance to log in as a different user profile in Authentication service.
        //This allows us to test services integration locally by utilising Parrelsync.
        if (ClonesManager.IsClone())
        {
            var cloneName = ClonesManager.GetCurrentProject().name;
            var lastUnderscoreIndex = cloneName.LastIndexOf("_"); // Get the last occurrence of "_" in the string
            var numberStr =
                cloneName.Substring(lastUnderscoreIndex +
                    1); // Extract the substring that follows the last occurrence of "_"
            Debug.Log("NumberStr: " + numberStr);
            return numberStr;
        }
        else
        {
            return "original";
        }
    }
#endif
    private void Update()
    {
        LobbyPalpito();
        LobbyActualizacion();
    }
    public async void CreateLobby()
    {
        string nombreLobby = "Lobby";
        int maxJug = 2;
        string nivel = "LevelTutorial";
        try
        {
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = true,
                Player = GetPlayer(true),
                Data = new Dictionary<string, DataObject>
                {
                    {"Nivel", new DataObject(DataObject.VisibilityOptions.Member, nivel) },
                    {"Empezado", new DataObject(DataObject.VisibilityOptions.Member, "no") },
                    {"Mode", new DataObject(DataObject.VisibilityOptions.Member, "history") },
                    {"RCode", new DataObject(DataObject.VisibilityOptions.Member, "") }
                }
            };
            player1.SetText(nombreJug);
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(nombreLobby, maxJug, createLobbyOptions);
            var callbacks = new LobbyEventCallbacks();
            callbacks.PlayerDataChanged += onPlayerDataChange;
            callbacks.PlayerLeft += onPlayerLeft;
            callbacks.PlayerJoined += onPlayerJoined;
            await Lobbies.Instance.SubscribeToLobbyEventsAsync(lobby.Id, callbacks);
            lobbyCreado = lobby;


            var allocation = await Relay.Instance.CreateAllocationAsync(2);
            // Obtener el join code del Relay
            var joinCode = await Relay.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log("Relay join code: " + joinCode);

            // A�adir los datos del Relay al lobby
            await Lobbies.Instance.UpdateLobbyAsync(lobbyCreado.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject>
                {
                    { "RCode", new DataObject(DataObject.VisibilityOptions.Member, joinCode) }
                }
            });

            Debug.Log("Relay join code added to lobby data");

            // Configurar el servidor con Relay en NGO
            var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetRelayServerData(new RelayServerData(allocation, "dtls"));

            // Iniciar el servidor con NGO
            NetworkManager.Singleton.StartHost();

            Debug.Log("LOBY CODIGO : " + lobbyCreado.LobbyCode);
            codeText.SetText(lobbyCreado.LobbyCode);
            Debug.Log("HOST CODIGO : " + lobbyCreado.HostId);
            lobbyUnido = lobbyCreado;
            if (botones != null)
            {
                botones.UnirLobby();
            }
            else
            {
                Debug.LogError("Botones is null!");
            }
        }
        catch (LobbyServiceException e)
        {
            botones.FallodeConexion("Error al crear Lobby");
            Debug.Log(e);
        }
    }

    private void onPlayerJoined(List<LobbyPlayerJoined> list)
    {
        player2.SetText(list[0].Player.Data["NombreJug"].Value);
    }

    private void onPlayerLeft(List<int> list)
    {
        player2.SetText("Player 2");

    }
    private void OnApplicationQuit()
    {
        LeaveLobby();
    }
    private void onPlayerDataChange(Dictionary<int, Dictionary<string, ChangedOrRemovedLobbyValue<PlayerDataObject>>> dictionary)
    {
        Debug.Log("player cambios");
    }
    private void onLobbyDeleted()
    {
        LeaveLobby();
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
            }
        }
    }
    public async void JoinLobby(string code)
    {
        try
        {
            JoinLobbyByCodeOptions joinLobbyByCode = new JoinLobbyByCodeOptions
            {
                Player = GetPlayer(false)
            };
            Lobby lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(code, joinLobbyByCode);
            lobbyUnido = lobby;
            var callbacks = new LobbyEventCallbacks();
            callbacks.LobbyChanged += OnLobbyChanged;
            callbacks.LobbyDeleted += onLobbyDeleted;
            callbacks.PlayerLeft += onPlayerLeft;
            callbacks.PlayerDataChanged += onPlayerDataChange;
            await Lobbies.Instance.SubscribeToLobbyEventsAsync(lobby.Id, callbacks);

            string relayJoinCode = lobbyUnido.Data["RCode"].Value;
            Debug.Log("Relay join code: " + relayJoinCode);

            // Unirse a la allocation de Relay usando el join code
            var joinAllocation = await Relay.Instance.JoinAllocationAsync(relayJoinCode);
            Debug.Log("Joined Relay allocation");

            // Configurar la direcci�n del cliente para unirse al servidor Relay usando NGO
            var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));

            // Iniciar el cliente con NGO
            NetworkManager.Singleton.StartClient();

            codeText.SetText(lobbyUnido.LobbyCode);
            level.SetText(lobbyUnido.Data["Nivel"].Value);
            mode.SetText(lobbyUnido.Data["Mode"].Value);
            level_selection.gameObject.SetActive(false);
            mode_selection.gameObject.SetActive(false);
            level.gameObject.SetActive(true);
            mode.gameObject.SetActive(true);
            player1.SetText(lobbyUnido.Players[0].Data["NombreJug"].Value);
            player2.SetText(nombreJug);
            botones.UnirLobby();
        }
        catch (LobbyServiceException e)
        {
            botones.FallodeConexion("Error al unirse al Lobby");            
            Debug.Log("HOLASDASD");
        }
    }

    private Player GetPlayer(bool status)
    {
        string estado = status ? "si" : "no";
        return new Player
        {
            Data = new Dictionary<string, PlayerDataObject> {
                        { "NombreJug", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, nombreJug) },
                        { "PlayerId", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, PlayerPrefs.GetInt("UserId").ToString())},
                        {
            "Ready", new PlayerDataObject(
                visibility: PlayerDataObject.VisibilityOptions.Member,
                value: estado)
        }
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
                    {"Nivel", new DataObject(DataObject.VisibilityOptions.Member, nivel) }
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
                {"NombreJug", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, nuevoNombre) }
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
            if (imHost())
            {
                await LobbyService.Instance.DeleteLobbyAsync(lobbyUnido.Id);
            }
            else
            {
                await LobbyService.Instance.RemovePlayerAsync(lobbyUnido.Id, AuthenticationService.Instance.PlayerId);
            }
            lobbyUnido = null;
            canvaLobby.gameObject.SetActive(false);
            canvaPreLobby.gameObject.SetActive(true);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    public async void KickPlayer()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(lobbyUnido.Id, lobbyUnido.Players[1].Id);
            player2.SetText("Player 2");
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    private void OnLobbyChanged(ILobbyChanges changes)
    {
        Debug.Log("Lobby changed");
        changes.ApplyToLobby(lobbyUnido);
        level.SetText(lobbyUnido.Data["Nivel"].Value);
        mode.SetText(lobbyUnido.Data["Mode"].Value);
        if (lobbyUnido != null)
        {
            if (changes.PlayerLeft.Changed)
            {
                if (lobbyUnido.HostId == AuthenticationService.Instance.PlayerId)
                {
                    LeaveLobby();
                    lobbyUnido = null;
                    canvaLobby.gameObject.SetActive(false);
                    canvaPreLobby.gameObject.SetActive(true);
                }
            }            
            if (changes.Data.Value["Empezado"].Changed)
            {
                Debug.Log("ERMERESFD");
                empesar empesar = FindObjectOfType<empesar>();
                empesar.crearClient(lobbyUnido.Data["Nivel"].Value);
            }
        }
    }
    public bool imHost()
    {
        if (lobbyUnido.HostId == AuthenticationService.Instance.PlayerId)
        {

            return true;
        }
        return false;
    }
    public async void Empezado()
    {
        try
        {
            var callbacks = new LobbyEventCallbacks();
            callbacks.LobbyChanged += (lobby) =>
            {
                Debug.Log("Lobby changed: " + lobby);
            };
            lobbyUnido = await Lobbies.Instance.UpdateLobbyAsync(lobbyUnido.Id, new UpdateLobbyOptions
            {
                Data = new Dictionary<string, DataObject>
                {
                    {"Empezado", new DataObject(DataObject.VisibilityOptions.Member, "si") }
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
    public async void ChangeMode(string mode)
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
                {"Mode", new DataObject(DataObject.VisibilityOptions.Member, mode) }
            }
            });
            lobbyUnido = lobbyCreado;

        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    public async void ChangeCode(string code)
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
                {"RCode", new DataObject(DataObject.VisibilityOptions.Member, code) }
            }
            });
            lobbyUnido = lobbyCreado;

        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    public async void ChangeStatus()
    {
        ready = !ready;
        string estado = ready ? "si" : "no";
        try
        {
            await LobbyService.Instance.UpdatePlayerAsync(lobbyUnido.Id, AuthenticationService.Instance.PlayerId, new UpdatePlayerOptions
            {
                Data = new Dictionary<string, PlayerDataObject>
            {
                {"NombreJug", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, nombreJug) },
                { "PlayerId", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, PlayerPrefs.GetInt("UserId").ToString())},
                {"Ready", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, estado) }
            }
            });
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    public bool iReady()
    {
        foreach (var player in lobbyUnido.Players)
        {
            if (player.Id == AuthenticationService.Instance.PlayerId)
            {
                if (player.Data["Ready"].Value == "si")
                    return true;
            }
        }
        return false;
    }
    public bool uReady()
    {
        foreach (var player in lobbyUnido.Players)
        {
            if (player.Id != AuthenticationService.Instance.PlayerId)
            {
                if (player.Data["Ready"].Value == "si")
                    return true;
            }
        }
        return false;
    }
}
public static class NameGenerator
{
    public static string GetName(string userId)
    {
        int seed = userId.GetHashCode();
        seed *= Math.Sign(seed);
        StringBuilder nameOutput = new StringBuilder();
        #region Word part
        int word = seed % 22;
        if (word == 0) // Note that some more data-driven approach would be better.
            nameOutput.Append("Ant");
        else if (word == 1)
            nameOutput.Append("Bear");
        else if (word == 2)
            nameOutput.Append("Crow");
        else if (word == 3)
            nameOutput.Append("Dog");
        else if (word == 4)
            nameOutput.Append("Eel");
        else if (word == 5)
            nameOutput.Append("Frog");
        else if (word == 6)
            nameOutput.Append("Gopher");
        else if (word == 7)
            nameOutput.Append("Heron");
        else if (word == 8)
            nameOutput.Append("Ibex");
        else if (word == 9)
            nameOutput.Append("Jerboa");
        else if (word == 10)
            nameOutput.Append("Koala");
        else if (word == 11)
            nameOutput.Append("Llama");
        else if (word == 12)
            nameOutput.Append("Moth");
        else if (word == 13)
            nameOutput.Append("Newt");
        else if (word == 14)
            nameOutput.Append("Owl");
        else if (word == 15)
            nameOutput.Append("Puffin");
        else if (word == 16)
            nameOutput.Append("Rabbit");
        else if (word == 17)
            nameOutput.Append("Snake");
        else if (word == 18)
            nameOutput.Append("Trout");
        else if (word == 19)
            nameOutput.Append("Vulture");
        else if (word == 20)
            nameOutput.Append("Wolf");
        else
            nameOutput.Append("Zebra");
        #endregion

        int number = seed % 1000;
        nameOutput.Append(number.ToString("000"));

        return nameOutput.ToString();
    }
}