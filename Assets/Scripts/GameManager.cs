using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Linq;
using DialogueEditor;
//using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] float _endSpace;
    public GameObject player;
    BallUserControl playerControl;
    [NonSerialized]public SavePoint savePoint;
    public static GameManager instance;

    [SerializeField]private GameObject _levelCompletedPanel;
    
    [Header("GreenAreaReset")]
    [SerializeField] private SavePoint[] _greenSpotsArea;
    [SerializeField] private Structure[] _floorGreenSpotsArea;
    [SerializeField] private SavePoint _lastSpot;

    [SerializeField] private GameObject _warningOn;
    [SerializeField] private GameObject _warningOff;
    private int _counter;
    private int _counter2;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
      
        Time.timeScale = 1;
        playerControl = player.GetComponent<BallUserControl>();
    }

    private void Start()
    {
        _levelCompletedPanel.SetActive(false);
        _warningOn.SetActive(false);
        _warningOff.SetActive(false);
        _counter = 1;
        _counter2 = 1;
    }

    private void Update()
    {
        if (player.transform.position.y <= _endSpace)
        {
            ResetPosition();
        }

        if (_greenSpotsArea.Contains(savePoint) && _counter == 1)
        {
                _warningOn.SetActive(true);
                _counter--;
        }
        
        if (savePoint == _lastSpot && _counter2 ==1)
        {
            _warningOff.SetActive(true);
            _counter2--;
        }
    }

    public void ResetPosition()
    {
        player.transform.position = savePoint.transform.position;
        playerControl.RestartState();
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        if (_greenSpotsArea.Contains(savePoint))ResetColorFloor();
    }

    void ResetColorFloor()
    {
        foreach (var v in _floorGreenSpotsArea)
        {
            v.ResetToGrey();
        }
        
    }
    public void LevelCompleted()
    {
        HudManager.Instance._dataCanvas.SetActive(true);
        HudManager.Instance.UpdateData();
        _levelCompletedPanel.SetActive(true);
        Time.timeScale = 0;
    }

    
}
