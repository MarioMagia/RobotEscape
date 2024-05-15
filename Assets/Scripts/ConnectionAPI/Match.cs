using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Match : MonoBehaviour
{
    public void EnterStage(string name)
    {
        StartCoroutine(GetStageAPI(Settings.URL + "/stages/getStageByName/"+name));
    }

    IEnumerator GetStageAPI(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                StageData stageData = JsonUtility.FromJson<StageData>(responseText);
                if (request.responseCode == 200)
                {
                    PlayerPrefs.SetInt("StageId", stageData.id);
                    MatchStageBody body = new MatchStageBody();
                    body.id_stage = stageData.id;
                    body.id_match = PlayerPrefs.GetInt("MatchId");
                    StartCoroutine(PostAPI(Settings.URL + "/match_stage/enterStage", JsonUtility.ToJson(body)));
                }
            }
            else
            {
                Debug.Log("Error: " + request.error);
                FindFirstObjectByType<TMP_Text>().text = request.error;
            }
        }
    }

    IEnumerator PostAPI(string url, string body)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(url, body, "application/json"))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                if (request.responseCode == 200)
                {
                    Debug.Log(url+" success");
                }
                else
                {
                    Debug.Log(url+" failure");
                }
            }
            else
            {
                Debug.Log("Error: " + request.error);
                FindFirstObjectByType<TMP_Text>().text = request.error;
            }
        }
    }

    public void EndStage(ArrayList checkpointTimes)
    {
        MatchStageBody body = new MatchStageBody();
        body.id_stage = PlayerPrefs.GetInt("StageId");
        body.id_match = PlayerPrefs.GetInt("MatchId");
        RoomTimeBody[] rooms = new RoomTimeBody[checkpointTimes.Count];
        for (int i = 0; i < checkpointTimes.Count; i++)
        {
            KeyValuePair<string, string> room = (KeyValuePair<string, string>)checkpointTimes[i];
            rooms[i] = new RoomTimeBody();
            rooms[i].id = room.Key;
            rooms[i].time = room.Value;
            // Do something with each item
        }
        body.rooms = rooms;
        StartCoroutine(PostAPI(Settings.URL + "/match_stage/endStage", JsonUtility.ToJson(body)));
    }

    public bool StartMatch(int idHost,int idClient)
    {
        StartMatchBody body = new StartMatchBody();
        body.id_host = idHost.ToString();
        body.id_client = idClient.ToString();
        StartCoroutine(StartMatchAPI(Settings.URL + "/match/startMatch", JsonUtility.ToJson(body)));
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
                StartMatchData matchData = JsonUtility.FromJson<StartMatchData>(responseText);
                if (request.responseCode == 200)
                {
                    PlayerPrefs.SetInt("MatchId", matchData.insertId);
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
public class StageData
{
    public int id;
    public string name;
    public int order;
    public int times_played;
    public int times_completed;
    public int countdown_timer;
}
[System.Serializable]
public class MatchStageBody
{
    public int id_match;
    public int id_stage;
    public RoomTimeBody[] rooms;
}
[System.Serializable]
public class RoomTimeBody
{
    public string id;
    public string time;
}
[System.Serializable]
public class StartMatchData
{
    public int insertId;
}
[System.Serializable]
public class StartMatchBody
{
    public string id_host; public string id_client;
}
