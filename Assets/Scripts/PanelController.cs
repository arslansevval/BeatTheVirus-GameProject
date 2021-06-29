using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject FahrettinPanel;
    [SerializeField] GameObject star1;
    [SerializeField] GameObject star2;
    [SerializeField] GameObject star3;
    //a new Static variable to hold our score.

    [SerializeField]  GameSession score;
    void Start()
    {
        //score = GameObject.Find("GameSessionscore").GetComponent<GameSession>();
        score = GetComponent<GameSession>();
    }

    void Update() {
        if (GameSession.score > 0)
        {
            StartCoroutine(Wait());

        }

        if (GameSession.score > 10 && GameSession.score < 30)
        {
            StartCoroutine(Waitiki());
        }

        if (GameSession.score >=30)
        {
            StartCoroutine(Waitüc());
        }
    }

    IEnumerator Wait()
    {
        
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        yield return new WaitForSecondsRealtime(0.7f);

        star3.SetActive(true);
    }

    IEnumerator Waitiki()
    {

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        yield return new WaitForSecondsRealtime(0.7f);
        star3.SetActive(true);

        yield return new WaitForSecondsRealtime(0.7f);
        star2.SetActive(true);
    }

    IEnumerator Waitüc()
    {

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        yield return new WaitForSecondsRealtime(0.7f);
        star1.SetActive(true);

        yield return new WaitForSecondsRealtime(0.7f);
        star2.SetActive(true);

        yield return new WaitForSecondsRealtime(0.7f);
        star3.SetActive(true);
    }

    //next level button on WinPanel
    //opens the scene of the next level from the current level
    public void NextLevelButton()
    {
         Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameSession.score = 0;
    }

    public void StartOverButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameSession.score = 0;
    }

    //close button on WinPanel
    //return to start menu
    //we can use it LosePanel
    public void CloseButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
        GameSession.score = 0;
    }

    //resart button on WinPanel
    //reopens the current level 
    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameSession.score = 0;
    }

    public void SkipButton()
    {
        Time.timeScale = 0f;
        FahrettinPanel.SetActive(false);
        WinPanel.SetActive(true);
        GameSession.score = 0;

    }










}

