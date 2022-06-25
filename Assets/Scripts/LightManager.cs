using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public static LightManager instance;

    [SerializeField] private LightArea[] light;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    
    public enum MyLight
    {
        White,
        Green,
        Purple,
        Yellow,
        Grey
    }
}

[System.Serializable]
public class LightArea
{   
   // [Header("Light Type")]
    public LightManager.MyLight lightType;
    
   // [Header("Light Object Material")]
    public Material lightMaterial;
    
   // [Header("Light Volume Material")]
    public Material lightVolumeMaterial; 
    
   // [Header("Ball Material")]
    public Material ballMaterial;
    
}

