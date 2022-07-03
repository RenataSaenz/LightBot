using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public static int minutes;
    public static string time;
    void Update()
    {
 
        float t = Time.timeSinceLevelLoad;

        float milliseconds = (Mathf.Floor(t * 100) % 100);

        int seconds = (int)(t % 60);

        t /= 60; 
        minutes = (int)(t % 60);
        t /= 60;
        int hours = (int)(t % 24); 
        
        time = "Time: " + string.Format("{0}:{1}:{2}.{3}", hours.ToString("00"), minutes.ToString("00"), seconds.ToString("00"), milliseconds.ToString("00"));

      //  HudManager.Instance._timer.text = "Time: " + string.Format("{0}:{1}:{2}.{3}", hours.ToString("00"), minutes.ToString("00"), seconds.ToString("00"), milliseconds.ToString("00"));
    }

}