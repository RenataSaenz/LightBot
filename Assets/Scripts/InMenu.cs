using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InMenu : MonoBehaviour
{
    public GameObject goToMenu;
    public GameObject actualMenu;
    public void OnClick()
    {
        goToMenu.SetActive(true);
        actualMenu.SetActive(false);
    }
}