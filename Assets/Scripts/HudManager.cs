using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudManager : MonoBehaviour
{
    public TMP_Text _timer;
    [SerializeField]TMP_Text _points;
    private int box;
    public static HudManager Instance;
    
    public TMP_Text _data;
    public GameObject _dataCanvas;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        _dataCanvas.SetActive(false);
    }
    public void AddBoxToHud(int p)
    {
        box += p;
        _points.text = "Total Boxes: " + box;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _dataCanvas.SetActive(true);
            UpdateData();
            Time.timeScale = 0;
            
            if (BoxManager.Instance.BoxAchievement(10))
            {
                Debug.Log("achievement completed");
            }
            else
            {
                Debug.Log("achievement not completed");
            }
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            _dataCanvas.SetActive(false);
            Time.timeScale = 1;
        }
        
    }

    public void UpdateData()
    {
        _data.text = "Yellow Total Points: " +
                     BoxManager.Instance.CountTotalPointsCollectedByColor(LightManager.MyLight.Yellow) + "/" +
                     BoxManager.Instance.CountTotalPointsByColor(LightManager.MyLight.Yellow) + Environment.NewLine +
                     "Yellow LightBoxes Collected: " +
                     BoxManager.Instance.CountTotalBoxesCollectedByColor(LightManager.MyLight.Yellow) + "/" +
                     BoxManager.Instance.CountTotalBoxesByColor(LightManager.MyLight.Yellow) + Environment.NewLine +
                     "Green Total Points: " +
                     BoxManager.Instance.CountTotalPointsCollectedByColor(LightManager.MyLight.Green) + "/" +
                     BoxManager.Instance.CountTotalPointsByColor(LightManager.MyLight.Green) + Environment.NewLine +
                     "Green LightBoxes Collected: " +
                     BoxManager.Instance.CountTotalBoxesCollectedByColor(LightManager.MyLight.Green) + "/" +
                     BoxManager.Instance.CountTotalBoxesByColor(LightManager.MyLight.Green) + Environment.NewLine +
                     "Purple Total Points: " +
                     BoxManager.Instance.CountTotalPointsCollectedByColor(LightManager.MyLight.Purple) + "/" +
                     BoxManager.Instance.CountTotalPointsByColor(LightManager.MyLight.Purple) + Environment.NewLine +
                     "Purple LightBoxes Collected: " +
                     BoxManager.Instance.CountTotalBoxesCollectedByColor(LightManager.MyLight.Purple) + "/" +
                     BoxManager.Instance.CountTotalBoxesByColor(LightManager.MyLight.Purple) + Environment.NewLine +
                     "LightBoxes Points Collected: " + BoxManager.Instance.CountTotalPointsCollected() + "/" +
                     BoxManager.Instance.CountTotalPoints() + Environment.NewLine +
                     "LightBoxes Collected: " + BoxManager.Instance.CountTotalBoxesCollected() + "/" +
                     BoxManager.Instance.CountTotalBoxes() + Environment.NewLine +
                     "Red Total Boxes Collected: " +
                    BoxManager.Instance.CountTotalRedBoxes();

    }
}
