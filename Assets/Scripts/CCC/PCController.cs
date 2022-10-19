using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : MonoBehaviour
{
    [Header("PC Movement Variables")]
    [SerializeField] float pcSpeed;
    [SerializeField] float forwardTiltAdjust = 8.5f;
    [SerializeField] float forwardTiltCutoff = 5f;
    float zMove;
    Rigidbody rb; //the rigidbody of the PC

    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ForwardBackAxis();
        var forwardMovement = Camera.main.GetComponent<NewThirdPersonCamera>().GetCameraForwardVector() * zMove;
        movement = new Vector3(0f, Physics.gravity.y,0f ) + forwardMovement; //get the accelerometer values in the real world  x and y and place them against Unity's x and y
         
        
    }

    private void FixedUpdate()
    {
        //move the pc using the rigidbody by applying a velocity to it
        rb.velocity = movement * pcSpeed;
    }

    void ForwardBackAxis()
    {
        //records whether movement is forward or back, Forward = 1, Back = -1
        zMove = 0f;

        //Get's input from the accelerometer
        float forwardBackMovement = Input.acceleration.y;

        //scale movement by a factor of 10
        float scaledForwardBackMovement = forwardBackMovement * 10f;

        //cancel out all rotation past zero(device flat on the table)
        if (scaledForwardBackMovement > 0)
        {
            scaledForwardBackMovement = 0;
        }

        //only work with positive values
        scaledForwardBackMovement = Mathf.Abs(scaledForwardBackMovement);


        //cut of values less than tilt cutoff as these the device ergonomics at that angle
        //are undesirable
        if (scaledForwardBackMovement < forwardTiltCutoff)
        {
            scaledForwardBackMovement = 0;
            zMove = 0f;
        }
        else if (scaledForwardBackMovement > forwardTiltCutoff)
        {
            //use the forwardTiltAdjust to determine forward and back movement
            //forward zMove = 1; backward zMove = -1;
            if (scaledForwardBackMovement < forwardTiltAdjust)
            {
                zMove = 1;
            }
            else
            {
                zMove = -1;
            }
        }
    }
 
}
