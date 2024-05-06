using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    private string filePath;
    // Start is called before the first frame update
    void Start()
    {
        // Path to the text file
        filePath = Path.Combine(Application.streamingAssetsPath, "login.txt");

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read the entire file as a string
            string[] logindata = File.ReadAllText(filePath).Split(";");
            Debug.Log("File Contents: " + logindata[0]+", " + logindata[1]);
            LoginData loginData = new LoginData();
            loginData.email = logindata[0];
            loginData.password = logindata[1];
            StartCoroutine(loginAPI(Settings.URL+"/users/login", JsonUtility.ToJson(loginData)));
            // Now you can parse/process the fileContents as needed
        }
        else
        {
            string error = ("File not found at path: " + filePath);
            FindFirstObjectByType<TMP_Text>().text = error;
        }
    }

    IEnumerator loginAPI(string url, string body)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(url, body, "application/json"))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                MessageData messageData = JsonUtility.FromJson<MessageData>(responseText);
                FindFirstObjectByType<TMP_Text>().text = messageData.msg;
                if(request.responseCode == 200)
                {
                    #if UNITY_EDITOR
                    #else
                    File.Delete(filePath);
                    #endif
                    SceneManager.LoadScene("MainMenu");
                }
            }
            else
            {
                Debug.Log("Error: " + request.error);
                FindFirstObjectByType<TMP_Text>().text = request.error;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class MessageData
{
    public string msg;
}
[System.Serializable]
public class LoginData
{
    public string email;
    public string password;
}