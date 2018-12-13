using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float acceleration = 20f;

    Rigidbody rb;

    Vector3 endPos;

    Vector3 startPos;

    float durationOfLerp = 0.2f;

    float distanceToMove = 5f;

    bool isLerping = false;

    float timeStarted;

    Vector3 jumpVelocity = new Vector3(0, 5f, 0f);

    private Animator anim;

    //Ground Checking
    [SerializeField]
    Transform groundCheck;
    float groundRadius = 0.1f;
    [SerializeField]
    LayerMask whatIsGround;
    [SerializeField]
    bool onGround;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

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

        if (Input.GetButtonDown("Jump") && onGround)
        {
            anim.SetTrigger("Jump");
            rb.AddForce(jumpVelocity, ForceMode.VelocityChange);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetTrigger("Slide");
        }
    }

    private void FixedUpdate()
    {
        Run();
        onGround = isGrounded();

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
        if(rb.velocity.z >= 20f)
        {
            rb.velocity = new Vector3(0, 0, 20f);
        }
        rb.AddForce(0f, 0f, acceleration, ForceMode.Acceleration);
        anim.SetBool("Run", true);
    }
}
