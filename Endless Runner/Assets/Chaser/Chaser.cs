using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{

    public Transform Catch;
    public Transform Close;
    public Transform Far;
    public static bool isFar = true;
    public static bool isClose = false;
    public static bool isCaught = false;
    public float dangerousDuration = 10f;
    private float dangerousTimeRemaining;

    private float toCloseDurationOfLerp = 0.5f;
    private float toFarDurationOfLerp = 1f;
    private float toCatchDurationOfLerp = 0.2f;
    public static bool isLerping = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isClose == true && dangerousTimeRemaining > 0)
        {
            dangerousTimeRemaining -= Time.deltaTime;
        }

        if (dangerousTimeRemaining <= 0 && isClose == true)//lerp to far
        {
            LerpToFar();
        }
        if (isLerping)
        {
            if (isCaught)
            {
                LerpToCatch();
            }
            else if (isFar)
            {
                LerpToFar();
            }
            else if (isClose)
            {
                LerpToClose();

            }
        }
        if (isLerping == false)
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
        ;
    }


    public void LerpToClose()
    {

        
        
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

                }
            
        }
    }

    public void LerpToFar()
    {


        
       
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

            }
        }
    }

    public void LerpToCatch()
    {
        Catch.transform.parent = null;
        

        isLerping = true;
        float timeStarted = Time.time;
        Vector3 startPos = transform.position;
        if (isLerping)
        {

            {
                float timeSinceStarted = Time.time - timeStarted;
                float percentageComplete = timeSinceStarted / toCatchDurationOfLerp;
                
                    Vector3 newPos = Vector3.Lerp(startPos, Catch.transform.position, percentageComplete);
                    transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);

                    if (percentageComplete >= 1.0f)
                    {
                        isLerping = false;
                    }
                
            }

        }
    }
}
    



