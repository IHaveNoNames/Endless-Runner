using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    [SerializeField]
    Transform barrierPrefab;
    [SerializeField]
    Transform vehiclePrefab;
    Queue<Transform> obstacleQueueLeft;
    Queue<Transform> obstacleQueue;
    Queue<Transform> obstacleQueueRight;
    int noOfObstaclesPerPlatform = 2;

    // Use this for initialization
    void Start () {
        obstacleQueueLeft = new Queue<Transform>(noOfObstaclesPerPlatform);
        obstacleQueue = new Queue<Transform>(noOfObstaclesPerPlatform);
        obstacleQueueRight = new Queue<Transform>(noOfObstaclesPerPlatform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RecycleObstacles(Vector3 position, Queue<Transform> queue)
    {
        int determineObstacle = Random.Range(0, 2);
        if (determineObstacle == 0) //nothing
        {

        }

        else if (determineObstacle == 1) //barrier
        {

        }

        else if (determineObstacle == 2) //vehicle
        {

        }
    }
}
