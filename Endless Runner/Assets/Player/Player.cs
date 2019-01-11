﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private AudioController audioController;
    
    public static float acceleration = 20f;

    Rigidbody rb;

    Vector3 endPos;

    Vector3 startPos;

    float durationOfLerp = 0.2f;

    float distanceToMove = 5f;

    bool isLerping = false;

    float timeStarted;

    Vector3 jumpVelocity = new Vector3(0, 5f, 0f);

    private Animator anim;

    private Vector2 touchOrigin = -Vector2.one;

    private BoxCollider boxCollider;

    //Ground Checking
    [SerializeField]
    Transform groundCheck;
    float groundRadius = 0.1f;
    [SerializeField]
    LayerMask whatIsGround;
    [SerializeField]
    bool onGround;

    
    float slidingDuration = 0.9f;
    float slidingRemaining = 0f;
    float jumpDuration = 0.9f;
    float jumpRemaining = 0f;

    // Use this for initialization
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioController = GameObject.Find("Audio").GetComponent<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpRemaining > 0)
        {
            jumpRemaining -= Time.deltaTime;
        }

        if (slidingRemaining >0f)
        {
            slidingRemaining -= Time.deltaTime;
            //boxcollider becomes smaller
            boxCollider.center = new Vector3(0f, 0.2f, 0f);
            boxCollider.size = new Vector3(0.4f,0.42f, 0.4f);
        }
        if (slidingRemaining <= 0f)
        {
            //normal boxcollider
            boxCollider.center = new Vector3(0f, 0.77f, 0f);
            boxCollider.size = new Vector3(0.4f, 1.14f, 0.4f);
        }


#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!isLerping && transform.position.x < 5)
            {
                StartLerping(Vector3.right);
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!isLerping && transform.position.x > -5)
            {
                StartLerping(Vector3.left);
            }
        }

        if (Input.GetButtonDown("Jump") && onGround &&jumpRemaining<=0)
        {
            anim.SetTrigger("Jump");
            jumpRemaining = jumpDuration;
            audioController.jump.Play();
            rb.AddForce(jumpVelocity, ForceMode.VelocityChange);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)&&slidingRemaining<=0)
        {
            anim.SetTrigger("Slide");
            slidingRemaining = slidingDuration;
        }
#else
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];

            if(myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;
            }

            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;
                float x = touchEnd.x - touchOrigin.x;
                float y = touchEnd.y - touchOrigin.y;
                touchOrigin.x = -1; //make sure that this if-else is breaked.
                if(Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x > 0)
                    {
                        if(!isLerping && transform.position.x < 5)
                        {
                            StartLerping(Vector3.right);
                        }
                    }

                    else if (x < 0)
                    {
                        if (!isLerping && transform.position.x > -5)
                        {
                            StartLerping(Vector3.left);
                        }
                    }
                }

                else if(Mathf.Abs(y) > Mathf.Abs(x))
                {
                    if (y > 0 && onGround &&jumpRemaining<=0)
                    {
                        jumpRemaining = jumpDuration;
                        anim.SetTrigger("Jump");
                        rb.AddForce(jumpVelocity, ForceMode.VelocityChange);
                    }

                    else if (y < 0 &&slidingRemaining<=0)
                    {
                        anim.SetTrigger("Slide");
                        slidingRemaining = slidingDuration;
                    }
                }
            }
        }
#endif
    }

    private void FixedUpdate()
    {
        Run();
        onGround = isGrounded();
        GameStatus.distanceTravelled = transform.localPosition.z;

        if (isLerping)
        {
            float timeSinceStarted = Time.time - timeStarted;
            float percentageComplete = timeSinceStarted / durationOfLerp;

            Vector3 newPos = Vector3.Lerp(startPos, endPos, percentageComplete);
            transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);

            if (percentageComplete >= 1.0f)
            {
                isLerping = false;
            }
        }
    }

    void StartLerping(Vector3 direction)
    {
        isLerping = true;
        timeStarted = Time.time;

        startPos = transform.position;
        endPos = transform.position + direction * distanceToMove;

    }

    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);
    }

    void Run()
    {
        if(rb.velocity.z >= acceleration)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, acceleration);
        }
        rb.AddForce(0f, 0f, acceleration, ForceMode.Acceleration);
        anim.SetBool("Run", true);
    }
}
