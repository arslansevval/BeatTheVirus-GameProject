using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int sceneToContinue;
    private int currentSceneIndex;

    //Next Level
   public void nextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //Restart Level
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    //Open Level 1
    public void openLevel1()
    {
        
        SceneManager.LoadScene("Level1");
    }
    //Open Level 2
    public void openLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
    //Open Level 3
    public void openLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
    //To Do
    public void resumeGame()
    {
        //sceneToContinue = PlayerPrefs.GetInt("SavedScene");

        //if(sceneToContinue != 0)
        //{
        //    SceneManager.LoadScene(sceneToContinue);
        //}
        //else
        //{
        //    return;
        //}
    }
    //Load Main Menu
    public void LoadMainMenu()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
        SceneManager.LoadScene(0);
    }
    //Exit Game
    public void exitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }


}
