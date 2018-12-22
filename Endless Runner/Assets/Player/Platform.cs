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
    Queue<Transform> obstacleQueueLeft;
    Queue<Transform> obstacleQueue;
    Queue<Transform> obstacleQueueRight;

    [SerializeField]
    Transform[] obstacles;

    int noOfPlatforms = 5;
    int noOfObstacles = 5;

    // Use this for initialization
    void Start () {

        platformLeftQueue = new Queue<Transform>(noOfPlatforms);
        platformQueue = new Queue<Transform>(noOfPlatforms);
        platformRightQueue = new Queue<Transform>(noOfPlatforms);

        obstacleQueueLeft = new Queue<Transform>(noOfObstacles);
        obstacleQueue = new Queue<Transform>(noOfObstacles);
        obstacleQueueRight = new Queue<Transform>(noOfObstacles);


        for (int i = 0; i<noOfPlatforms; i++)
        {
            platformLeftQueue.Enqueue((Transform)Instantiate(leftPlatformPrefab));
            platformQueue.Enqueue((Transform)Instantiate(platformPrefab));
            platformRightQueue.Enqueue((Transform)Instantiate(rightPlatformPrefab));

            obstacleQueueLeft.Enqueue((Transform)Instantiate(obstacles[Random.Range(0,2)]));
            obstacleQueue.Enqueue((Transform)Instantiate(obstacles[Random.Range(0, 2)]));
            obstacleQueueRight.Enqueue((Transform)Instantiate(obstacles[Random.Range(0, 2)]));
        }

        nextLeftPos = startLeftPos;
        nextPos = startPos;
        nextRightPos = startRightPos;


        for (int i = 0; i<noOfPlatforms; i++)
        {
            Recycle(ref nextLeftPos, ref platformLeftQueue, ref obstacleQueueLeft);
            Recycle(ref nextPos, ref platformQueue,  ref obstacleQueue);
            Recycle(ref nextRightPos, ref platformRightQueue, ref obstacleQueueRight);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (platformQueue.Peek().localPosition.z + 25f < Camera.main.transform.position.z)
        {
            Recycle(ref nextLeftPos, ref platformLeftQueue, ref obstacleQueueLeft);
            Recycle(ref nextPos, ref platformQueue, ref obstacleQueue);
            Recycle(ref nextRightPos, ref platformRightQueue, ref obstacleQueueRight);
        }
	}

    void Recycle(ref Vector3 nextPosition, ref Queue<Transform> queue, ref Queue<Transform> obsqueue)
    {

        Vector3 position = nextPosition;

        position.z += 25f * 0.5f;

        Transform platform = queue.Dequeue();

        Transform obstacle = obsqueue.Dequeue();

        Destroy(obstacle.gameObject);

        platform.position = position;

        nextPosition.z += 25f;

        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        platform.gameObject.GetComponent<Renderer>().material.color = newColor;

        queue.Enqueue(platform);

        int RNG = Random.Range(0, 2);
        obsqueue.Enqueue((Transform)Instantiate(obstacles[RNG], new Vector3(position.x, position.y + obstacles[RNG].transform.localScale.y / 2, position.z), Quaternion.identity));

    }
}
