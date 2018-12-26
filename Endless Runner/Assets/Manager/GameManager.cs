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
    public bool bossFightActive = false;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        PowerUpCountDown();

        if (bossCurrentHealth <= 0 && bossFightActive == true)
        {
            BossDefeated();
        }
        
    }

    /*public void GetMagicWind()
    {
        PlayerStatus.magicWind = true;
    }

    public void GetMagnet()
    {
        PlayerStatus.magnet = true;
        PlayerStatus.magnetRemaining = 15f;
    }*/

    void PowerUpCountDown()
    {
        PlayerStatus.vestRemaining -= Time.deltaTime;
        PlayerStatus.magnetRemaining -= Time.deltaTime;
        if (PlayerStatus.vestRemaining <= 0f)
        {
            PlayerStatus.vest = false;
        }
        if (PlayerStatus.magnetRemaining <= 0f)
        {
            PlayerStatus.magnet = false;
        }
    }

    void UseMagicWind()
    {

    }

    public void TakingFatalHit()   //when player is hit, either function will be called
    {
        if (PlayerStatus.vest == false)
        {
            //gameover event
            print("GameOVer");
        }
        else
        {
            print("Protected");
            PlayerStatus.vest = false;
        }
    }
    public void TakingLightHit()
    {
        if (PlayerStatus.vest == false)
        {
            //police catches up
        }
        else
        {
            PlayerStatus.vest = false;
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
}
