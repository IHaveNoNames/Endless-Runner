using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour {

    public Transform Catch;
    public Transform Close;
    public Transform Far;
    public static bool isFar;
    public static bool isClose = false;
    public static bool isCaught = false;
    public float dangerousDuration = 10f;
    private float dangerousTimeRemaining;
    
    private float toCloseDurationOfLerp = 0.5f;
    private float toFarDurationOfLerp = 1f;
    private float toCatchDurationOfLerp = 0.2f;
    bool isLerping = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isClose == true)
        {
            dangerousTimeRemaining -= Time.deltaTime;
        }

        if (dangerousTimeRemaining <= 0&&isClose==true)//lerp to far
        {
            LerpToFar();
        }
        if (isLerping==false)
        {
            if (isFar)
            {
                transform.position = Far.transform.position;

            }
            else if (isClose)
            {
                transform.position = Close.transform.position;
            }
        }
        //placeholder for now
        transform.position = Far.transform.position;
	}

    
    public  void LerpToClose()
    {
        isLerping = true;
        float timeStarted = Time.time;
        Vector3 startPos = transform.position;
        if (isLerping)
        {
            float timeSinceStarted = Time.time - timeStarted;
            float percentageComplete = timeSinceStarted / toCloseDurationOfLerp;

            Vector3 newPos = Vector3.Lerp(startPos, Close.transform.position, percentageComplete);
            transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);

            if (percentageComplete >= 1.0f)
            {
                isLerping = false;
                isClose = true;
                isFar = false;
            }
        }
    }

    public  void LerpToFar()
    {
        isLerping = true;
        float timeStarted = Time.time;
        Vector3 startPos = transform.position;
        if (isLerping)
        {
            float timeSinceStarted = Time.time - timeStarted;
            float percentageComplete = timeSinceStarted / toFarDurationOfLerp;

            Vector3 newPos = Vector3.Lerp(startPos, Far.transform.position, percentageComplete);
            transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);

            if (percentageComplete >= 1.0f)
            {
                isLerping = false;
                isClose = false;
                isFar = true;
            }
        }
    }

    public  void LerpToCatch()
    {
        Catch.transform.parent = null;
        isLerping = true;
        float timeStarted = Time.time;
        Vector3 startPos = transform.position;
        if (isLerping)
        {
            float timeSinceStarted = Time.time - timeStarted;
            float percentageComplete = timeSinceStarted / toCatchDurationOfLerp;

            Vector3 newPos = Vector3.Lerp(startPos, Catch.transform.position, percentageComplete);
            transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);

            if (percentageComplete >= 1.0f)
            {
                isLerping = false;
                isClose = false;
                
                isFar = false;
                isCaught = true;
            }
        }
    }
    


}
