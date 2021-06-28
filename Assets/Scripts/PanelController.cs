using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject star1;
    [SerializeField] GameObject star2;
    [SerializeField] GameObject star3;

    //a new Static variable to hold our score.
    private static int score = GameSession.score;

    //next level button on WinPanel
    //opens the scene of the next level from the current level
    public void NextLevelButton()
    {
         Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //close button on WinPanel
    //return to start menu
    //we can use it LosePanel
    public void CloseButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
        //Time.timeScale = 0;
    }

    //resart button on WinPanel
    //reopens the current level 
    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Checking stars by score
    public void StarManager()
    {
        if (score == 0)
        {
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);
        }
        if (score > 0)
        {
            star1.SetActive(true);
            star2.SetActive(false);
            star3.SetActive(false);
        }

        if (score > 10 && score < 30)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(false);
        }

        if (score > 30)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
    }
    //The code block we can use if we want to show the highest score as in the documentation
    public void HighScore()
    {
        if (score> PlayerPrefs.GetInt("highscore", 0))
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }



    //Level3
    public void StartOverButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }



}

