using System;
using System.Linq;
using UnityEngine;

public class RescueRobot: MonoBehaviour
{
    public BotsNames name;
    public int points;
    public LightManager.MyLight typeOfLight;
    public bool collected;
    public float minuteCollected;
    public string timeCollected;


    private void Start()
    {
        ScoreManager.Instance.collectableObjects.AddRobot(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerColor = other.gameObject.GetComponent<Player>()._defaultColor.lightType;
        
        if (playerColor == null) return;
        
        if (playerColor != typeOfLight) return;
        
        
        SoundManager.instance.Play(SoundManager.Types.Item);
            collected = true;
            minuteCollected = LevelTimer.minutes;
            timeCollected = LevelTimer.time;
        gameObject.SetActive(false);
        
    }
   
}
public enum BotsNames
{
    YellowBot,
    GreenBot,
    PurpleBot,
}
