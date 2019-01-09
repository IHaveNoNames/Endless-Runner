using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    private Platform platform;
    public Camera mainCamera;
    public Chaser chaser;

    
    public int bossTotalHeath = 10;
    [HideInInspector]
    public int bossCurrentHealth;
    [HideInInspector]
    public static bool bossFightActive = false;

    float minigameDistance = 100;
    int powerupDistance = 200;

    public static int oneObstaclePercent = 35;
    public static int twoObstaclesPercent = 45;
    public static int threeObstaclesPercent = 20;

    public static int percentMagicWand = 10;
    public static int percentVest = 20;
    public static int percentMagnet = 30;

    public static bool readyToSpawn = true; //put in game manager when cross 500m.

    public Text coinText;
    public Text distanceText;
    public GameObject wandButton;
    public GameObject magnetImage;
    public GameObject vestImage;
    public Image vestFillBar;
    public Image magnetFillBar;
    public Text coinTextGameOver;
    public Text distanceTextGameOver;
    public GameObject gameOverCanvas;
    public GameObject mainCanvas;


    // Use this for initialization
    void Start()
    {
        platform = GameObject.Find("Game Manager").GetComponent<Platform>();
        GameStatus.vest = false;
        GameStatus.vestRemaining = 0f;
        GameStatus.magicWand = false;
        GameStatus.magnet = false;
        GameStatus.magnetRemaining = 0f;
        GameStatus.coinCollected = 0;
        GameStatus.distanceTravelled = 0f;


    }

    // Update is called once per frame
    void Update()
    {
        SpawnPowerups();

        //if(Mathf.RoundToInt(GameStatus.distanceTravelled) % minigameDistance == 0) //for every 100 metres
        //{
        //    float n = Random.Range(0, 2);
        //    if (n == 0)
        //    {
        //        QTEStart();
        //    } 

        //    else if (n == 1)
        //    {
        //        BossFightStart();
        //    }

        //    readyToSpawn = true;
        //}

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

            GameOver();

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
            if (chaser.isClose == true)
            {
                GameOver();
            }
            else if (chaser.isFar == true)
            {
                chaser.timeStarted = Time.time;
                chaser.startPos = chaser.gameObject.transform.position;
                chaser.dangerousTimeRemaining = chaser.dangerousDuration;
                chaser.isFar = false;
                chaser.isClose = true;
                chaser.isLerping = true;
            }
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
            vestFillBar.fillAmount = GameStatus.vestRemaining / 30f;
        }
        else
        {
            vestImage.SetActive(false);
        }
        if (GameStatus.magnet)
        {
            magnetImage.SetActive(true);
            magnetFillBar.fillAmount = GameStatus.magnetRemaining / 15f;
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

    public void GameOver()
    {
        chaser.timeStarted = Time.time;
        chaser.startPos = chaser.gameObject.transform.position;
        chaser.isCaught = true;
        chaser.isFar = false;
        chaser.isClose = false;
        
        chaser.isLerping=true;
        
        gameOverCanvas.SetActive(true);
        coinTextGameOver.text = "Coin:"+GameStatus.coinCollected.ToString();
        distanceTextGameOver.text = "Distance:"+Mathf.RoundToInt(GameStatus.distanceTravelled).ToString();
        mainCamera.transform.parent = null;
        mainCanvas.SetActive(false);
        player.SetActive(false);
        
    }

    void SpawnPowerups()
    {
        if(Mathf.RoundToInt(GameStatus.distanceTravelled) % powerupDistance == 0)
        {
            readyToSpawn = true;
        }
    }
}
