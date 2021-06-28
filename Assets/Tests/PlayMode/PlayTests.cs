using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI; 
public class PlayerMovingTest
{
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

    /*[UnityTest]*/
    /*public IEnumerator FireTest()
    {
        // Arrange 
        GameObject gameObject = new GameObject();
        PlayerController playerController = gameObject.AddComponent<PlayerController>();

        //Set up gunbar 
        GameSession gameSession = gameObject.AddComponent<GameSession>();
        GunBar gunBar = gameObject.AddComponent<GunBar>();

        // Set up Slider 
        GameObject s = new GameObject("Slider", typeof(RectTransform));
        Slider slider = s.AddComponent<Slider>();
        gunBar.slider = slider; 

        
        // Set up Healthbar 
        HealthBar healthBar = gameObject.AddComponent<HealthBar>();
        healthBar.slider = slider;

        // Set up Shield
        Shield shield = gameObject.AddComponent<Shield>();
        shield.slider = slider;

        // create drag and drops 
        Fire fire = gameObject.AddComponent<Fire>();
        Transform fire_point = new GameObject().transform; 
        playerController.fire = fire;
        playerController.fire_point = fire_point;
        GameObject fires = new GameObject("Fires");

        // Set up hygieneTimeText
        Text hygieneTimeText = new GameObject().AddComponent<Text>();
        gameSession.hygieneTimeText = hygieneTimeText;

        // Set up scoreText 
        GameObject newText = new GameObject("text", typeof(RectTransform));
        Text scoreText = newText.AddComponent<Text>();
        gameSession.scoreText = scoreText;

        yield return new WaitForSeconds(1f);

        // Set up fire Input 
        playerController.PlayerInputFire = Substitute.For<IPlayerInputFire>();

        // Act 
        playerController.PlayerInputFire.Fire.Returns(true);
        yield return new WaitForSeconds(1f); // wait for 1 seconds

        // Assert
        Assert.IsTrue(fire_point.transform.position.x != fire.transform.position.x);
    }*/

    [UnityTest]
    public IEnumerator GetDamage()
    {
        // Arrange 
        GameObject gameObject = new GameObject();

        GunBar gunBar = gameObject.AddComponent<GunBar>();
        HealthBar healthBar = gameObject.AddComponent<HealthBar>();
        Shield shield = gameObject.AddComponent<Shield>();

        GameSession gameSession = gameObject.AddComponent<GameSession>();

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
    public IEnumerator collectCoins()
    {
        // Arrange 
        GameObject gameObject = new GameObject();

        PlayerController playerController = gameObject.AddComponent<PlayerController>();
        GunBar gunBar = gameObject.AddComponent<GunBar>();
        HealthBar healthBar = gameObject.AddComponent<HealthBar>();
        Shield shield = gameObject.AddComponent<Shield>();

        GameSession gameSession = gameObject.AddComponent<GameSession>();

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
    public IEnumerator otherTest()
    {
        // Arrange 
        GameObject gameObject = new GameObject();

        PlayerController playerController = gameObject.AddComponent<PlayerController>();
        GunBar gunBar = gameObject.AddComponent<GunBar>();
        HealthBar healthBar = gameObject.AddComponent<HealthBar>();
        Shield shield = gameObject.AddComponent<Shield>();

        GameSession gameSession = gameObject.AddComponent<GameSession>();

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

        // Assert
        Assert.IsTrue(initScore < currentScore);
    }

}
