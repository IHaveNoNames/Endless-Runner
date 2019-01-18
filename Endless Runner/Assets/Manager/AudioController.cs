using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class  AudioController :MonoBehaviour {
    private GameObject player;
    public  AudioSource click;
    public  AudioSource hit;
    public  AudioSource powerup;
    public  AudioSource coin;
    public  AudioSource die;
    public  AudioSource jump;
    public AudioSource magic;
    // Use this for initialization
    public Slider volumeSlider;

    float masterVolume = 0.5f;

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

    public void SetMasterVolume()
    {
        masterVolume = volumeSlider.value;
        AudioListener.volume = masterVolume;
    }

}
