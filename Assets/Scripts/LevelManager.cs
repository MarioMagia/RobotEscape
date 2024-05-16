using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;

    private void Start()
    {
        if (levelManager)
            Destroy(this);
        else
            levelManager = this;
    }

    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Destroy(GameObject.Find("NetworkManager"));
    }
}
