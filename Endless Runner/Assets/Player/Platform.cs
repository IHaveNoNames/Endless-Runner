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

    Vector3 startLeftPos = new Vector3(-5, 0, 0);
    Vector3 startPos = new Vector3(0, 0, 0);
    Vector3 startRightPos = new Vector3(5, 0, 0);

    Vector3 nextLeftPos;
    Vector3 nextPos;
    Vector3 nextRightPos;

    int noOfPlatforms = 5;  

	// Use this for initialization
	void Start () {
        //leftPlatforms = GameObject.FindGameObjectsWithTag("Left Platform");
        //platforms = GameObject.FindGameObjectsWithTag("Platform");
        //rightPlatforms = GameObject.FindGameObjectsWithTag("Right Platform");

        platformLeftQueue = new Queue<Transform>(noOfPlatforms);
        platformQueue = new Queue<Transform>(noOfPlatforms);
        platformRightQueue = new Queue<Transform>(noOfPlatforms);


        for(int i = 0; i<noOfPlatforms; i++)
        {
            platformLeftQueue.Enqueue((Transform)Instantiate(leftPlatformPrefab));
            platformQueue.Enqueue((Transform)Instantiate(platformPrefab));
            platformRightQueue.Enqueue((Transform)Instantiate(rightPlatformPrefab));
        }

        nextLeftPos = startLeftPos;
        nextPos = startPos;
        nextRightPos = startRightPos;


        for (int i = 0; i<noOfPlatforms; i++)
        {
            Recycle(ref nextLeftPos, ref platformLeftQueue);
            Recycle(ref nextPos, ref platformQueue);
            Recycle(ref nextRightPos, ref platformRightQueue);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (platformQueue.Peek().localPosition.z + 25f < Camera.main.transform.position.z)
        {
            Recycle(ref nextLeftPos, ref platformLeftQueue);
            Recycle(ref nextPos, ref platformQueue);
            Recycle(ref nextRightPos, ref platformRightQueue);
        }
	}

    void Recycle(ref Vector3 nextPosition, ref Queue<Transform> queue)
    {
        //Vector3 leftPos = nextLeftPos;
        //Vector3 position = nextPos;
        //Vector3 rightPos = nextRightPos;

        Vector3 position = nextPosition;

        //leftPos.z += 25f * 0.5f;
        //position.z += 25f * 0.5f;
        //rightPos.z += 25f * 0.5f;

        position.z += 25f * 0.5f;

        //Transform leftPlatform = platformLeftQueue.Dequeue();
        //Transform platform = platformQueue.Dequeue();
        //Transform rightPlatform = platformRightQueue.Dequeue();

        Transform platform = queue.Dequeue();

        platform.position = position;

        //leftPlatform.position = leftPos;
        //platform.position = position;
        //rightPlatform.position = rightPos;

        nextPosition.z += 25f;

        //nextLeftPos.z += 25f;
        //nextPos.z += 25f;
        //nextRightPos.z += 25f;

        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        platform.gameObject.GetComponent<Renderer>().material.color = newColor;


        queue.Enqueue(platform);

        //platformLeftQueue.Enqueue(leftPlatform);
        //platformQueue.Enqueue(platform);
        //platformRightQueue.Enqueue(rightPlatform);
        
    }
}
