using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skyline : MonoBehaviour {

    [SerializeField]
    Transform skylinePrefab;

    [SerializeField]
    Transform[] skylinePrefabs;

    [SerializeField]
    float[] skylinePrefabsLength;

    [SerializeField]
    float[] skylinePrefabsHeight;

    int numberOfObjects = 10;

    Vector3 leftStartPos = new Vector3(-11, 0, 0);
    Vector3 rightStartPos = new Vector3(11, 0, 0);
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
        //Vector3 leftScale = new Vector3(5f, Random.Range(minSizeY, maxSizeY), Random.Range(minSizeZ, maxSizeZ));
        //Vector3 rightScale = new Vector3(5f, Random.Range(minSizeY, maxSizeY), Random.Range(minSizeZ, maxSizeZ));
        Vector3 leftPosition = leftNextPos;
        Vector3 rightPosition = rightNextPos;
        //leftPosition.y += leftScale.y * 0.5f;
        //rightPosition.y += rightScale.y * 0.5f;
        


        
        
        Transform leftSkyline = skylineLeftQueue.Dequeue();
        Transform rightSkyline = skylineRightQueue.Dequeue();

        Destroy(leftSkyline.gameObject);
        Destroy(rightSkyline.gameObject);

        int leftrand = Random.Range(0, skylinePrefabs.Length);
        int rightrand = Random.Range(0, skylinePrefabs.Length);

        leftPosition.z += skylinePrefabsLength[leftrand];
        rightPosition.z += skylinePrefabsLength[rightrand];

        leftSkyline = (Transform)Instantiate(skylinePrefabs[leftrand], new Vector3(leftPosition.x, skylinePrefabsHeight[leftrand] + 0.6042783f, leftPosition.z), Quaternion.identity);
        rightSkyline = (Transform)Instantiate(skylinePrefabs[rightrand], new Vector3(rightPosition.x, skylinePrefabsHeight[rightrand] + 0.5590504f, rightPosition.z), Quaternion.identity);

        leftSkyline.Rotate(0, -90, 0);
        rightSkyline.Rotate(0, 90, 0);

        //leftSkyline.localScale = leftScale;
        //rightSkyline.localScale = rightScale;
        //leftSkyline.localPosition = leftPosition;
        //rightSkyline.localPosition = rightPosition;

        leftNextPos.z += skylinePrefabsLength[leftrand] * 2 + 1f;
        rightNextPos.z += skylinePrefabsLength[rightrand] * 2 + 1f;

        //Color leftColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        //Color rightColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        //leftSkyline.gameObject.GetComponent<Renderer>().material.color = leftColor;
        //rightSkyline.gameObject.GetComponent<Renderer>().material.color = rightColor;

        skylineLeftQueue.Enqueue(leftSkyline);
        skylineRightQueue.Enqueue(rightSkyline);
    }
}
