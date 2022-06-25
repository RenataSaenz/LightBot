using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoxManager : MonoBehaviour
{
    public List<CollectableObjects> objectsCollected = new List<CollectableObjects>();  //new list depending if collected in yeelow, white, grey, purple, etc

    public static BoxManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        objectsCollected.Add(new CollectableObjects());
    }

    public int TotalPointsInAllGame()
    {
        return default;
    }
    public int TotalBoxInAllGame()
    {
        return default;
    }
    public int SpecialPoints()
    {
        return default;
    }
    public int CountTotalPointsByColor(LightManager.MyLight lightType)
    {
        var points = objectsCollected.SelectMany(x => x.lightBoxes).
            Where(x =>  x.typeOfLight == lightType).
            Select(x => x.points).
            Aggregate(0, (acum, current) => acum + current);
        return points;
    }

    public int CountTotalPointsCollectedByColor(LightManager.MyLight lightType)
    {
        var points = objectsCollected.SelectMany(x => x.lightBoxes).
                        Where(x =>  x.typeOfLight == lightType && x.collected).
                        Select(x => x.points).
                        Aggregate(0, (acum, current) => acum + current);
        return points;
    }
    
    public int CountTotalBoxesByColor(LightManager.MyLight lightType)
    {
        var boxes = objectsCollected.SelectMany(x => x.lightBoxes).
                        Count(x =>  x.typeOfLight == lightType);
        return boxes;
    }
    
    public int CountTotalBoxesCollectedByColor(LightManager.MyLight lightType)
    {
        var  boxes = objectsCollected.SelectMany(x => x.lightBoxes).
                        Where(x =>  !x.red && x.typeOfLight == lightType).
                        Count(x => x.collected);
        return boxes;
    }

    public int CountTotalPointsCollected()
    {
        var points = objectsCollected.SelectMany(x => x.lightBoxes).
                        Where(x => x.collected).
                        Select(x => x.points).
                        Aggregate(0, (acum, current) => acum + current);
        return points;
    }
    
    public bool BoxAchievement(int minute)
    {
        int boxes = objectsCollected.SelectMany(x => x.lightBoxes).
            Where(x => x.collected && !x.red).
            TakeWhile(x => x.timeCollected <= minute).Count();

        if (boxes >= CountTotalBoxes()) return true;
        
        return false;
    }
    
    public int CountTotalPoints()
    {
        var points = objectsCollected.SelectMany(x => x.lightBoxes).
                        Select(x => x.points).
                        Aggregate(0, (acum, current) => acum + current);
        return points;
    }

    public int CountTotalRedBoxes()
    {
        var boxes = objectsCollected.SelectMany(x => x.lightBoxes).
            Count(x =>  x.red);
        return boxes;
    }
    
    
    public int CountTotalBoxesCollected()
    {
        var boxes = objectsCollected.SelectMany(x => x.lightBoxes).
                        Where(x =>  !x.red).
                        Count(x => x.collected);
        return boxes;
    }
    
    public int CountTotalBoxes()
    {
        var boxes = objectsCollected.SelectMany(x => x.lightBoxes).
            Count();
        return boxes;
    }

 
    
}   

[Serializable]
public class CollectableObjects
{
    public List<LightBox> lightBoxes = new List<LightBox>();
    public List<SpecialBox> specialBoxes = new List<SpecialBox>();

    public void AddSpecialBox(SpecialBox box)
    {
        specialBoxes.Add(box);
    }

    public void AddLightBox(LightBox box)
    {
        lightBoxes.Add(box);
    }

}

