using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameManager : MonoBehaviour
{
    void Update() {
        if (gameObject.activeSelf)
        {
            // Set the cursor to be visible and unlocked
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void NextLevel() {
        GameManager.Instance.ChangeSceneMethod("Playground");
    }

    public void ReturnLobby() {

        GameManager.Instance.ChangeSceneMethod("Playground");
    }
    public void Exit() {
        GameManager.Instance.ChangeSceneMethod("Playground");

    }
}
