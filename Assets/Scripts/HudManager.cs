using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

//IA2-P1
public class HudManager : MonoBehaviour
{
    public TMP_Text _timer;
    [SerializeField]TMP_Text _points;
    private int box;
    public static HudManager Instance;
    
    public TMP_Text _yellowData;
    public TMP_Text _purpleData;
    public TMP_Text _greenData;
    public TMP_Text _redData;
    public GameObject _dataCanvas;

    [SerializeField] private PieChart _pieChart;

    [SerializeField] private Image _colorImage;
    
    [SerializeField] private MyBots[] _bots;
    
    private int _yellowPointsCollected;
    private int _greenPointsCollected;
    private int _purplePointsCollected;


    Coroutine _routine;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        _dataCanvas.SetActive(false);
        _pieChart.gameObject.SetActive(false);
        _colorImage.gameObject.SetActive(false);

        foreach (var robot in _bots)
        {
            robot.Set(false);
        }
    }
    public void AddBoxToHud(int p)
    {
        box += p;
        _points.text = "Total Boxes: " + box;
    }

    private void Update()
    {
        _timer.text = LevelTimer.time;
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _dataCanvas.SetActive(true);
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

            List<BotsNames> botsCollected = new List<BotsNames>(BoxManager.Instance.GetRobots().ToList());
            List<string> botsData = new List<string>(BoxManager.Instance.GetTimeRobots().ToList());

            for (int i = 0; i < botsCollected.Count; i++)
            {
                SetRobot(botsCollected[i], botsData[i]);
            }

        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (_routine != null)StopCoroutine(_routine);
            _dataCanvas.SetActive(false);
            _pieChart.gameObject.SetActive(false);
            _colorImage.gameObject.SetActive(false);
           Time.timeScale = 1;
           
        }
    }
    private void SetRobot(BotsNames robotName, string data)
    {
        MyBots r = Array.Find(_bots, robot => robot.botName == robotName);
        
        if (r == null)
        {
            Debug.LogWarning("Robot: " + robotName + " not found!");
            return;
        }
        r.Set(true);
        r.time.text = data;
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
        
        _routine = StartCoroutine(TimeSlicerCoroutine(colors, 0.3f));
    }
    
    
    IEnumerator TimeSlicerCoroutine(IEnumerable<LightManager.MyLight> list, float timeQuota)
    {
        List<int> totalYellowPointsCollected = new List<int>(BoxManager.Instance.GetAllPointsCollectedByColor(LightManager.MyLight.Yellow));
        List<int> totalGreenPointsCollected = new List<int>(BoxManager.Instance.GetAllPointsCollectedByColor(LightManager.MyLight.Green));
        List<int> totalPurplePointsCollected = new List<int>(BoxManager.Instance.GetAllPointsCollectedByColor(LightManager.MyLight.Purple));
        
        
        int yellowCounter = 0;
        int greenCounter = 0;
        int purpleCounter = 0;
        
        _yellowPointsCollected = 0;
        _greenPointsCollected = 0;
        _purplePointsCollected = 0;
        
        var wait = new WaitForSecondsRealtime(timeQuota);
        
        foreach(var elem in list) 
        {
            SoundManager.instance.Play(SoundManager.Types.Item);

            switch (elem)
            {
                case LightManager.MyLight.Green:
                {
                    _colorImage.color = Color.green;
                    _greenPointsCollected += totalGreenPointsCollected[greenCounter];
                    greenCounter++;
                    break;  
                }
                case LightManager.MyLight.Purple:
                {
                    _colorImage.color = new Color(0.3799582f, 0, 1);
                    _purplePointsCollected += totalPurplePointsCollected[purpleCounter];
                    purpleCounter++;
                    break;
                }
                case LightManager.MyLight.White:
                    _colorImage.color = Color.white;
                    break;
                case LightManager.MyLight.Yellow:
                {
                    _colorImage.color = Color.yellow;
                    _yellowPointsCollected += totalYellowPointsCollected[yellowCounter];
                    yellowCounter++;
                    break;
                }
            }
            
            UpdateData();
            
            yield return wait;
            _colorImage.color = Color.clear;
            yield return wait;
        }
    }

    public void UpdateData()
    {
        _yellowData.text = "Yellow Total Points: " +
                           _yellowPointsCollected + "/" +
                           BoxManager.Instance.CountTotalPointsByColor(LightManager.MyLight.Yellow) +
                           Environment.NewLine +
                           "Yellow LightBoxes Collected: " +
                           BoxManager.Instance.CountTotalBoxesCollectedByColor(LightManager.MyLight.Yellow) + "/" +
                           BoxManager.Instance.CountTotalBoxesByColor(LightManager.MyLight.Yellow);

        _greenData.text = "Green Total Points: " +
                          _greenPointsCollected + "/" +
                          BoxManager.Instance.CountTotalPointsByColor(LightManager.MyLight.Green) +
                          Environment.NewLine +
                          "Green LightBoxes Collected: " +
                          BoxManager.Instance.CountTotalBoxesCollectedByColor(LightManager.MyLight.Green) + "/" +
                          BoxManager.Instance.CountTotalBoxesByColor(LightManager.MyLight.Green);

        _purpleData.text = "Purple Total Points: " +
                           _purplePointsCollected + "/" +
                           BoxManager.Instance.CountTotalPointsByColor(LightManager.MyLight.Purple) +
                           Environment.NewLine +
                           "Purple LightBoxes Collected: " +
                           BoxManager.Instance.CountTotalBoxesCollectedByColor(LightManager.MyLight.Purple) + "/" +
                           BoxManager.Instance.CountTotalBoxesByColor(LightManager.MyLight.Purple);
        
        _redData.text = "Red Total Boxes Collected: " +
                        BoxManager.Instance.CountTotalRedBoxes();

    }
    
    [Serializable]
    public class MyBots
    {
        public BotsNames botName;
        public Image image;
        public TMP_Text time;

        public void Set(bool b)
        {
            image.gameObject.SetActive(b);
            time.gameObject.SetActive(b);
        }
    }
    
}
