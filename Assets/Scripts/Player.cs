using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public LightManager.MyLight _defaultColor;
    
    [Header("Colors")]
    [SerializeField] private Material _green;
    [SerializeField] private Material _purple;
    [SerializeField] private Material _yellow;
    [SerializeField] private Material _white;
    
    private Material _defaultMaterial;
    
    private void Awake()
    {
        _defaultMaterial = GetComponent<MeshRenderer>().sharedMaterial;
        GetType(_defaultMaterial);
    }
    void GetType(Material d)
    {
        if (d == _green)
        {
            _defaultColor = LightManager.MyLight.Green;
            return;
        }

        if (d == _purple)
        {
            _defaultColor = LightManager.MyLight.Purple;
            return;
        }

        if (d == _white)
        {
            _defaultColor = LightManager.MyLight.White;
            return;
        }

        if (d == _yellow)
        {
            _defaultColor = LightManager.MyLight.Yellow;
           // return;
        }
        //
        // if (d == _grey)
        // {
        //     _defaultColor = LightManager.MyLight.Grey;
        //     return;
        // }
    }
    private void OnCollisionEnter(Collision other)
    {
             var obj = other.gameObject.GetComponent<IColored>();
            
             if (obj != null) obj.Color(_defaultColor);
    }
    private void OnTriggerEnter(Collider other)
    {
             var obj = other.gameObject.GetComponent<IColored>();
            
             if (obj != null) obj.Color(_defaultColor);
    }

    public void ChangeColor(LightManager.MyLight color)
    {
        switch (color)
        {
            case LightManager.MyLight.Green:
                GetComponent<MeshRenderer>().material = _green;
                _defaultColor = LightManager.MyLight.Green;
                break;
            case LightManager.MyLight.Purple:
                GetComponent<MeshRenderer>().material = _purple;
                _defaultColor = LightManager.MyLight.Purple;
                break;
            case LightManager.MyLight.White:
                GetComponent<MeshRenderer>().material = _white;
                _defaultColor = LightManager.MyLight.White;
                break;
            case LightManager.MyLight.Yellow:
                GetComponent<MeshRenderer>().material = _yellow;
                _defaultColor = LightManager.MyLight.Yellow;
                break;
        }
    }
}
