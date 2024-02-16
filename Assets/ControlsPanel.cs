using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlsPanel : MonoBehaviour
{
    public RectTransform contentPanel;
    public GameObject controlPrefab;

    void Start()
    {
        /*
                string[] controlNames = GetControlNames();

                // Create UI elements for each control
                foreach (string controlName in controlNames)
                {
                    GameObject newControl = Instantiate(controlPrefab);
                    Debug.Log(controlName);
                    Button button = newControl.GetComponentInChildren<Button>();
                    button.onClick.AddListener(() => HandleControlClick(controlName));
                    newControl.transform.SetParent(this.gameObject.transform, false);
                    newControl.transform.GetChild(0).GetComponent<TMP_Text>().text = controlName;
                }*
            }

            void HandleControlClick(string controlName)
            {
                // Handle button click for the control
                Debug.Log("Clicked on control: " + controlName);
            }

            // Example method to retrieve control names (replace with your own logic)
            string[] GetControlNames()
            {
                // Example array of control names
                return new string[] { "Forward", "Left", "Backwards", "Right", "Jump", "Sprint", "Pick Up", "Crouch", "Place Mark", "Teleport 1", "Teleport 2", "Pause Menu" };
            }*/
    }
}