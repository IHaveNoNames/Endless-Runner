using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {

    public float magnetDuration =10f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetMagnet();
            Destroy(gameObject);
        }
    }

    public void GetMagnet() //These funciton is called when the powers being picked up
    {
        GameStatus.magnet = true;
        GameStatus.magnetRemaining = magnetDuration;
    }
}
