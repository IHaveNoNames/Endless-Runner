using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWind : MonoBehaviour {

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
            GetWind();
            Destroy(gameObject);
        }
    }

    public void GetWind() //These funciton is called when the powers being picked up
    {
        PlayerStatus.magicWind = true;
        
    }
}
