using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ManagerUI : MonoBehaviour
{
    public GameObject pausedMenu;
    public Image cameraShutDown;
    public float speedFade;

   
   Action controlsMethod;

   private void Start()
    {
        if (pausedMenu!=null)pausedMenu.SetActive(false);
        
        controlsMethod = NormalControls;

        if (cameraShutDown != null)
            cameraShutDown.color = new Color(cameraShutDown.color.r, cameraShutDown.color.g, cameraShutDown.color.b, 0);

    }

    public void OnCliclkSound()
    {
      //  SoundManager.instance.PlayLevel1(SoundManager.Types.Click);
    }

    public void ActivePause()
    {
        pausedMenu.SetActive(true);
    }

    public void InactivePause()
    {
        pausedMenu.SetActive(false);
    }

    public void FadeOn(params object[] parameters)
    {
        StartCoroutine(FadeActive());
    }

    public IEnumerator FadeActive()
    {
        Color fade = cameraShutDown.color;
        fade.a = 1;

        while (cameraShutDown.color.a < 1)
        {
            cameraShutDown.color = Color.Lerp(cameraShutDown.color, fade, speedFade * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private void Update()
    {
        controlsMethod();
    }

    void NormalControls()
    {
        if (pausedMenu == null) return;
        InactivePause();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            controlsMethod = PausedControls;
            ActivePause();
        }
    }

    void PausedControls()
    {if (pausedMenu == null) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
            controlsMethod = NormalControls;
            InactivePause();
        }
    }
}