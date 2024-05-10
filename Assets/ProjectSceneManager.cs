using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectSceneManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        NetworkManager.Singleton.SceneManager.OnLoadComplete += SceneManager_OnLoadComplete;
    }

    public void LoadNetworkSceneRpc(string sceneName)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        
    }

    private void SceneManager_OnLoadComplete(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        ReloadPlayerRpc();
    }

    public void ReloadPlayerRpc()
    {
        Debug.Log("Holaaaajsdsajda");
        NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<ReloadPlayer>().Reload();
    }
}
