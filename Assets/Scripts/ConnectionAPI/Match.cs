using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Match : MonoBehaviour
{
    // Start is called before the first frame update
    public bool StartMatch(int idHost,int idClient)
    {
        StartMatchBody body = new StartMatchBody();
        body.id_host = idHost.ToString();
        body.id_client = idClient.ToString();
        StartCoroutine(StartMatchAPI(Settings.URL + "/users/login", JsonUtility.ToJson(body)));
        return false;
    }

    IEnumerator StartMatchAPI(string url, string body)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(url, body, "application/json"))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                MessageData messageData = JsonUtility.FromJson<MessageData>(responseText);
                if (request.responseCode == 200)
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
}
[System.Serializable]
public class StartMatchBody
{
    public string id_host; public string id_client;
}
