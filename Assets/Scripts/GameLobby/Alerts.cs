using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertController : MonoBehaviour
{
    public TMP_Text alertText;
    public float displayDuration = 3f;

    private bool isDisplaying = false;

    public void ShowAlert(string message)
    {
        if (!isDisplaying)
        {
            isDisplaying = true;
            alertText.text = message;
            alertText.gameObject.SetActive(true);
            Invoke("HideAlert", displayDuration);
        }
    }

    private void HideAlert()
    {
        alertText.gameObject.SetActive(false);
        isDisplaying = false;
    }
}
