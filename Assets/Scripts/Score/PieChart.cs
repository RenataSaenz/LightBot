using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieChart : MonoBehaviour
{
    [SerializeField] private Image[] imagesPieChart;
    
    

    public void SetValues(List<float> valuesToSet)
    {
        float totalValues = 0;
        
        for (int i = 0; i < imagesPieChart.Length; i++)
        {
            totalValues += FindPercentage(valuesToSet,i);
            imagesPieChart[i].fillAmount = totalValues;
        }
    }

    private float FindPercentage(List<float> valueToSet, int index)
    {
        float totalAmount = 0;
        
        for (int i = 0; i < valueToSet.Count; i++)
        {
            totalAmount += valueToSet[i];
        }

        return valueToSet[index] / totalAmount;
    }
}
