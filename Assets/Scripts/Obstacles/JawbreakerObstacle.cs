using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class JawbreakerObstacle : MonoBehaviour
{
    [Header("Jawbreaker Movement Variables")]
    [SerializeField] float jawbreakerSpeed = 5f;

    Vector3 directionalMovement;

    Rigidbody rb;
    
       
    float ballVelocity; //store x axis velocity of the ball at any given time
    public float maxVelocity = 30f; //Store maximum velocity pc gameobject can attain
    public float minVelocity = 10f;

    [Header("Position variables")]
    Vector3 newPosition;
    Vector3 movement;
    Vector3 previousPosition;
    Vector3 forward;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //directionalMovement = new Vector3(0f, Physics.gravity.y, ballVelocity);
        //rb.velocity = directionalMovement * jawbreakerSpeed;

        //ballVelocity = rb.velocity.z;

        //if (ballVelocity >= maxVelocity)
        //{
        //    ballVelocity = maxVelocity;
        //}
        //else if (ballVelocity <= minVelocity)
        //{
        //    ballVelocity = maxVelocity;
        //}

        //JustMove();
        
    }

    
    void JustMove()
    {
        newPosition = transform.position;
        movement = (newPosition - previousPosition);
                      
        if (Vector3.Dot(forward, movement) > 0) //add and height is less than 4 
        {
            directionalMovement = new Vector3(0f, Physics.gravity.y, ballVelocity);
            rb.velocity = directionalMovement * jawbreakerSpeed;
            //rb.velocity = new Vector3(0f, Physics.gravity.y, ballVelocity);
        }
        else if (Vector3.Dot(forward, movement) < 0)
        {
            directionalMovement = new Vector3(0f, Physics.gravity.y, -ballVelocity);
            rb.velocity = directionalMovement * jawbreakerSpeed;
            //rb.velocity = new Vector3(0f, Physics.gravity.y, -ballVelocity);
        }    
    }

}
