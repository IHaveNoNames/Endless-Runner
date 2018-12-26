using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : Obstacles
{
    public float speed;
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Moving();
		
	}

    private void Moving()
    {
        Vector3 temp;
        temp = transform.position;
        temp.z = temp.z - speed * Time.fixedDeltaTime;
        transform.position= temp;
    }
}
