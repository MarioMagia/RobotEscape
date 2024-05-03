using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Path to the text file
        string filePath = Path.Combine(Application.streamingAssetsPath, "login.txt");

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read the entire file as a string
            string fileContents = File.ReadAllText(filePath);
            Debug.Log("File Contents: " + fileContents);

            // Now you can parse/process the fileContents as needed
        }
        else
        {
            string error = ("File not found at path: " + filePath);
            FindFirstObjectByType<TMP_Text>().text = error;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
