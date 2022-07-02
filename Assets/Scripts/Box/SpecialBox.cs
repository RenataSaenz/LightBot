using System;
using System.Linq;
using UnityEngine;

public class SpecialBox: MonoBehaviour
{
    public int points;
    public LightManager.MyLight typeOfLight;
    public bool collected;
    public float timeCollected;
    

    private void Start()
    {
        BoxManager.Instance.collectableObjects.specialBoxes.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        collected = true;
        gameObject.SetActive(false);
        
    }
    
}