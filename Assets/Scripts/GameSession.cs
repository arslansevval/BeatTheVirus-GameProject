using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] public Text scoreText;
    [SerializeField] public Text hygieneTimeText;

    [SerializeField] public int gunAmmo = 10;
    [SerializeField] public static int score = 0;
    [SerializeField] public int playerHealth = 100;

    public GunBar gunBar;
    public Shield shield;
    public HealthBar healthBar;
    [SerializeField] public GameObject player;
    public void InitGameSession()
    {
        gunBar = FindObjectOfType<GunBar>();
        shield = FindObjectOfType<Shield>();
        healthBar = FindObjectOfType<HealthBar>();

    }

    // ============================================== Variables 

    // Hygiene Products 
    bool isHygienePEnabled = false;
    float hygienePEffectRetio = 1f;
    int currentHygieneTime = -1;

    // ============================================== Overrides 

    // Start is called before the first frame update
    void Start()
    {
        InitGameSession();
        scoreText.text = score.ToString();
        hygieneTimeText.gameObject.SetActive(false);
        gunBar.UbdateGunBar(gunAmmo);
        healthBar.UpdateHealthBar(playerHealth);
        shield.DeactivateShieldBar();
        player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        /*Time.timeScale = 0.1f;*/
        if (isHygienePEnabled)
        {
            hygieneTimeText.gameObject.SetActive(true);
            hygieneTimeText.text = currentHygieneTime.ToString();
        } else // hide the shield bar if there is no shield. 
        {
            hygieneTimeText.gameObject.SetActive(false);
        }
    }

    // ============================================== Public mehtodes 

    // Increase the score of the player by the amount "amount"
    public void AddToScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    // Increase the health of the player the amount "amount"
    public void AddToHealth(int amount)
    {
        if (playerHealth >= 100)
        {
            return;
        } else if (playerHealth + amount >= 100)
        {
            playerHealth = 100;
        } else
        {
            playerHealth += amount;
        }
        healthBar.UpdateHealthBar(playerHealth);
    }

    // increases the gun ammo of the player 
    public void AddToGun(int amount)
    {
        gunAmmo += amount;
        gunBar.UbdateGunBar(gunAmmo);
    }

    public bool DecreaseAmmo()
    {
        if (gunAmmo > 0)
        {
            gunAmmo -= 1;
            gunBar.UbdateGunBar(gunAmmo);
            return false;
        }
        return true;
    }

    // decreases the health of the player 
    // return false if the health is more than 0
    // return ture if the health is less than or equal to 0
    public bool DecreaseHealth(int amount)
    {
        if (isHygienePEnabled)
        {
            amount = (int)(amount * hygienePEffectRetio);
        }
        playerHealth -= amount;
        Debug.Log("Y koordinatý: " + player.transform.position.y);
        if (player.transform.position.y <= -10)
        {
            Debug.Log("Y koordinatý ifin içersi: " + player.transform.position.y);
            healthBar.UpdateHealthBar(0);
            return true;
        }
        if (playerHealth <= 0)
        {
            healthBar.UpdateHealthBar(0);
            return true;
        } else
        {
            healthBar.UpdateHealthBar(playerHealth);
            return false;
        }
    }

    // enables the shield, which will decrease the the power of the
    // enemies by retio of "ratio" for "time"
    public void EnableHygieneP(float ratio, int time)
    {
        // if enabled -> there is already a shield -> don't start another coroutine and just extend the time.  
        if (isHygienePEnabled)
        {
            hygienePEffectRetio = ratio;
            currentHygieneTime = time;
        } else
        {
            isHygienePEnabled = true;
            hygienePEffectRetio = ratio;
            StartCoroutine(CountHygienePTime(time));
        }

    }

    // coroutine to count the time of the current shield
    IEnumerator CountHygienePTime(int time)
    {
        currentHygieneTime = time;
        shield.ActivateShieldBar();
        while (currentHygieneTime > 0)
        {
            shield.UpdateShieldBar(currentHygieneTime);
            yield return new WaitForSeconds(1);
            currentHygieneTime -= 1;
        }
        isHygienePEnabled = false;
        currentHygieneTime = -1;
        hygienePEffectRetio = 1f;
        shield.DeactivateShieldBar();
    }

    internal void ProcessPlayerDeath()
    {
        StartCoroutine(DeathRoutine(3.5f)); 
    }

    IEnumerator DeathRoutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
        score = 0;
        Destroy(gameObject);
    }
}
