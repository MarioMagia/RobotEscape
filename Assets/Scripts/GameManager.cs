using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Text nivel;
    public enum GameState
    {
        OnGoing,
        End
    }
    private GameState _currentState;
    private static GameManager _instance;
    // Start is called before the first frame update
    void Start()
    {
        _currentState = GameState.OnGoing;
        //StartCoroutine(disappearText());  
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static GameManager Instance 
    {
        get {
            if (_instance is null) {
                Debug.LogError("GameManager is null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    public void ChangeSceneMethod(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    /*
    IEnumerator disappearText() {
        yield return new WaitForSeconds(5);
        nivel.text = "";
    }
    */

    public void SetGameState(GameState newState)
    {
        _currentState = newState;
    }
    public GameState GetGameState()
    {
        return _currentState;
    }

}