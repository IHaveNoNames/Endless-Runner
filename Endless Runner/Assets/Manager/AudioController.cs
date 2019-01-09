using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class  AudioController :MonoBehaviour {
    private GameObject player;
    public  AudioSource click;
    public  AudioSource hit;
    public  AudioSource powerup;
    public  AudioSource coin;
    public  AudioSource die;
    public  AudioSource jump;
    // Use this for initialization

    private void Start()
    {
        player=GameObject.Find("Player");
    }
    private void Update()
    {
        if (player)
        {
            transform.position = player.transform.position;
        }
    }

}
