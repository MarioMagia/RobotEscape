using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
   
    [SerializeField] private TextMeshProUGUI generalTimerText;
    [SerializeField] private TextMeshProUGUI levelCountdownText;

    private float timerUp = 0f;
    private float timerDown = 300f;

    private bool isPaused = false;

    private void Start()
    {      

        // Iniciar la actualización del temporizador cada segundo
        InvokeRepeating("UpdateTimer", 0f, 1f);
    }

    // Método para actualizar el temporizador
    private void UpdateTimer()
    {
        if (!isPaused)
        {

        // Incrementar el temporizador general
        timerUp += 1f;

        // Restar a la cuenta atras
        timerDown -= 1f;

        // Actualizar el texto del temporizador en formato de minutos y segundos
        int minutesUp = Mathf.FloorToInt(timerUp / 60f);
        int secondsUp = Mathf.FloorToInt(timerUp % 60f);

        // Actualizar el texto de la cuenta atras en formato de minutos y segundos
        int minutesDown = Mathf.FloorToInt(timerDown / 60f);
        int secondsDown = Mathf.FloorToInt(timerDown % 60f);
        generalTimerText.text = string.Format("{0:00}:{1:00}", minutesUp, secondsUp);
        levelCountdownText.text = string.Format("{0:00}:{1:00}", minutesDown, secondsDown);
        }
    }

    // Método para iniciar el temporizador
    public void StartTimer()
    {
        isPaused = false;
    }

    // Método para pausar el temporizador
    public void PauseTimer()
    {
        isPaused = true;
    }
}
