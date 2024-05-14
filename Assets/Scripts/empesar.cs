using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mono.Cecil.Cil;

public class empesar : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown level_selection;
    [SerializeField] private TMP_Dropdown mode_selection;
    [SerializeField] private Button Ready;
    [SerializeField] private TMP_Text textoReady1;
    [SerializeField] private TMP_Text textoReady2;
    [SerializeField] private TMP_Text lobbyCode;
    [SerializeField] private Button CopytoClip;
    [SerializeField] private GameObject ProjectSceneManager;
    TestLobby game;
    private static string code = null;
    // Start is called before the first frame update
    private void Awake()
    {
        game = FindObjectOfType<TestLobby>();
        if (game.iReady())
        {
            textoReady1.text = "Ready";
            textoReady1.color = Color.green;
        }
        if (game.uReady())
        {
            textoReady1.text = "Ready";
            textoReady1.color = Color.green;
        }
        game = FindObjectOfType<TestLobby>();
        level_selection.onValueChanged.AddListener(delegate { level_selectionValueChanged(level_selection); });
        mode_selection.onValueChanged.AddListener(delegate { mode_selectionValueChanged(mode_selection); });
        Ready.onClick.AddListener(() => { Incio(); });
        CopytoClip.onClick.AddListener(() =>
        {
            string testString = lobbyCode.text;
            testString.CopyToClipboard();
        });

    }
    void level_selectionValueChanged(TMP_Dropdown change)
    {
        game.ChangeLevel(change.options[change.value].text);
    }
    void mode_selectionValueChanged(TMP_Dropdown change)
    {
        game.ChangeMode(change.options[change.value].text);
    }
    void Incio()
    {
        if (game.imHost())
        {
            if (game.iReady() && game.uReady())
            {
                crearHost();
            }
        }
        else
        {
            game.ChangeStatus();
        }
    }
    void Update()
    {
        if (!game.imHost())
        {
            if (game.iReady())
            {
                textoReady2.text = "Ready";
                textoReady2.color = Color.green;
            }
            else
            {
                textoReady2.text = "Not Ready";
                textoReady2.color = Color.white;
            }
        }
        else
        {
            if (game.uReady())
            {
                textoReady2.text = "Ready";
                textoReady2.color = Color.green;
            }
            else
            {
                textoReady2.text = "Not Ready";
                textoReady2.color = Color.white;
            }
        }
    }
    public void crearHost()
    {
        game.Empezado();
        SceneManager.LoadScene(level_selection.options[level_selection.value].text);
        SceneManager.activeSceneChanged += OnSceneChanged;
    }
    private async void OnSceneChanged(Scene arg0, Scene arg1)
    {
        Debug.Log("HOLA");
        Debug.Log(arg1.name + " HOLA " + arg0.name);
        if (arg1.name != "MainMenu")
        {
            PlayerPrefs.SetString("MODO", mode_selection.options[mode_selection.value].text);
            Debug.Log(PlayerPrefs.GetString("MODO"));
            PlayerPrefs.Save();
            if (arg1.name == level_selection.options[level_selection.value].text)
            {
                code = await StartHostWithRelay();
                PlayerPrefs.SetString("code", code);
                PlayerPrefs.Save();
                if (code != null && !(GameObject.Find("[ Game Manager ]")))
                {
                    Instantiate(ProjectSceneManager);
                }
            }
        }
        else if (arg1.name == "MainMenu")
        {
            AuthenticationService.Instance.SignOut(true);
            AuthenticationService.Instance.ClearSessionToken();
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }
    }
    public void crearClient(string sala, string code)
    {
        SceneManager.LoadScene(sala);
        SceneManager.activeSceneChanged += async (arg0, arg1) =>
        {
            bool started = await StartClientWithRelay(code);
            if (started && !(GameObject.Find("[ Game Manager ]")))
            {
                Instantiate(ProjectSceneManager);

            }

            /*NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
                                "127.0.0.1",  // IP que entra por el input
                                7777 // Puerto server
                            );
            if (arg1.name == sala)
                NetworkManager.Singleton.StartClient();*/


        };

    }
    /// <summary>
    /// Starts a game host with a relay allocation: it initializes the Unity services, signs in anonymously and starts the host with a new relay allocation.
    /// </summary>
    /// <param name="maxConnections">Maximum number of connections to the created relay.</param>
    /// <returns>The join code that a client can use.</returns>
    /// <exception cref="ServicesInitializationException"> Exception when there's an error during services initialization </exception>
    /// <exception cref="UnityProjectNotLinkedException"> Exception when the project is not linked to a cloud project id </exception>
    /// <exception cref="CircularDependencyException"> Exception when two registered <see cref="IInitializablePackage"/> depend on the other </exception>
    /// <exception cref="AuthenticationException"> The task fails with the exception when the task cannot complete successfully due to Authentication specific errors. </exception>
    /// <exception cref="RequestFailedException"> See <see cref="IAuthenticationService.SignInAnonymouslyAsync"/></exception>
    /// <exception cref="ArgumentException">Thrown when the maxConnections argument fails validation in Relay Service SDK.</exception>
    /// <exception cref="RelayServiceException">Thrown when the request successfully reach the Relay Allocation service but results in an error.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the UnityTransport component cannot be found.</exception>

    public async Task<string> StartHostWithRelay(int maxConnections = 2)
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));
        var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        return NetworkManager.Singleton.StartHost() ? joinCode : null;
    }
    /// <summary>
    /// Joins a game with relay: it will initialize the Unity services, sign in anonymously, join the relay with the given join code and start the client.
    /// </summary>
    /// <param name="joinCode">The join code of the allocation</param>
    /// <returns>True if starting the client was successful</returns>
    /// <exception cref="ServicesInitializationException"> Exception when there's an error during services initialization </exception>
    /// <exception cref="UnityProjectNotLinkedException"> Exception when the project is not linked to a cloud project id </exception>
    /// <exception cref="CircularDependencyException"> Exception when two registered <see cref="IInitializablePackage"/> depend on the other </exception>
    /// <exception cref="AuthenticationException"> The task fails with the exception when the task cannot complete successfully due to Authentication specific errors. </exception>
    /// <exception cref="RequestFailedException">Thrown when the request does not reach the Relay Allocation service.</exception>
    /// <exception cref="ArgumentException">Thrown if the joinCode has the wrong format.</exception>
    /// <exception cref="RelayServiceException">Thrown when the request successfully reach the Relay Allocation service but results in an error.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the UnityTransport component cannot be found.</exception>
    public async Task<bool> StartClientWithRelay(string joinCode)
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode: joinCode);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));
        return !string.IsNullOrEmpty(joinCode) && NetworkManager.Singleton.StartClient();
    }

}
