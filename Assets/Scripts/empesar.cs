using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class empesar : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown level_selection;
    [SerializeField] private Button Ready;
    [SerializeField] private TMP_Text textoReady1;
    [SerializeField] private TMP_Text textoReady2;
    TestLobby game;
    // Start is called before the first frame update
    private void Awake()
    {
        game = FindObjectOfType<TestLobby>();
        level_selection.onValueChanged.AddListener(delegate { level_selectionValueChanged(level_selection); });
        Ready.onClick.AddListener(() => { Incio(); });
        if (game.iReady())
        {
            textoReady1.text = "Ready";
            textoReady1.color = Color.green;
        }
    }
    void level_selectionValueChanged(TMP_Dropdown change)
    {
        game.ChangeLevel(change.options[change.value].text);
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
            game.ChangeStatus();
        }
    }
    public void crearHost()
    {
        game.Empezado();
        SceneManager.LoadScene(level_selection.options[level_selection.value].text);
        SceneManager.activeSceneChanged += (arg0, arg1) =>
        {
            NetworkManager.Singleton.StartHost();
        };
    }
    public static void crearCLient(string sala)
    {
        SceneManager.LoadScene(sala);
        SceneManager.activeSceneChanged += (arg0, arg1) =>
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
                                "127.0.0.1",  // IP que entra por el input
                                7777 // Puerto server
                            );
            NetworkManager.Singleton.StartClient();
        };

    }


}
