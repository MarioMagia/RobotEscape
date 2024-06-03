using SerializableCallback;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private string GoTo; 
    // Start is called before the first frame update
    void Start()
    {
        nextLevelButton.onClick.AddListener(GoToNextLevel);
    }

    void GoToNextLevel()
    {
        if (PlayerPrefs.GetString("MODO").ToLower() == "time trial")
        {
            FindObjectOfType<Match>().EnterStage(GoTo);
        }
        
        NetworkManager.Singleton.SceneManager.LoadScene(GoTo, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
