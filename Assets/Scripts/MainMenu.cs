using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        NetworkObject[] objects = FindObjectsOfType<NetworkObject>();
        foreach (NetworkObject obj in objects)
        {
            Destroy(obj.gameObject);
        }

    }
    // Start is called before the first frame update
    public void Play()
    {
        SceneManager.LoadScene("Lobby");   
    }

    // Update is called once per frame
    public void Exit()
    {
        Application.Quit();
        
    }
}
