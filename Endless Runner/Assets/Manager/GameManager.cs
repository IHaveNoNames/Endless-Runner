using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    private Platform platform;

    
    public int bossTotalHeath = 10;
    [HideInInspector]
    public int bossCurrentHealth;
    [HideInInspector]
    public static bool bossFightActive = false;

    float minigameDistance = 100;

    public static int oneObstaclePercent = 50;
    public static int twoObstaclesPercent = 40;
    public static int threeObstaclesPercent = 10;

    public static int percentPowerup = 30;
    public static int percentVest = 40;
    public static int percentMagnet = 60;

    public Text coinText;
    public Text distanceText;
    public GameObject wandButton;
    public GameObject magnetImage;
    public GameObject vestImage;


    // Use this for initialization
    void Start()
    {
        platform = GameObject.Find("Game Manager").GetComponent<Platform>();

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
        DisplayPowerUps();
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

    public void UseMagicWind()
    {
        platform.DestroyAllObstacles();
        GameStatus.magicWand = false;
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
        oneObstaclePercent = 50;
        twoObstaclesPercent = 40;
        threeObstaclesPercent = 10;

        percentVest = 40;
        percentMagnet = 60;
    }

    void IncreaseDifficulty()
    {
        Player.acceleration += 5f;

        //obstacle
    }

    void UpdateUIText()
    {
        float currentDistance = Mathf.RoundToInt(GameStatus.distanceTravelled);
        coinText.text = "Coin:" + GameStatus.coinCollected.ToString();
        distanceText.text = currentDistance.ToString();

    }

    void DisplayPowerUps()
    {
        if (GameStatus.vest)
        {
            vestImage.SetActive(true);
        }
        else
        {
            vestImage.SetActive(false);
        }
        if (GameStatus.magnet)
        {
            magnetImage.SetActive(true);
        }
        else
        {
            magnetImage.SetActive(false);
        }
        if (GameStatus.magicWand)
        {
            wandButton.SetActive(true);
        }
        else
        {
            wandButton.SetActive(false);
        }
    }
}
