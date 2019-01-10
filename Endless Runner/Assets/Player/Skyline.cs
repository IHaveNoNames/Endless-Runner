﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skyline : MonoBehaviour {

    [SerializeField]
    Transform skylinePrefab;

    int numberOfObjects = 10;

    Vector3 leftStartPos = new Vector3(-10, 0, 0);
    Vector3 rightStartPos = new Vector3(10, 0, 0);
    Vector3 leftNextPos;
    Vector3 rightNextPos;
    Queue<Transform> skylineLeftQueue;
    Queue<Transform> skylineRightQueue;

    float minSizeY = 5f;
    float maxSizeY = 20f;

    float minSizeZ = 5f;
    float maxSizeZ = 10f;

	// Use this for initialization
	void Start () {
        skylineLeftQueue = new Queue<Transform>(numberOfObjects);
        skylineRightQueue = new Queue<Transform>(numberOfObjects);
        
        for (int i = 0; i < numberOfObjects; i++)
        {
            skylineLeftQueue.Enqueue((Transform)Instantiate(skylinePrefab));
            skylineRightQueue.Enqueue((Transform)Instantiate(skylinePrefab));
        }

        leftNextPos = leftStartPos;
        rightNextPos = rightStartPos;

        for (int i = 0; i < numberOfObjects; i++)
        {
            Recycle();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(skylineLeftQueue.Peek().localPosition.z < GameStatus.distanceTravelled)
        {
            Recycle();
        }
	}

    void Recycle()
    {
        Vector3 leftScale = new Vector3(5f, Random.Range(minSizeY, maxSizeY), Random.Range(minSizeZ, maxSizeZ));
        Vector3 rightScale = new Vector3(5f, Random.Range(minSizeY, maxSizeY), Random.Range(minSizeZ, maxSizeZ));
        Vector3 leftPosition = leftNextPos;
        Vector3 rightPosition = rightNextPos;
        leftPosition.y += leftScale.y * 0.5f;
        rightPosition.y += rightScale.y * 0.5f;
        leftPosition.z += leftPosition.z * 0.5f;
        rightPosition.z += rightPosition.z * 0.5f;


        

        Transform leftSkyline = skylineLeftQueue.Dequeue();
        Transform rightSkyline = skylineRightQueue.Dequeue();

        leftSkyline.localScale = leftScale;
        rightSkyline.localScale = rightScale;
        leftSkyline.localPosition = leftPosition;
        rightSkyline.localPosition = rightPosition;

        leftNextPos.z += leftScale.z + 5f;
        rightNextPos.z += rightScale.z + 5f;

        Color leftColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        Color rightColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        leftSkyline.gameObject.GetComponent<Renderer>().material.color = leftColor;
        rightSkyline.gameObject.GetComponent<Renderer>().material.color = rightColor;

        skylineLeftQueue.Enqueue(leftSkyline);
        skylineRightQueue.Enqueue(rightSkyline);
    }
}
