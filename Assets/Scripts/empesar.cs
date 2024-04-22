using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class empesar : MonoBehaviour
{
    // Start is called before the first frame update
   public void crearHost()
    {
        TestLobby game = FindObjectOfType<TestLobby>();
        game.setEmpezar();
        game.Empezado();
        SceneManager.LoadScene("LevelPrueba");
        SceneManager.activeSceneChanged += (arg0, arg1) =>
        {
            NetworkManager.Singleton.StartHost();
        };
    }
    public static void crearCLient()
    {
        SceneManager.LoadScene("LevelPrueba");
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
