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
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
       
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
        currentSceneIndex = 1;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
        
    }
    //Open Level 2
    public void openLevel2()
    {
        SceneManager.LoadScene("Level2");
        currentSceneIndex = 2;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
    }
    //Open Level 3
    public void openLevel3()
    {
        SceneManager.LoadScene("Level3");
        currentSceneIndex = 3;
        PlayerPrefs.SetInt("SavedScene", currentSceneIndex);
    }
    //To Do
    public void resumeGame()
    {
       
        sceneToContinue = PlayerPrefs.GetInt("SavedScene");

       
        if (sceneToContinue != 0)
        {
            SceneManager.LoadScene(sceneToContinue);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
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
       
        Application.Quit();
    }


}
