using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour
{
    bool isColliding = false;

    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject GunBar;
    [SerializeField] GameObject HealtBar;
    [SerializeField] GameObject ShieldBar;


    //when the player reaches the syringe, it means completing the level, winpanel is activated
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding) return;
        isColliding = true;
        
        
        WinPanel.SetActive(true);
        GunBar.SetActive(false);
        HealtBar.SetActive(false);
        ShieldBar.SetActive(false);

        Time.timeScale = 0f;

        Destroy(gameObject);
    }

    private void Update()
    {
        Time.timeScale = 1f;
        isColliding = false;
    }
}
