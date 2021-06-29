using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject QuitPopUpUI;


    //When pressing the ESC key
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    //when the player wants to resume the game
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    //when the player wants to pause the game
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //when the player wants to exit the game
    public void QuitGame()
    {
        Debug.Log("You have quit the game");
        PauseMenuUI.SetActive(false);
        QuitPopUpUI.SetActive(true);
    }

    //when the player is sure to exit the game
    public void YesButton()
    {
        SceneManager.LoadScene("StartMenu");

    }

    //when the player wants to return to the game
    public void NoButton()
    {
        QuitPopUpUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

}
