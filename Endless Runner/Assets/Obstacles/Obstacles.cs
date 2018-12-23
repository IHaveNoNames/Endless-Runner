using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacles : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
            
            if (gameObject.CompareTag("HeavyObstacle"))
            {
                GM.TakingFatalHit();
                print("heavy");
            }
            else if (gameObject.CompareTag("LightObstacle"))
            {
                print("Light");
                GM.TakingLightHit();
            }

            Destroy(this.gameObject);
        }           
        
    }

    public void SelfDestoy()
    {
        Destroy(this.gameObject);
    }

    
}
