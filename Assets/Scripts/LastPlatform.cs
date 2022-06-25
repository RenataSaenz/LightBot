using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlatform : MonoBehaviour, IColored
{
    [SerializeField] private LightManager.MyLight _defaultColor;


    public void Color(LightArea colorData)
    {
        var color = colorData.lightType;
        if (_defaultColor == color)
        {
            GameManager.instance.LevelCompleted();
            return;
        }
            GameManager.instance.ResetPosition();
    }
}
