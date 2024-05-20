using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFormatter : MonoBehaviour
{
    public static string FormatTime(int totalSeconds)
    {
        // Calculate hours, minutes, and seconds
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;

        // Format hours, minutes, and seconds with leading zeros
        string formattedHours = hours.ToString("D2");
        string formattedMinutes = minutes.ToString("D2");
        string formattedSeconds = seconds.ToString("D2");

        // Combine into the 'hh:mm:ss' format
        return $"{formattedHours}:{formattedMinutes}:{formattedSeconds}";
    }
}
