using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*
     * Tilt inputs from accelerometer - create variables to store them
     * Tilt cutoff for the y inputs
     * Forward and backwards movement cutoff
     * Movement with speed value to the forward/back input
     * Move the ball using physics - either rigidbody. velocity or rigidbody.addforce
     */

    Vector2 movementInput;
    Rigidbody rb;
    public float pcSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        rb.AddForce(Vector3.forward * pcSpeed, ForceMode.VelocityChange);
    }

    private void Update()
    {
        float yValue;
        yValue = Input.acceleration.y;
        Debug.Log(yValue);
        yValue *= 10f;

        //Cancel out negative value
        yValue = Mathf.Abs(yValue);
        Debug.Log(yValue);

        if(yValue < 6.0f)
        {
            yValue = 0f;
        }
        Debug.Log(yValue);
       
    }
}
