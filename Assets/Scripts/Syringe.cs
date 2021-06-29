using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Syringe : MonoBehaviour
{
    bool isColliding = false;
    public bool hasWon = false;

    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject FahrettinPanel;
    [SerializeField] GameObject GunBar;
    [SerializeField] GameObject HealtBar;
    [SerializeField] GameObject ShieldBar;
    [SerializeField] AudioClip starSound, twostarSound, threestarSound;
    [SerializeField] AudioSource starAudio;
    
    public void setObjects()
    {
        WinPanel = new GameObject();
        GunBar = new GameObject();
        HealtBar = new GameObject();
        ShieldBar = new GameObject();
    }
    

    //when the player reaches the syringe, it means completing the level, winpanel is activated
    private void OnTriggerEnter2D(Collider2D collision)
    {
        WinTheLevel();
        StarSoundEffect();
    }

    public void WinTheLevel()
    {
        if (isColliding) return;
        isColliding = true;

        hasWon = true; 

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            FahrettinPanel.SetActive(true);
            GunBar.SetActive(false);
            HealtBar.SetActive(false);
            ShieldBar.SetActive(false);

        } else
        {
            WinPanel.SetActive(true);
            GunBar.SetActive(false);
            HealtBar.SetActive(false);
            ShieldBar.SetActive(false);
        }



        Time.timeScale = 0f;

        Destroy(gameObject);
    }

    private void Update()
    {
        Time.timeScale = 1f;
        isColliding = false;
    }
    public void StarSoundEffect()
    {
        if (GameSession.score > 0)
        {
            starAudio.PlayOneShot(starSound);

        }

        if (GameSession.score >= 10 && GameSession.score < 30)
        {

            starAudio.PlayOneShot(twostarSound);

        }

        if (GameSession.score >= 30)
        {
            starAudio.PlayOneShot(threestarSound);

        }
    }
  





}
