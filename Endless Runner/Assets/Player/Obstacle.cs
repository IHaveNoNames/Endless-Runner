using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    [SerializeField]
    GameObject barrierPrefab;
    [SerializeField]
    GameObject vehiclePrefab;
    Queue<Transform> obstacleQueueLeft;
    Queue<Transform> obstacleQueue;
    Queue<Transform> obstacleQueueRight;
    int noOfObstaclesPerPlatform = 2;
    [SerializeField]
    GameObject[] obstaclesPrefab;

    // Use this for initialization
    void Start () {
        obstaclesPrefab = new GameObject[2];
        obstaclesPrefab[0] = barrierPrefab;
        obstaclesPrefab[1] = vehiclePrefab;
        obstacleQueueLeft = new Queue<Transform>(noOfObstaclesPerPlatform);
        obstacleQueue = new Queue<Transform>(noOfObstaclesPerPlatform);
        obstacleQueueRight = new Queue<Transform>(noOfObstaclesPerPlatform);


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RecycleObstacles(Vector3 position, Queue<Transform> queue, int determineObstacle)
    {
        
        float pos1 = position.z;
        pos1 -= 25f * 0.5f + obstaclesPrefab[determineObstacle].gameObject.transform.localScale.z;
        float pos2 = position.z;
        pos2 += 25f * 0.5f - obstaclesPrefab[determineObstacle].gameObject.transform.localScale.z;
        Instantiate(obstaclesPrefab[determineObstacle], new Vector3(pos1,pos1,pos1), Quaternion.identity);

    }
}
