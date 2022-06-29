﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Queries : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private Material _green;
    [SerializeField] private Material _purple;
    [SerializeField] private Material _yellow;
    [SerializeField] private Material _white;
    [SerializeField] private LightArea _activeLightArea;
    [SerializeField] private LightArea _nonActiveLightArea;
    
    public bool isBox;
    public float radius = 20f;
    public SpatialGrid targetGrid;
    public float width = 15f;
    public float height = 30f;
    public IEnumerable<GridEntity> selected = new List<GridEntity>();

    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private int _currentWaypoint;
    [SerializeField] private float _speed;

    private void Update()
    {
        Vector3 dir = _waypoints[_currentWaypoint].position - transform.position;
        dir.y = 0;
        transform.forward = dir;
        transform.position += transform.forward * _speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, _waypoints[_currentWaypoint].position) <= 0.05f)
        {
            if (_currentWaypoint < _waypoints.Count() - 1)
            {
                _currentWaypoint++;
            }
            else
            {
                _currentWaypoint = 0;
            }
        }
    }

    public IEnumerable<GridEntity> Query()
    {
        if (isBox)
        {
            var h = height * 0.5f;
            var w = width * 0.5f;
            //posicion inicial --> esquina superior izquierda de la "caja"
            //posición final --> esquina inferior derecha de la "caja"
            //como funcion para filtrar le damos una que siempre devuelve true, para que no filtre nada.
            return targetGrid.Query(
                transform.position + new Vector3(-w, 0, -h),
                transform.position + new Vector3(w, 0, h),
                x => true);
        }
        else
        {
            //creo una "caja" con las dimensiones deseadas, y luego filtro segun distancia para formar el círculo
            return targetGrid.Query(
                transform.position + new Vector3(-radius, 0, -radius),
                transform.position + new Vector3(radius, 0, radius),
                x => {
                    var position2d = x - transform.position;
                    position2d.y = 0;
                    return position2d.sqrMagnitude < radius * radius;
                });
        }
    }

    void OnDrawGizmos()
    {
        if (targetGrid == null)
            return;

        //Flatten the sphere we're going to draw
        Gizmos.color = Color.cyan;
        if (isBox)
            Gizmos.DrawWireCube(transform.position, new Vector3(width, 0, height));
        else
        {
            Gizmos.matrix *= Matrix4x4.Scale(Vector3.forward + Vector3.right);
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        if (Application.isPlaying)
        {
            selected = Query();
            var temp = FindObjectsOfType<GridEntity>().Where(x=>!selected.Contains(x));
            foreach (var item in temp)
            {
                item.onGrid = false;
                var str = item.GetComponent<Structure>();
                if (str != null)
                {
                    str.LightChangeColor(_nonActiveLightArea);
                }
            }
            foreach (var item in selected)
            {
                item.onGrid = true;
                var str = item.GetComponent<Structure>();
                if (str != null)
                {
                    str.LightChangeColor(_activeLightArea);
                }
            }

        }
    }

    // void GetType(Material d)
    // {
    //     if (d == _green)
    //     {
    //         _defaultColor.lightType = LightManager.MyLight.Green;
    //         return;
    //     }
    //
    //     if (d == _purple)
    //     {
    //         _defaultColor.lightType = LightManager.MyLight.Purple;
    //         return;
    //     }
    //
    //     if (d == _white)
    //     {
    //         _defaultColor.lightType = LightManager.MyLight.White;
    //         return;
    //     }
    //
    //     if (d == _yellow)
    //     {
    //         _defaultColor.lightType = LightManager.MyLight.Yellow;
    //         // return;
    //     }
    //     //
    //     // if (d == _grey)
    //     // {
    //     //     _defaultColor = LightManager.MyLight.Grey;
    //     //     return;
    //     // }
    // }
    
    private void OnGUI()
    {
        GUI.Label( new Rect(0,0,20,20), "HOLA");
    }
}
