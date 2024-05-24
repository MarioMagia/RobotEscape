using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private PlayerInput m_PlayerInput;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        NetworkObject[] objects = FindObjectsOfType<NetworkObject>();
        NetworkManager[] objectos = FindObjectsOfType<NetworkManager>();
        foreach (NetworkObject obj in objects)
        {
            Destroy(obj.gameObject);
        }
        foreach (NetworkManager obj in objectos)
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
