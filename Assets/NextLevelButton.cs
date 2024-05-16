using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        FindAnyObjectByType<ProjectSceneManager>().LoadNetworkSceneRpc(GoTo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
