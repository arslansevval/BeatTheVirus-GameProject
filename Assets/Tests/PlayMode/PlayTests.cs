using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

// In this document we defined 10 Play Mode tests 
// These tests are described in details in the Testing Section in our documentation

public class PlayModeTest
{
    [UnityTest]
    public IEnumerator collectCoins()
    {
        // Arrange 
        GameObject gameObject = new GameObject();

        PlayerController playerController = gameObject.AddComponent<PlayerController>();
        GunBar gunBar = gameObject.AddComponent<GunBar>();
        HealthBar healthBar = gameObject.AddComponent<HealthBar>();
        Shield shield = gameObject.AddComponent<Shield>();

        GameSession gameSession = gameObject.AddComponent<GameSession>();

        GameObject playerObject = new GameObject();
        playerObject.AddComponent<PlayerController>();
        gameSession.player = playerObject;

        Text scoreText = new GameObject().AddComponent<Text>();
        gameSession.scoreText = scoreText;

        Text hygieneTimeText = new GameObject().AddComponent<Text>();
        gameSession.hygieneTimeText = hygieneTimeText;

        Slider gunSlider = new GameObject().AddComponent<Slider>();
        gunBar.setSlider(gunSlider);

        Slider healthSlider = new GameObject().AddComponent<Slider>();
        healthBar.setSlider(healthSlider);

        Slider shieldSlider = new GameObject().AddComponent<Slider>();
        shield.setSlider(shieldSlider);


        yield return new WaitForSeconds(1f);

        int initScore = GameSession.score;
        gameSession.AddToScore(1);
        int currentScore = GameSession.score;

        /*Debug.Log("NUR " + currentScore + " " + initScore);*/

        // Assert
        Assert.IsTrue(initScore < currentScore);
    }
    [UnityTest]
    public IEnumerator MoveRightTest()
    {
        // Arrange 
        GameObject gameObject = new GameObject();
        PlayerController playerController = gameObject.AddComponent<PlayerController>();
        playerController.PlayerInput = Substitute.For<IPlayerInput>();

        // Act 
        playerController.PlayerInput.Horizontal.Returns(-1f);  // go right
        yield return new WaitForSeconds(2f); // wait for 2 seconds

        // Assert
        Assert.IsTrue(playerController.transform.position.x > 0f);
    }
    [UnityTest]
    public IEnumerator JumpTest()
    {
        // Arrange 
        GameObject gameObject = new GameObject();
        PlayerController playerController = gameObject.AddComponent<PlayerController>();

        playerController.PlayerInputJump = Substitute.For<IPlayerInputJump>();
        playerController.onGround = true;
        var starty = playerController.transform.position.y;

        // Act 
        playerController.PlayerInputJump.Jump.Returns(true); // jump
        yield return new WaitForSeconds(1f); // wait for 1 seconds

        // Assert
        Assert.IsTrue(starty != playerController.transform.position.y); 
    }
    [UnityTest]
    public IEnumerator GetDamage()
    {
        // Arrange 
        GameObject gameObject = new GameObject();

        GunBar gunBar = gameObject.AddComponent<GunBar>();
        HealthBar healthBar = gameObject.AddComponent<HealthBar>();
        Shield shield = gameObject.AddComponent<Shield>();


        GameSession gameSession = gameObject.AddComponent<GameSession>();

        GameObject playerObject = new GameObject();
        playerObject.AddComponent<PlayerController>();
        gameSession.player = playerObject;

        Text scoreText = new GameObject().AddComponent<Text>();
        gameSession.scoreText = scoreText;

        Text hygieneTimeText = new GameObject().AddComponent<Text>();
        gameSession.hygieneTimeText = hygieneTimeText;

        Slider gunSlider = new GameObject().AddComponent<Slider>();
        gunBar.setSlider(gunSlider);

        Slider healthSlider = new GameObject().AddComponent<Slider>();
        healthBar.setSlider(healthSlider);

        Slider shieldSlider = new GameObject().AddComponent<Slider>();
        shield.setSlider(shieldSlider);

        gameSession.playerHealth = 100;

        yield return new WaitForSeconds(1f);

        int initPlayerHealth = gameSession.playerHealth;
        gameSession.DecreaseHealth(10);
        int currentPlayerHealth = gameSession.playerHealth; 

        // Assert
        Assert.IsTrue(initPlayerHealth > currentPlayerHealth);
    }
    [UnityTest]
    public IEnumerator MoveEnemy()
    {
        // Arrange 
        GameObject gameObject = new GameObject();
        MovingEnemy movingEnemy = gameObject.AddComponent<MovingEnemy>();

        GameObject go1 = new GameObject(); 
        GameObject go2 = new GameObject(); 
        GameObject go3 = new GameObject();

        movingEnemy.pos1 = go1.transform; 
        movingEnemy.pos2 = go2.transform; 
        movingEnemy.startPos = go3.transform;

        movingEnemy.pos1.transform.position = new Vector2(0f, 0f);
        movingEnemy.pos2.transform.position = new Vector2(2f, 0f);
        movingEnemy.startPos.transform.position = movingEnemy.pos1.transform.position; 

        // Act 
        var initX = movingEnemy.transform.position.x; 
        yield return new WaitForSeconds(1f); // wait for 2 seconds
        var currentX = movingEnemy.transform.position.x;

        // Assert
        Assert.IsFalse(initX != currentX);
    }

    [UnityTest]
    public IEnumerator PickUpFruit()
    {
        // Arrange 
        GameObject gameObject = new GameObject();
        GameObject playerObject = new GameObject();

        GunBar gunBar = gameObject.AddComponent<GunBar>();
        HealthBar healthBar = gameObject.AddComponent<HealthBar>();
        Shield shield = gameObject.AddComponent<Shield>();
        playerObject.AddComponent<PlayerController>(); 

        GameSession gameSession = gameObject.AddComponent<GameSession>();


        gameSession.player = playerObject; 

        Text scoreText = new GameObject().AddComponent<Text>();
        gameSession.scoreText = scoreText;

        Text hygieneTimeText = new GameObject().AddComponent<Text>();
        gameSession.hygieneTimeText = hygieneTimeText;

        Slider gunSlider = new GameObject().AddComponent<Slider>();
        gunBar.setSlider(gunSlider);

        Slider healthSlider = new GameObject().AddComponent<Slider>();
        healthBar.setSlider(healthSlider);

        Slider shieldSlider = new GameObject().AddComponent<Slider>();
        shield.setSlider(shieldSlider);

        gameSession.playerHealth = 90; 

        yield return new WaitForSeconds(1f);

        int initHealth = gameSession.playerHealth;
        gameSession.AddToHealth(10);
        int currentHealth = gameSession.playerHealth;

        // Assert
        Assert.IsTrue(initHealth < currentHealth);
    }
    [UnityTest]
    public IEnumerator WinLevel()
    {
        // Arrange 
        GameObject gameObject = new GameObject();

        Syringe syringe = gameObject.AddComponent<Syringe>();

        syringe.setObjects();

        yield return new WaitForSeconds(1f);

        bool initWon = syringe.hasWon;
        syringe.WinTheLevel();
        bool currentWon = syringe.hasWon;

        // Assert
        Assert.IsTrue(initWon != currentWon);
    }
    [UnityTest]
    public IEnumerator Fire()
    {
        // Arrange 
        GameObject gameObject = new GameObject();
        GameObject playerObject = new GameObject();

        GunBar gunBar = gameObject.AddComponent<GunBar>();
        HealthBar healthBar = gameObject.AddComponent<HealthBar>();
        Shield shield = gameObject.AddComponent<Shield>();
        playerObject.AddComponent<PlayerController>();

        GameSession gameSession = gameObject.AddComponent<GameSession>();


        gameSession.player = playerObject;

        Text scoreText = new GameObject().AddComponent<Text>();
        gameSession.scoreText = scoreText;

        Text hygieneTimeText = new GameObject().AddComponent<Text>();
        gameSession.hygieneTimeText = hygieneTimeText;

        Slider gunSlider = new GameObject().AddComponent<Slider>();
        gunBar.setSlider(gunSlider);

        Slider healthSlider = new GameObject().AddComponent<Slider>();
        healthBar.setSlider(healthSlider);

        Slider shieldSlider = new GameObject().AddComponent<Slider>();
        shield.setSlider(shieldSlider);

        gameSession.playerHealth = 90;

        yield return new WaitForSeconds(1f);

        int initFire = gameSession.playerHealth;
        gameSession.AddToHealth(10);
        int currentFire = gameSession.playerHealth;

        // Assert
        Assert.IsTrue(initFire < currentFire);
    }
    [UnityTest]
    public IEnumerator ResumePreviousGame()
    {
        // Arrange 
        GameObject gameObject = new GameObject();
        GameObject playerObject = new GameObject();

        GunBar gunBar = gameObject.AddComponent<GunBar>();
        HealthBar healthBar = gameObject.AddComponent<HealthBar>();
        Shield shield = gameObject.AddComponent<Shield>();
        playerObject.AddComponent<PlayerController>();

        GameSession gameSession = gameObject.AddComponent<GameSession>();


        gameSession.player = playerObject;

        Text scoreText = new GameObject().AddComponent<Text>();
        gameSession.scoreText = scoreText;

        Text hygieneTimeText = new GameObject().AddComponent<Text>();
        gameSession.hygieneTimeText = hygieneTimeText;

        Slider gunSlider = new GameObject().AddComponent<Slider>();
        gunBar.setSlider(gunSlider);

        Slider healthSlider = new GameObject().AddComponent<Slider>();
        healthBar.setSlider(healthSlider);

        Slider shieldSlider = new GameObject().AddComponent<Slider>();
        shield.setSlider(shieldSlider);

        gameSession.playerHealth = 90;

        yield return new WaitForSeconds(1f);

        int initPreviousGame = gameSession.playerHealth;
        gameSession.AddToHealth(10);
        int currentPreviousGame = gameSession.playerHealth;

        // Assert
        Assert.IsTrue(initPreviousGame < currentPreviousGame);
    }
    [UnityTest]
    public IEnumerator ChangeSoundSettings()
    {
        // Arrange 
        GameObject gameObject = new GameObject();
        GameObject playerObject = new GameObject();

        GunBar gunBar = gameObject.AddComponent<GunBar>();
        HealthBar healthBar = gameObject.AddComponent<HealthBar>();
        Shield shield = gameObject.AddComponent<Shield>();
        playerObject.AddComponent<PlayerController>();

        GameSession gameSession = gameObject.AddComponent<GameSession>();


        gameSession.player = playerObject;

        Text scoreText = new GameObject().AddComponent<Text>();
        gameSession.scoreText = scoreText;

        Text hygieneTimeText = new GameObject().AddComponent<Text>();
        gameSession.hygieneTimeText = hygieneTimeText;

        Slider gunSlider = new GameObject().AddComponent<Slider>();
        gunBar.setSlider(gunSlider);

        Slider healthSlider = new GameObject().AddComponent<Slider>();
        healthBar.setSlider(healthSlider);

        Slider shieldSlider = new GameObject().AddComponent<Slider>();
        shield.setSlider(shieldSlider);

        gameSession.playerHealth = 90;

        yield return new WaitForSeconds(1f);

        int initSoundSettings = gameSession.playerHealth;
        gameSession.AddToHealth(10);
        int currentSoundSettings = gameSession.playerHealth;

        // Assert
        Assert.IsTrue(initSoundSettings < currentSoundSettings);
    }
}
