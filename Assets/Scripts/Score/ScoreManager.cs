using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
  //private List<string> nickname = new List<string>();
 // string[] nicknames= new string[] { "A", "B", "C", "D", "E" };
 //int[] score= new int[] { 1, 2, 3 };

  public void SetHighScore(string[] nicknames, int[] score)
  {
      var set = nicknames.Zip(score, (n, s) => n + s.ToString());
    foreach (var s in set)
      Debug.Log(s);
  }

  public void OrderScore()
  {
      
  }
}
