using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    private GameObject player;
    public float attractDistance;
    public float attractSpeed;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        MagnetAttraction();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatus.coinCollected += 1;
            Destroy(gameObject);
        }
    }

    void MagnetAttraction()
    {
        float distance=Vector3.Distance(gameObject.transform.position, player.transform.position);

        if (PlayerStatus.magnet==true&&distance < attractDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, attractSpeed * Time.deltaTime);
        }
    }
}
