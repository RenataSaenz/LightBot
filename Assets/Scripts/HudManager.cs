using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public TMP_Text _timer;
    [SerializeField]TMP_Text _points;
    private int box;
    public static HudManager Instance;
    
    public TMP_Text _data;
    public GameObject _dataCanvas;

    [SerializeField] private PieChart _pieChart;

    [SerializeField] private Image _colorImage;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        _dataCanvas.SetActive(false);
        _pieChart.gameObject.SetActive(false);
        _colorImage.gameObject.SetActive(false);
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

            ShowAllColorsCollected();
            
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            _dataCanvas.SetActive(false);
            _pieChart.gameObject.SetActive(false);
            _colorImage.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        
    }
    public void ShowAllColorsCollected()
    {
        _pieChart.gameObject.SetActive(true);
        _colorImage.gameObject.SetActive(true);
        var colors = BoxManager.Instance.GetAllColorsCollected().OrderBy(o => o.ToString());
        
        var green = colors.SkipWhile(x => x!= LightManager.MyLight.Green).TakeWhile(x => x == LightManager.MyLight.Green).Count();
        var purple = colors.SkipWhile(x => x!= LightManager.MyLight.Purple).TakeWhile(x => x == LightManager.MyLight.Purple).Count();
        var yellow = colors.SkipWhile(x => x!= LightManager.MyLight.Yellow).TakeWhile(x => x == LightManager.MyLight.Yellow).Count();
         
        List<float> values = new List<float>();

        values.Add(green);
        values.Add(purple);
        values.Add(yellow);

        _pieChart.SetValues(values);
        
        StartCoroutine(TimeSlicerCoroutine(colors, 0.35f));
    }
    
    IEnumerator TimeSlicerCoroutine(IEnumerable<LightManager.MyLight> list, float timeQuota) 
    {
        var wait = new WaitForSecondsRealtime(timeQuota);
        foreach(var elem in list) 
        {
            Debug.Log(elem);
            switch (elem)
            {
                case LightManager.MyLight.Green:
                    _colorImage.color = Color.green;
                    break;
                case LightManager.MyLight.Purple:
                    _colorImage.color = Color.magenta;
                    break;
                case LightManager.MyLight.White:
                    _colorImage.color = Color.white;
                    break;
                case LightManager.MyLight.Yellow:
                    _colorImage.color = Color.yellow;
                    break;
            }
            yield return wait;
            _colorImage.color = Color.clear;
            yield return wait;
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
