using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, IColored
{
    [Header("Lighter")]
    [SerializeField] private Material _green;
    [SerializeField] private Material _purple;
    [SerializeField] private Material _yellow;
    [SerializeField] private Material _white;
    [SerializeField] private Material _grey;

    [Header("Light")]
    [SerializeField] private Material _greenLight;
    [SerializeField] private Material _purpleLight;
    [SerializeField] private Material _yellowLight;
    [SerializeField] private Material _whiteLight;
    
    [SerializeField] private LightManager.MyLight _defaultColor;
    
    
    [Header("Set GameObjects")]
    [SerializeField]private GameObject _lightVolume;
    [SerializeField]private GameObject _lighter;
    private Material _default;

    private Player _player;
    
    [Header("Save")]
    [SerializeField]private bool _isNotSaveSpot = false;
    
    private void Start()
    {
        _default = _lighter.GetComponent<MeshRenderer>().sharedMaterial;
        GetType(_default);
        _player = GameManager.instance.player.GetComponent<Player>();
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
    public void Color(LightArea colorData)
    {  var color = colorData.lightType;
        if (_defaultColor != LightManager.MyLight.Grey)
        {
            if(!_isNotSaveSpot) GameManager.instance.savePoint = this;
            _player.ChangeColor(_defaultColor);
            return;
        }
        
        if (_defaultColor == LightManager.MyLight.Grey) 
        {
            switch (color)
            {
                case LightManager.MyLight.Green:
                    if(!_isNotSaveSpot) GameManager.instance.savePoint = this;
                    _lighter.GetComponent<MeshRenderer>().material = _green;
                    _lightVolume.GetComponent<MeshRenderer>().material = _greenLight;
                    _defaultColor = LightManager.MyLight.Green;
                    break;
                case LightManager.MyLight.Purple:
                    if(!_isNotSaveSpot) GameManager.instance.savePoint = this;
                    _lighter.GetComponent<MeshRenderer>().material = _purple;
                    _lightVolume.GetComponent<MeshRenderer>().material = _purpleLight;
                    _defaultColor = LightManager.MyLight.Purple;
                    break;
                case LightManager.MyLight.White:
                    if(!_isNotSaveSpot) GameManager.instance.savePoint = this;
                    _lighter.GetComponent<MeshRenderer>().material = _white;
                    _lightVolume.GetComponent<MeshRenderer>().material = _whiteLight;
                    _defaultColor = LightManager.MyLight.White;
                    break;
                case LightManager.MyLight.Yellow:
                    if(!_isNotSaveSpot) GameManager.instance.savePoint = this;
                    _lighter.GetComponent<MeshRenderer>().material = _yellow;
                    _lighter.GetComponent<MeshRenderer>().material = _yellowLight;
                    _defaultColor = LightManager.MyLight.Yellow;
                    break;
            }
        }    GameManager.instance.ResetPosition();
    }
}
