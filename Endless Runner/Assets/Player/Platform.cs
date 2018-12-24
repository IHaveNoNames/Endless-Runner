using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    //GameObject[] leftPlatforms;
    //GameObject[] platforms;
    //GameObject[] rightPlatforms;

    [SerializeField]
    Transform leftPlatformPrefab;
    [SerializeField]
    Transform platformPrefab;
    [SerializeField]
    Transform rightPlatformPrefab;

    public Queue<Transform> PlatformQueue
    {
        get
        {
            return platformQueue;
        }
    }

    Queue<Transform> platformQueue;
    Queue<Transform> platformLeftQueue;
    Queue<Transform> platformRightQueue;

    Queue<Transform> obstacleQueueLeft;
    Queue<Transform> obstacleQueue;
    Queue<Transform> obstacleQueueRight;

    Queue<Transform> powerupQueueLeft;
    Queue<Transform> powerupQueue;
    Queue<Transform> powerupQueueRight;

    Vector3 startLeftPos = new Vector3(-5, 0, 0);
    Vector3 startPos = new Vector3(0, 0, 0);
    Vector3 startRightPos = new Vector3(5, 0, 0);

    Vector3 nextLeftPos;
    Vector3 nextPos;
    Vector3 nextRightPos;

    [SerializeField]
    Transform barrierPrefab;
    [SerializeField]
    Transform vehiclePrefab;
    [SerializeField]
    Transform[] obstacles;
    [SerializeField]
    Transform[] powerups;

    int noOfPlatforms = 5;
    int noOfObstacles = 5;
    int noOfPowerups = 5;

    // Use this for initialization
    void Start () {

        platformLeftQueue = new Queue<Transform>(noOfPlatforms);
        platformQueue = new Queue<Transform>(noOfPlatforms);
        platformRightQueue = new Queue<Transform>(noOfPlatforms);

        obstacleQueueLeft = new Queue<Transform>(noOfObstacles);
        obstacleQueue = new Queue<Transform>(noOfObstacles);
        obstacleQueueRight = new Queue<Transform>(noOfObstacles);

        powerupQueueLeft = new Queue<Transform>(noOfPowerups);
        powerupQueue = new Queue<Transform>(noOfPowerups);
        powerupQueueRight = new Queue<Transform>(noOfPowerups);


        for (int i = 0; i<noOfPlatforms; i++)
        {
            platformLeftQueue.Enqueue((Transform)Instantiate(leftPlatformPrefab));
            platformQueue.Enqueue((Transform)Instantiate(platformPrefab));
            platformRightQueue.Enqueue((Transform)Instantiate(rightPlatformPrefab));
        }

        for (int i = 0; i<noOfObstacles; i++)
        {
            obstacleQueueLeft.Enqueue((Transform)Instantiate(obstacles[Random.Range(0, 2)]));
            obstacleQueue.Enqueue((Transform)Instantiate(obstacles[Random.Range(0, 2)]));
            obstacleQueueRight.Enqueue((Transform)Instantiate(obstacles[Random.Range(0, 2)]));
        }

        for (int i = 0; i<noOfPowerups; i++)
        {
            powerupQueueLeft.Enqueue((Transform)Instantiate(powerups[Random.Range(0, 3)]));
            powerupQueue.Enqueue((Transform)Instantiate(powerups[Random.Range(0, 3)]));
            powerupQueueRight.Enqueue((Transform)Instantiate(powerups[Random.Range(0, 3)]));
        }

        nextLeftPos = startLeftPos;
        nextPos = startPos;
        nextRightPos = startRightPos;


        for (int i = 0; i<noOfPlatforms; i++)
        {
            Recycle(ref nextLeftPos, ref platformLeftQueue, ref obstacleQueueLeft, ref powerupQueueLeft);
            Recycle(ref nextPos, ref platformQueue,  ref obstacleQueue, ref powerupQueue);
            Recycle(ref nextRightPos, ref platformRightQueue, ref obstacleQueueRight, ref powerupQueueRight);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (platformQueue.Peek().localPosition.z + 25f < Camera.main.transform.position.z)
        {
            Recycle(ref nextLeftPos, ref platformLeftQueue, ref obstacleQueueLeft, ref powerupQueueLeft);
            Recycle(ref nextPos, ref platformQueue, ref obstacleQueue, ref powerupQueue);
            Recycle(ref nextRightPos, ref platformRightQueue, ref obstacleQueueRight, ref powerupQueueRight);
        }
	}

    void Recycle(ref Vector3 nextPosition, ref Queue<Transform> queue, ref Queue<Transform>obstacleq, ref Queue<Transform> powerupq)
    {

        Vector3 position = nextPosition;

        position.z += 25f * 0.5f;

        Transform platform = queue.Dequeue();

        Transform obstacle = obstacleq.Dequeue();

        Transform powerup = powerupq.Dequeue();

        
        Destroy(obstacle.gameObject);

        if(powerup != null)
        {
            Destroy(powerup.gameObject);
        }
        
        platform.position = position;

        nextPosition.z += 25f;

        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        platform.gameObject.GetComponent<Renderer>().material.color = newColor;

        queue.Enqueue(platform);

        int RNG = Random.Range(0, 2);
        obstacleq.Enqueue((Transform)Instantiate(obstacles[RNG], new Vector3(position.x, position.y + obstacles[RNG].transform.localScale.y / 2, position.z), Quaternion.identity));

        
        int RNG2 = Random.Range(0, 3);
        powerupq.Enqueue((Transform)Instantiate(powerups[RNG2], new Vector3(position.x, position.y + powerups[RNG2].transform.localScale.y / 2, position.z), Quaternion.identity));

    }
}
