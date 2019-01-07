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
        if (platformQueue.Peek().localPosition.z + 25f < GameStatus.distanceTravelled)
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
        
        platform.position = position;

        nextPosition.z += 25f;

        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        platform.gameObject.GetComponent<Renderer>().material.color = newColor;

        queue.Enqueue(platform);

        if(GameManager.bossFightActive != true)
        {
            RecycleObstacles(position, ref obstacleq);
            RecyclePowerups(position, ref powerupq);
        }
    }

    void RecyclePlatform()
    {
        Vector3 leftPos = nextLeftPos;
        Vector3 position = nextPos;
        Vector3 rightPos = nextRightPos;

        leftPos.z += 25f * 0.5f;
        position.z += 25f * 0.5f;
        rightPos.z += 25f * 0.5f;

        Transform leftPlatform = platformLeftQueue.Dequeue();
        Transform platform = platformQueue.Dequeue();
        Transform rightPlatform = platformRightQueue.Dequeue();

        leftPlatform.position = leftPos;
        platform.position = position;
        rightPlatform.position = rightPos;

        nextLeftPos.z += 25f;
        nextPos.z += 25f;
        nextRightPos.z += 25f;

        platformLeftQueue.Enqueue(leftPlatform);
        platformQueue.Enqueue(platform);
        platformRightQueue.Enqueue(rightPlatform);

        int minNoOfItems = 1;
        int maxNoOfItems = 3;

        int noOfItems = Random.Range(1, 100);

        int percent = 50;
        int percent2 = 40;
        int percent3 = 10;

        if(noOfItems <= 50) //if 1 item is spawn
        {
            int platformChosen = Random.Range(1, 3);
            if(platformChosen == 1)
            {
                //if platform chosen to be left platform
                int whichObstacle = Random.Range(1, 3);
                obstacleQueueLeft.Enqueue((Transform)Instantiate(obstacles[whichObstacle], new Vector3(leftPlatform.position.x, leftPlatform.position.y + obstacles[whichObstacle].transform.localScale.y / 2, leftPlatform.position.z), Quaternion.identity));
            }

            else if (platformChosen == 2)
            {
                //if platform chosen to be left platform
                int whichObstacle = Random.Range(1, 3);
                obstacleQueue.Enqueue((Transform)Instantiate(obstacles[whichObstacle], new Vector3(platform.position.x, platform.position.y + obstacles[whichObstacle].transform.localScale.y / 2, platform.position.z), Quaternion.identity));
            }

            else
            {
                //if platform chosen to be left platform
                int whichObstacle = Random.Range(1, 3);
                obstacleQueueRight.Enqueue((Transform)Instantiate(obstacles[whichObstacle], new Vector3(rightPlatform.position.x, rightPlatform.position.y + obstacles[whichObstacle].transform.localScale.y / 2, rightPlatform.position.z), Quaternion.identity));
            }

        }

        else if(noOfItems >50 && noOfItems <= 90)
        {

        }



    }

    void RecycleObstacles(Vector3 position, ref Queue<Transform> obstacleq)
    {
        Transform obstacle = obstacleq.Dequeue();

        Destroy(obstacle.gameObject);


        int percentage = Random.Range(0, 100);

        if(percentage <= GameManager.percentObstacle)
        {
            int obstaclePercentage = Random.Range(0, 100);
            if (obstaclePercentage <= 50)
            {
                obstacleq.Enqueue((Transform)Instantiate(obstacles[1], new Vector3(position.x, position.y + obstacles[1].transform.localScale.y / 2, position.z), Quaternion.identity));
            }

            else
            {
                obstacleq.Enqueue((Transform)Instantiate(obstacles[2], new Vector3(position.x, position.y + obstacles[2].transform.localScale.y / 2, position.z), Quaternion.identity));
            }
        }

        else
        {
            obstacleq.Enqueue((Transform)Instantiate(obstacles[0], new Vector3(position.x, position.y + obstacles[0].transform.localScale.y / 2, position.z), Quaternion.identity));
        }

        //int RNG = Random.Range(0, 2);
        //obstacleq.Enqueue((Transform)Instantiate(obstacles[RNG], new Vector3(position.x, position.y + obstacles[RNG].transform.localScale.y / 2, position.z), Quaternion.identity));
    }

    void RecyclePowerups(Vector3 position, ref Queue<Transform> powerupq)
    {
        Transform powerup = powerupq.Dequeue();

        if (powerup != null) //change it next time to powerup.gameobject.coin.pickedup == true
        {
            Destroy(powerup.gameObject);
        }

        int percentage = Random.Range(0, 100);

        if (percentage <= GameManager.percentPowerup)
        {
            int itemPercentage = Random.Range(0, 100);
            if(itemPercentage <= GameManager.percentCar)
            {
                powerupq.Enqueue((Transform)Instantiate(powerups[2], new Vector3(position.x, position.y + powerups[2].transform.localScale.y / 2, position.z), Quaternion.identity));
            }

            else if (itemPercentage > ((GameManager.percentCar + GameManager.percentBarrier) - GameManager.percentBarrier) && itemPercentage <= (GameManager.percentCar + GameManager.percentBarrier))
            {
                powerupq.Enqueue((Transform)Instantiate(powerups[1], new Vector3(position.x, position.y + powerups[1].transform.localScale.y / 2, position.z), Quaternion.identity));
            }
        }

        else
        {
            powerupq.Enqueue((Transform)Instantiate(powerups[0], new Vector3(position.x, position.y + powerups[0].transform.localScale.y / 2, position.z), Quaternion.identity));
        }
    }

     void DestroyLaneObstacles(ref Queue<Transform> obstacleq)
    {
        for (int i = 0;i< noOfObstacles;i++)
        {
            Transform obstacle = obstacleq.Dequeue();
            Destroy(obstacle.gameObject);
        }

        
    }

    public void DestroyAllObstacles()
    {
        DestroyLaneObstacles(ref obstacleQueueLeft);
        DestroyLaneObstacles(ref obstacleQueue);
        DestroyLaneObstacles(ref obstacleQueueRight);


        for (int i = 0; i < noOfObstacles; i++)
        {
            obstacleQueueLeft.Enqueue((Transform)Instantiate(obstacles[Random.Range(0, 2)]));
            obstacleQueue.Enqueue((Transform)Instantiate(obstacles[Random.Range(0, 2)]));
            obstacleQueueRight.Enqueue((Transform)Instantiate(obstacles[Random.Range(0, 2)]));
        }

    }

    void CheckObstacles()
    {
        int noOfItemsPerLaneMin = 1;
        GameObject platformLeft = obstacleQueueLeft.Peek().gameObject;
        GameObject platform = obstacleQueue.Peek().gameObject;
        GameObject platformRight = obstacleQueueRight.Peek().gameObject;




    }
}
