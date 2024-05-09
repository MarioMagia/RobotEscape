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
    [SerializeField] private TMP_Dropdown mode_selection;
    [SerializeField] private Button Ready;
    [SerializeField] private TMP_Text textoReady1;
    [SerializeField] private TMP_Text textoReady2;
    TestLobby game;
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
        
        SceneManager.activeSceneChanged += (arg0, arg1) =>
        {
            PlayerPrefs.SetString("MODO", mode_selection.options[mode_selection.value].text);
            PlayerPrefs.Save();
            if (arg1.name == level_selection.options[level_selection.value].text)
            {
                NetworkManager.Singleton.StartHost();
            }
        };
    }
    public static void crearCLient(string sala)
    {
        SceneManager.LoadScene(sala);
        SceneManager.activeSceneChanged += (arg0, arg1) =>
        {
            PlayerPrefs.SetString("MODO", sala);
            PlayerPrefs.Save();
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
                                "127.0.0.1",  // IP que entra por el input
                                7777 // Puerto server
                            );
            if (arg1.name == sala)
                NetworkManager.Singleton.StartClient();
        };

    }


}
