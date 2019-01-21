using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    private GameObject boss;
    public float thrownSpeed;
    public bool pickedByPlayer = false;
    public GameManager GM;
    

	// Use this for initialization
	void Start () {
        

        boss = GameObject.Find("Boss");
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
            
        
	}
	
	// Update is called once per frame
	void Update () {
        if (pickedByPlayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, boss.transform.position, thrownSpeed * Time.deltaTime);
        }

        if (!GameManager.bossIsAlive)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Boss") && pickedByPlayer == true)
        {
            AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position);
            print("hitBOss");
            GameManager.bossCurrentHealth -= 1;
            //some visual effect



            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            pickedByPlayer = true;
            gameObject.tag = "Grenade";
        }

        
    }
}
