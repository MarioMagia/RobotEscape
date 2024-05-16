using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertController : MonoBehaviour
{
    [SerializeField]private TMP_Text alertText;
    [SerializeField] private float displayDuration = 3f;

    private bool isDisplaying = false;

    public void ShowAlert(string message)
    {
        if (!isDisplaying)
        {
            isDisplaying = true;
            alertText.text = message;
            this.gameObject.SetActive(true);
            Invoke("HideAlert", displayDuration);
        }
    }

    private void HideAlert()
    {
        this.gameObject.SetActive(false);
        isDisplaying = false;
    }
}
