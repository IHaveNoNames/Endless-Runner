using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject player;

    
    public int bossTotalHeath = 10;
    [HideInInspector]
    public int bossCurrentHealth;
    [HideInInspector]
    public static bool bossFightActive = false;

    float minigameDistance = 100;

    public static int percentObstacle = 50;
    public static int percentCar = 50;
    public static int percentBarrier = 50;

    public static int percentPowerup = 30;
    public static int percentVest = 40;
    public static int percentMagnet = 60;

    public Text coinText;
    public Text distanceText;


    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if(GameStatus.distanceTravelled % minigameDistance == 0) //for every 100 metres
        {
            float n = Random.Range(0, 2);
            if (n == 0)
            {
                QTEStart();
            } 

            else if (n == 1)
            {
                BossFightStart();
            }

            IncreaseDifficulty();
        }

        PowerUpCountDown();

        if (bossCurrentHealth <= 0 && bossFightActive == true)
        {
            BossDefeated();
        }
        UpdateUIText();
        
    }

    

    void PowerUpCountDown()
    {
        GameStatus.vestRemaining -= Time.deltaTime;
        GameStatus.magnetRemaining -= Time.deltaTime;
        if (GameStatus.vestRemaining <= 0f)
        {
            GameStatus.vest = false;
        }
        if (GameStatus.magnetRemaining <= 0f)
        {
            GameStatus.magnet = false;
        }
    }

    void UseMagicWind()
    {

    }

    public void TakingFatalHit()   //when player is hit, either function will be called
    {
        if (GameStatus.vest == false)
        {
            //gameover event
            print("GameOVer");
        }
        else
        {
            print("Protected");
            GameStatus.vest = false;
        }
    }
    public void TakingLightHit()
    {
        if (GameStatus.vest == false)
        {
            //police catches up
        }
        else
        {
            GameStatus.vest = false;
        }

    }

    public void BossDefeated()
    {
        {
            bossFightActive = false;
            //boss defeated
            //disable all the grenades
        }
    }

    public void BossFightStart()
    {
        bossCurrentHealth = bossTotalHeath;
        bossFightActive = true;
    }

    void QTEStart()
    {

    }

    void DefaultValues()
    {
        percentCar = 50;
        percentBarrier = 50;

        percentVest = 40;
        percentMagnet = 60;
    }

    void IncreaseDifficulty()
    {
        Player.acceleration += 5f;
        if(percentObstacle != 100)
        {
            percentObstacle += 10;
        }
    }

    void UpdateUIText()
    {
        float currentDistance = Mathf.RoundToInt(GameStatus.distanceTravelled);
        coinText.text = "Coin:" + GameStatus.coinCollected.ToString();
        distanceText.text = currentDistance.ToString();

    }
}
