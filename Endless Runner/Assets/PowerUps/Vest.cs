using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vest : MonoBehaviour {

    
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
            GetVest();
            Destroy(gameObject);
        }
    }

    public void GetVest() //These funciton is called when the powers being picked up
    {
        PlayerStatus.vest = true;
        PlayerStatus.vestRemaining = 30f;
    }
}
