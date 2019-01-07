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
            //Recycle(ref nextLeftPos, ref platformLeftQueue, ref obstacleQueueLeft, ref powerupQueueLeft);
            //Recycle(ref nextPos, ref platformQueue,  ref obstacleQueue, ref powerupQueue);
            //Recycle(ref nextRightPos, ref platformRightQueue, ref obstacleQueueRight, ref powerupQueueRight);
            Recycle();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (platformQueue.Peek().localPosition.z + 25f < GameStatus.distanceTravelled)
        {
            //Recycle(ref nextLeftPos, ref platformLeftQueue, ref obstacleQueueLeft, ref powerupQueueLeft);
            //Recycle(ref nextPos, ref platformQueue, ref obstacleQueue, ref powerupQueue);
            //Recycle(ref nextRightPos, ref platformRightQueue, ref obstacleQueueRight, ref powerupQueueRight);
            Recycle();
        }
	}

    //void Recycle(ref Vector3 nextPosition, ref Queue<Transform> queue, ref Queue<Transform>obstacleq, ref Queue<Transform> powerupq)
    //{

    //    Vector3 position = nextPosition;

    //    position.z += 25f * 0.5f;

    //    Transform platform = queue.Dequeue();
        
    //    platform.position = position;

    //    nextPosition.z += 25f;

    //    Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
    //    platform.gameObject.GetComponent<Renderer>().material.color = newColor;

    //    queue.Enqueue(platform);

    //    if(GameManager.bossFightActive != true)
    //    {
    //        RecycleObstacles(position, ref obstacleq);
    //        RecyclePowerups(position, ref powerupq);
    //    }
    //}

    void Recycle()
    {
        Vector3 leftPos = nextLeftPos;
        Vector3 position = nextPos;
        Vector3 rightPos = nextRightPos;

        leftPos.z += (leftPlatformPrefab.localScale.z / 2);
        position.z += (platformPrefab.localScale.z / 2);
        rightPos.z += (rightPlatformPrefab.localScale.z / 2);

        Transform leftPlatform = platformLeftQueue.Dequeue();
        Transform platform = platformQueue.Dequeue();
        Transform rightPlatform = platformRightQueue.Dequeue();

        leftPlatform.position = leftPos;
        platform.position = position;
        rightPlatform.position = rightPos;

        nextLeftPos.z += leftPlatformPrefab.localScale.z;
        nextPos.z += platformPrefab.localScale.z;
        nextRightPos.z += rightPlatformPrefab.localScale.z;

        platformLeftQueue.Enqueue(leftPlatform);
        platformQueue.Enqueue(platform);
        platformRightQueue.Enqueue(rightPlatform);

        Transform leftPlatformStatus;
        Transform platformStatus;
        Transform rightPlatformStatus;

        RecycleObstacle(leftPlatform, platform, rightPlatform, out leftPlatformStatus, out platformStatus, out rightPlatformStatus);

        //RecyclePowerups(leftPlatformStatus, platformStatus, rightPlatformStatus);

        
        

        //obstacle recycling done


        //make coin have chance to spawn
        //check for any object in position.transform.x

    }

    void RecycleObstacle(Transform leftPlatform, Transform platform, Transform rightPlatform, out Transform leftPlatformStatus, out Transform platformStatus, out Transform rightPlatformStatus)
    {
        Transform obstacleLeft = obstacleQueueLeft.Dequeue();
        Transform obstacleMiddle = obstacleQueue.Dequeue();
        Transform obstacleRight = obstacleQueueRight.Dequeue();

        Destroy(obstacleLeft.gameObject);
        Destroy(obstacleMiddle.gameObject);
        Destroy(obstacleRight.gameObject);

        int noOfItems = Random.Range(1, 100);

        //50% 1 platform
        //40% 2 platform
        //10% 3 platform


        if (noOfItems <= GameManager.oneObstaclePercent) //if 1 item is spawn
        {
            int platformChosen = Random.Range(1, 3);
            if (platformChosen == 1)
            {
                //if platform chosen to be left platform
                int leftObstacle = Random.Range(1, 3);
                Transform newLeftObstacle = HandleRecyclingObstacles(obstacleQueueLeft, leftObstacle, leftPlatform);
                //obstacleQueueLeft.Enqueue((Transform)Instantiate(obstacles[leftObstacle], new Vector3(leftPlatform.position.x, leftPlatform.position.y + obstacles[leftObstacle].transform.localScale.y / 2, leftPlatform.position.z), Quaternion.identity));
                //Queue<Transform> reverseLeft = Reverse(obstacleQueueLeft);
                //Transform newLeftObstacle = reverseLeft.Dequeue();

                Transform newObstacle = HandleRecyclingObstacles(obstacleQueue, 0, platform);
                //obstacleQueue.Enqueue((Transform)Instantiate(obstacles[0], new Vector3(platform.position.x, platform.position.y + obstacles[0].transform.localScale.y / 2, platform.position.z), Quaternion.identity));
                //Queue<Transform> reverse = Reverse(obstacleQueue);
                //Transform newObstacle = reverseLeft.Dequeue();

                Transform newRightObstacle = HandleRecyclingObstacles(obstacleQueueRight, 0, rightPlatform);
                //obstacleQueueRight.Enqueue((Transform)Instantiate(obstacles[0], new Vector3(rightPlatform.position.x, rightPlatform.position.y + obstacles[0].transform.localScale.y / 2, rightPlatform.position.z), Quaternion.identity));
                //Queue<Transform> reverseRight = Reverse(obstacleQueueRight);
                //Transform newRightObstacle = reverseRight.Dequeue();

                leftPlatformStatus = newLeftObstacle;
                platformStatus = newObstacle;
                rightPlatformStatus = newRightObstacle;
            }

            else if (platformChosen == 2)
            {
                //if platform chosen to be middle platform
                Transform newLeftObstacle = HandleRecyclingObstacles(obstacleQueueLeft, 0, leftPlatform);
                //obstacleQueueLeft.Enqueue((Transform)Instantiate(obstacles[0], new Vector3(leftPlatform.position.x, leftPlatform.position.y + obstacles[0].transform.localScale.y / 2, leftPlatform.position.z), Quaternion.identity));
                //Queue<Transform> reverseLeft = Reverse(obstacleQueueLeft);
                //Transform newLeftObstacle = reverseLeft.Dequeue();

                int obstacle = Random.Range(1, 3);
                Transform newObstacle = HandleRecyclingObstacles(obstacleQueue, obstacle, platform);
                //obstacleQueue.Enqueue((Transform)Instantiate(obstacles[obstacle], new Vector3(platform.position.x, platform.position.y + obstacles[obstacle].transform.localScale.y / 2, platform.position.z), Quaternion.identity));
                //Queue<Transform> reverse = Reverse(obstacleQueue);
                //Transform newObstacle = reverseLeft.Dequeue();

                Transform newRightObstacle = HandleRecyclingObstacles(obstacleQueueRight, 0, rightPlatform);
                //obstacleQueueRight.Enqueue((Transform)Instantiate(obstacles[0], new Vector3(rightPlatform.position.x, rightPlatform.position.y + obstacles[0].transform.localScale.y / 2, rightPlatform.position.z), Quaternion.identity));
                //Queue<Transform> reverseRight = Reverse(obstacleQueueRight);
                //Transform newRightObstacle = reverseRight.Dequeue();

                leftPlatformStatus = newLeftObstacle;
                platformStatus = newObstacle;
                rightPlatformStatus = newRightObstacle;
            }

            else
            {
                //if platform chosen to be right platform

                Transform newLeftObstacle = HandleRecyclingObstacles(obstacleQueueLeft, 0, leftPlatform);
                //obstacleQueueLeft.Enqueue((Transform)Instantiate(obstacles[0], new Vector3(leftPlatform.position.x, leftPlatform.position.y + obstacles[0].transform.localScale.y / 2, leftPlatform.position.z), Quaternion.identity));
                //Queue<Transform> reverseLeft = Reverse(obstacleQueueLeft);
                //Transform newLeftObstacle = reverseLeft.Dequeue();

                Transform newObstacle = HandleRecyclingObstacles(obstacleQueue, 0, platform);
                //obstacleQueue.Enqueue((Transform)Instantiate(obstacles[0], new Vector3(platform.position.x, platform.position.y + obstacles[0].transform.localScale.y / 2, platform.position.z), Quaternion.identity));
                //Queue<Transform> reverse = Reverse(obstacleQueue);
                //Transform newObstacle = reverseLeft.Dequeue();

                int rightObstacle = Random.Range(1, 3);
                Transform newRightObstacle = HandleRecyclingObstacles(obstacleQueueRight, rightObstacle, rightPlatform);
                //obstacleQueueRight.Enqueue((Transform)Instantiate(obstacles[rightObstacle], new Vector3(rightPlatform.position.x, rightPlatform.position.y + obstacles[rightObstacle].transform.localScale.y / 2, rightPlatform.position.z), Quaternion.identity));
                //Queue<Transform> reverseRight = Reverse(obstacleQueueRight);
                //Transform newRightObstacle = reverseRight.Dequeue();

                leftPlatformStatus = newLeftObstacle;
                platformStatus = newObstacle;
                rightPlatformStatus = newRightObstacle;
            }

        }

        else if (noOfItems > GameManager.oneObstaclePercent && noOfItems <= GameManager.oneObstaclePercent + GameManager.twoObstaclesPercent) //if 2 item is going to spawn
        {
            int platformChosen = Random.Range(1, 3);

            if (platformChosen == 1)
            {
                //if platform chosen to be middle and right
                Transform newLeftObstacle = HandleRecyclingObstacles(obstacleQueueLeft, 0, leftPlatform);
                //obstacleQueueLeft.Enqueue((Transform)Instantiate(obstacles[0], new Vector3(leftPlatform.position.x, leftPlatform.position.y + obstacles[0].transform.localScale.y / 2, leftPlatform.position.z), Quaternion.identity));

                int obstacle = Random.Range(1, 3);
                Transform newObstacle = HandleRecyclingObstacles(obstacleQueue, obstacle, platform);
                //obstacleQueue.Enqueue((Transform)Instantiate(obstacles[obstacle], new Vector3(platform.position.x, platform.position.y + obstacles[obstacle].transform.localScale.y / 2, platform.position.z), Quaternion.identity));

                int rightObstacle = Random.Range(1, 3);
                Transform newRightObstacle = HandleRecyclingObstacles(obstacleQueueRight, rightObstacle, rightPlatform);
                //obstacleQueueRight.Enqueue((Transform)Instantiate(obstacles[rightObstacle], new Vector3(rightPlatform.position.x, rightPlatform.position.y + obstacles[rightObstacle].transform.localScale.y / 2, rightPlatform.position.z), Quaternion.identity));

                leftPlatformStatus = newLeftObstacle;
                platformStatus = newObstacle;
                rightPlatformStatus = newRightObstacle;

            }

            else if (platformChosen == 2) //platform is left and right
            {
                int leftObstacle = Random.Range(1, 3);
                Transform newLeftObstacle = HandleRecyclingObstacles(obstacleQueueLeft, leftObstacle, leftPlatform);
                //obstacleQueueLeft.Enqueue((Transform)Instantiate(obstacles[leftObstacle], new Vector3(leftPlatform.position.x, leftPlatform.position.y + obstacles[leftObstacle].transform.localScale.y / 2, leftPlatform.position.z), Quaternion.identity));

                Transform newObstacle = HandleRecyclingObstacles(obstacleQueue, 0, platform);
                //obstacleQueue.Enqueue((Transform)Instantiate(obstacles[0], new Vector3(platform.position.x, platform.position.y + obstacles[0].transform.localScale.y / 2, platform.position.z), Quaternion.identity));

                int rightObstacle = Random.Range(1, 3);
                Transform newRightObstacle = HandleRecyclingObstacles(obstacleQueueRight, rightObstacle, rightPlatform);
                //obstacleQueueRight.Enqueue((Transform)Instantiate(obstacles[rightObstacle], new Vector3(rightPlatform.position.x, rightPlatform.position.y + obstacles[rightObstacle].transform.localScale.y / 2, rightPlatform.position.z), Quaternion.identity));

                leftPlatformStatus = newLeftObstacle;
                platformStatus = newObstacle;
                rightPlatformStatus = newRightObstacle;
            }

            else //left middle
            {
                int leftObstacle = Random.Range(1, 3);
                Transform newLeftObstacle = HandleRecyclingObstacles(obstacleQueueLeft, leftObstacle, leftPlatform);
                //obstacleQueueLeft.Enqueue((Transform)Instantiate(obstacles[leftObstacle], new Vector3(leftPlatform.position.x, leftPlatform.position.y + obstacles[leftObstacle].transform.localScale.y / 2, leftPlatform.position.z), Quaternion.identity));

                int obstacle = Random.Range(1, 3);
                Transform newObstacle = HandleRecyclingObstacles(obstacleQueue, obstacle, platform);
                //obstacleQueue.Enqueue((Transform)Instantiate(obstacles[obstacle], new Vector3(platform.position.x, platform.position.y + obstacles[obstacle].transform.localScale.y / 2, platform.position.z), Quaternion.identity));

                Transform newRightObstacle = HandleRecyclingObstacles(obstacleQueueRight, 0, rightPlatform);
                //obstacleQueueRight.Enqueue((Transform)Instantiate(obstacles[0], new Vector3(rightPlatform.position.x, rightPlatform.position.y + obstacles[0].transform.localScale.y / 2, rightPlatform.position.z), Quaternion.identity));

                leftPlatformStatus = newLeftObstacle;
                platformStatus = newObstacle;
                rightPlatformStatus = newRightObstacle;
            }
        }

        else if (noOfItems > ((GameManager.oneObstaclePercent + GameManager.twoObstaclesPercent + GameManager.threeObstaclesPercent) - GameManager.threeObstaclesPercent))
        {
            int oddPlatform = Random.Range(1, 3);

            int leftObstacle = Random.Range(1, 3);
            Transform newLeftObstacle = HandleRecyclingObstacles(obstacleQueueLeft, leftObstacle, leftPlatform);
            //obstacleQueueLeft.Enqueue((Transform)Instantiate(obstacles[leftObstacle], new Vector3(leftPlatform.position.x, leftPlatform.position.y + obstacles[leftObstacle].transform.localScale.y / 2, leftPlatform.position.z), Quaternion.identity));

            int obstacle = Random.Range(1, 3);
            Transform newObstacle = HandleRecyclingObstacles(obstacleQueue, obstacle, platform);
            //obstacleQueue.Enqueue((Transform)Instantiate(obstacles[obstacle], new Vector3(platform.position.x, platform.position.y + obstacles[obstacle].transform.localScale.y / 2, platform.position.z), Quaternion.identity));

            int rightObstacle = Random.Range(1, 3);
            Transform newRightObstacle = HandleRecyclingObstacles(obstacleQueueRight, rightObstacle, rightPlatform);
            //obstacleQueueRight.Enqueue((Transform)Instantiate(obstacles[rightObstacle], new Vector3(rightPlatform.position.x, rightPlatform.position.y + obstacles[rightObstacle].transform.localScale.y / 2, rightPlatform.position.z), Quaternion.identity));

            leftPlatformStatus = newLeftObstacle;
            platformStatus = newObstacle;
            rightPlatformStatus = newRightObstacle;
        }

        else //not needed
        {
            leftPlatformStatus = null;
            platformStatus = null;
            rightPlatformStatus = null;
        }
    }

    Transform HandleRecyclingObstacles(Queue<Transform> queue, int index, Transform transform)
    {
        Transform gObj = Instantiate(obstacles[index], new Vector3(transform.position.x, transform.position.y + obstacles[index].transform.localScale.y / 2, transform.position.z), Quaternion.identity);
        queue.Enqueue(gObj);

        return gObj;
    }

    void RecyclePowerups(Transform leftStatus, Transform middleStatus, Transform rightStaus)
    {
        Transform powerUpLeft = powerupQueueLeft.Dequeue();
        Transform powerUp = powerupQueue.Dequeue();
        Transform powerUpRight = powerupQueueRight.Dequeue();
        
        if(powerUpLeft != null)
        {
            Destroy(powerUpLeft.gameObject);
        }

        if(powerUp != null)
        {
            Destroy(powerUp.gameObject);
        }

        if(powerUpRight != null)
        {
            Destroy(powerUpRight.gameObject);
        }

        int percentage = Random.Range(1, 101);

        //None = 40%
        //Magnet = 30%
        //Vest = 20%
        //Magic Wand = 10%
        bool readyToSpawn = true; //put in game manager when cross 500m.
        if(readyToSpawn == true)
        {
            
        }

        //take this results, use System.Array.IndexOf to check if the obstacles[]'s array = 0;
    }
    //void RecyclePowerups(Vector3 position, ref Queue<Transform> powerupq)
    //{
    //    Transform powerup = powerupq.Dequeue();

    //    if (powerup != null) //change it next time to powerup.gameobject.coin.pickedup == true
    //    {
    //        Destroy(powerup.gameObject);
    //    }

    //    int percentage = Random.Range(0, 100);

    //    if (percentage <= GameManager.percentPowerup)
    //    {
    //        int itemPercentage = Random.Range(0, 100);
    //        if(itemPercentage <= GameManager.percentCar)
    //        {
    //            powerupq.Enqueue((Transform)Instantiate(powerups[2], new Vector3(position.x, position.y + powerups[2].transform.localScale.y / 2, position.z), Quaternion.identity));
    //        }

    //        else if (itemPercentage > ((GameManager.percentCar + GameManager.percentBarrier) - GameManager.percentBarrier) && itemPercentage <= (GameManager.percentCar + GameManager.percentBarrier))
    //        {
    //            powerupq.Enqueue((Transform)Instantiate(powerups[1], new Vector3(position.x, position.y + powerups[1].transform.localScale.y / 2, position.z), Quaternion.identity));
    //        }
    //    }

    //    else
    //    {
    //        powerupq.Enqueue((Transform)Instantiate(powerups[0], new Vector3(position.x, position.y + powerups[0].transform.localScale.y / 2, position.z), Quaternion.identity));
    //    }
    //}

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

    Queue<Transform> Reverse(Queue<Transform> queue)
    {
        Queue<Transform> reversed = new Queue<Transform>(noOfPlatforms);

        Stack<Transform> stack = new Stack<Transform>(noOfPlatforms);

        for (int i = 0; i < noOfPlatforms; i++)
        {
            stack.Push(queue.Dequeue());
        }

        for (int i = 0; i < noOfPlatforms; i++)
        {
            reversed.Enqueue(stack.Pop());
        }

        return reversed;
    }

    
}
