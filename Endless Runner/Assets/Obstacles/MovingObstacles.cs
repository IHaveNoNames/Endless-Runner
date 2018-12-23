using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : Obstacles
{
    public float speed;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Moving();
		
	}

    private void Moving()
    {
        rb.AddForce(0, 0, speed * Time.fixedDeltaTime);
    }
}
