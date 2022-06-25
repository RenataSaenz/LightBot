using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsContoller
{
    public static int totalScore;
    public static string playerName;

   /* public static void SaveName(string playerName)
    {
        
    }*/
    public static void SumScore(int score)
    {
        totalScore += score;
    }
    

}
