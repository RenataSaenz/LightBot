using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LightBox: MonoBehaviour
{
    public int points;
    public LightManager.MyLight typeOfLight;
    public bool collected;
    public float timeCollected;
    public Material redMaterial;
    private Renderer _renderer;
    public bool red = false;

    private void Start()
    {
        ScoreManager.Instance.collectableObjects.AddLightBox(this);
        _renderer = gameObject.GetComponent<Renderer>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var playerColor = other.gameObject.GetComponent<Player>()._defaultColor.lightType;
        
        if (playerColor == null) return;
        
        SoundManager.instance.Play(SoundManager.Types.Item);
        
        if (red)
        {
            points*=-1;
            collected = true;
            gameObject.SetActive(false);
            return;
        }
        
        if (playerColor != typeOfLight)
        {
            _renderer.material = redMaterial;
            red = true;
            return;
        }
        
        HudManager.Instance.AddBoxToHud(1);
        timeCollected = LevelTimer.minutes;
        collected = true;
        gameObject.SetActive(false);
    }
}