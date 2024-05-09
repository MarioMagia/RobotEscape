using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectSceneManager : NetworkBehaviour
{
    [Rpc(SendTo.Server)]
    public void LoadNetworkSceneRpc(string sceneName)
    {
        NetworkManager.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
