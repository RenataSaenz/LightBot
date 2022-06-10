using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public ManagerUI managerUI;

    [SerializeField]
    private float _timeForTransition;

    private void Start()
    {
    //     EventManager.Subscribe("GameOver", YouLost);
    //     EventManager.Subscribe("GameWon", YouWon);
    }

    public void Resume()
    {
        managerUI.InactivePause();
        Time.timeScale = 1f;
    }

    public void YouWon(params object[] parameters)
    {
        SceneManager.LoadScene("YouWon");
       // ScoreManager.Instance.PointsContoller();
    }

    public void YouLost(params object[] parameters)
    {
        StartCoroutine(WaitForLoadScene(2));
    }
    IEnumerator WaitForLoadScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("YouLost");
    }

    public void PlayLevel1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    } 
    public void PlayLevel2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    } 
    public void PlayLevel3()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level3");
    } 
    public void PlayLevel4()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level4");
    } 
    public void Tutorial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tutorial");
    } 
    
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
