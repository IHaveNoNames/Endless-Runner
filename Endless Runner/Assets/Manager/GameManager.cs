using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if(Player.distanceTravelled % minigameDistance == 0) //for every 100 metres
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
        
    }

    /*public void GetMagicWand()
    {
        PlayerStatus.magicWand = true;
    }

    public void GetMagnet()
    {
        PlayerStatus.magnet = true;
        PlayerStatus.magnetRemaining = 15f;
    }*/

    void PowerUpCountDown()
    {
        Player.vestRemaining -= Time.deltaTime;
        Player.magnetRemaining -= Time.deltaTime;
        if (Player.vestRemaining <= 0f)
        {
            Player.vest = false;
        }
        if (Player.magnetRemaining <= 0f)
        {
            Player.magnet = false;
        }
    }

    void UseMagicWind()
    {

    }

    public void TakingFatalHit()   //when player is hit, either function will be called
    {
        if (Player.vest == false)
        {
            //gameover event
            print("GameOVer");
        }
        else
        {
            print("Protected");
            Player.vest = false;
        }
    }
    public void TakingLightHit()
    {
        if (Player.vest == false)
        {
            //police catches up
        }
        else
        {
            Player.vest = false;
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
}
