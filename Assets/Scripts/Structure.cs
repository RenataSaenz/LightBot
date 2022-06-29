using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Structure : MonoBehaviour, IColored
{
    [SerializeField] private Material _green;
    [SerializeField] private Material _purple;
    [SerializeField] private Material _yellow;
    [SerializeField] private Material _white;
    [SerializeField] private Material _grey;
    [SerializeField] private Material _default;
    [SerializeField] private LightManager.MyLight _defaultColor;

    private void Start()
    {
        _default = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
        GetType(_default);
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
            return;
        }

        if (d == _grey)
        {
            _defaultColor = LightManager.MyLight.Grey;
            return;
        }
    }

    public void ResetToGrey()
    {
        gameObject.GetComponent<MeshRenderer>().material = _grey;
        _defaultColor = LightManager.MyLight.Grey;
    }

    public void Color(LightArea colorBase)
    {
        var color = colorBase.lightType;
            
        if (_defaultColor == color) return;
        
        if (_defaultColor == LightManager.MyLight.Grey) 
        {
            switch (color)
            {
                case LightManager.MyLight.Green:
                    gameObject.GetComponent<MeshRenderer>().material = _green;
                    _defaultColor = LightManager.MyLight.Green;
                    break;
                case LightManager.MyLight.Purple:
                    gameObject.GetComponent<MeshRenderer>().material = _purple;
                    _defaultColor = LightManager.MyLight.Purple;
                    break;
                case LightManager.MyLight.White:
                    gameObject.GetComponent<MeshRenderer>().material = _white;
                    _defaultColor = LightManager.MyLight.White;
                    break;
                case LightManager.MyLight.Yellow:
                    gameObject.GetComponent<MeshRenderer>().material = _yellow;
                    _defaultColor = LightManager.MyLight.Yellow;
                    break;
            }
            return;
        }

        // if (_defaultColor != LightManager.MyLight.Grey && _defaultColor != color) 
            GameManager.instance.ResetPosition();
    }
}
