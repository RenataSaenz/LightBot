using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


//IA2-P1
public class ScoreManager : MonoBehaviour
{
     public CollectableObjects collectableObjects;

    public static ScoreManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public int CountTotalRedBoxes()
    {
        var boxes = collectableObjects.lightBoxes.
            Count(x =>  x.red);
        return boxes;
    }
    public int CountTotalPointsByColor(LightManager.MyLight lightType)
    {
        var points = collectableObjects.lightBoxes.
             Where(x =>  x.typeOfLight == lightType).
             Select(x => x.points).
             Aggregate(0, (acum, current) => acum + current);
        
        var robots = collectableObjects.robots.
            Where(x =>  x.typeOfLight == lightType).
            Select(x => x.points).
            Aggregate(0, (acum, current) => acum + current);

        var total = points + robots;
        return total;
    }
    public int CountTotalBoxesByColor(LightManager.MyLight lightType)
    {
        var boxes = collectableObjects.lightBoxes.
                        Count(x =>  x.typeOfLight == lightType);
        return boxes;
    }
    public int CountTotalBoxesCollectedByColor(LightManager.MyLight lightType)
    {
        var  boxes = collectableObjects.lightBoxes.
                        Where(x =>  !x.red && x.typeOfLight == lightType).
                        Count(x => x.collected);
        return boxes;
    }
    public IEnumerable<LightManager.MyLight> GetAllColorsCollected()
    {
        var lightB = collectableObjects.lightBoxes.Where(x => !x.red && x.collected).Select(x => x.typeOfLight);
        var specialB = collectableObjects.robots.Where(x => x.collected).Select(x => x.typeOfLight);
        var colors = lightB.Concat(specialB);
        return colors;
    }
    public IEnumerable<int> GetAllPointsCollectedByColor(LightManager.MyLight lightType)
    {
        var points = collectableObjects.lightBoxes
            .Where(x => x.typeOfLight == lightType && x.collected)
            .Select(x => x.points);
        
        var robots = collectableObjects.robots
            .Where(x => x.typeOfLight == lightType && x.collected)
            .Select(x => x.points);

        var total = points.Concat(robots);
        return total;
    }
    public int CountTotalBoxes()
    {
        var boxes = collectableObjects.lightBoxes.
            Count();
        return boxes;
    }
    public bool BoxAchievement(int minute)
    {
        int boxes = collectableObjects.lightBoxes.
            Where(x => x.collected && !x.red).
            TakeWhile(x => x.timeCollected <= minute).Count();

        if (boxes >= CountTotalBoxes()) return true;
        
        return false;
    }

    public IEnumerable<string> GetTimeRobots()
    {
        var robots = collectableObjects.robots
            .Where(x => x.collected)
            .Select(x => x.name);
        var time = collectableObjects.robots
            .Where(x => x.collected)
            .Select(x => x.timeCollected);

        var robotAndTime = robots.Zip(time, (a, b) => a + " Collected " + b);

        return robotAndTime;
    }
    
    public IEnumerable<BotsNames> GetRobots()
    {
        var robots = collectableObjects.robots
            .Where(x => x.collected).Select(x => x.name);

        return robots;
    }
    
}   

[Serializable]
public class CollectableObjects
{
    public List<LightBox> lightBoxes = new List<LightBox>();
    public List<RescueRobot> robots = new List<RescueRobot>();

    public void AddRobot(RescueRobot box)
    {
        robots.Add(box);
    }

    public void AddLightBox(LightBox box)
    {
        lightBoxes.Add(box);
    }

}

