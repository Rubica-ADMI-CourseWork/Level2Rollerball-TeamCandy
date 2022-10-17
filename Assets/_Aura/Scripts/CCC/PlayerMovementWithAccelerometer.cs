using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public enum PlayerForceType
{
    PlainForce,
    VelocityForce,
    ImpulseForce,
    AccelerationForce
}

public class PlayerMovementWithAccelerometer : MonoBehaviour
{
    [Header("Player Movement Variables")]
    Rigidbody playerRB;
    [SerializeField] float forwardTiltAdjust = 8f;
    [SerializeField] float forwardTiltCutoff = 6f;
    [SerializeField] TMP_Text debugText;
    [SerializeField] float moveForce;

    //cache of moveInput to pass on to rigidbody following handling player input
    Vector3 moveInput = Vector3.zero;

    //cache of moment to moment heightMovement (to handle jump situation)
    Vector3 heightMovement = Vector3.zero;

    //final cache of movement direction to move to in each fixed update frame 
    Vector3 moveDirection = Vector3.zero;

    public PlayerForceType forceType;

    [Header("Camera Variables")]
    [SerializeField] Transform cameraTransform;

    //cache reference to the camera script(RollerBallCameraBehaviour)
    RollerBallCameraBehaviour cameraLogic;

    [Header("Ground Variables")]
    //cache reference to ground checker component
    GroundChecker groundChecker;

    private void Start()
    {
        InitializePlayer();
    }

    private void InitializePlayer()
    {
        groundChecker = GetComponent<GroundChecker>();
        cameraLogic = cameraTransform.GetComponent<RollerBallCameraBehaviour>();
        playerRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //records whether movement is forward or back, Forward = 1, Back = -1
        float zMove = 0f;
       
        //Get's input from the accelerometer
        float forwardBackMovement = Input.acceleration.y;

        //scale movement by a factor of 10
        float scaledForwardBackMovement = forwardBackMovement * 10f;

        //cancel out all rotation past zero(device flat on the table)
        if(scaledForwardBackMovement > 0)
        {
            scaledForwardBackMovement = 0;
        }

        //only work with positive values
        scaledForwardBackMovement = Mathf.Abs(scaledForwardBackMovement);
      

        //cut of values less than tilt cutoff as these the device ergonomics at that angle
        //are undesirable
        if(scaledForwardBackMovement < forwardTiltCutoff)
        {
            scaledForwardBackMovement = 0;
            zMove = 0f;
        }
        else if(scaledForwardBackMovement > forwardTiltCutoff)
        {
            //use the forwardTiltAdjust to determine forward and back movement
            //forward zMove = 1; backward zMove = -1;
            if(scaledForwardBackMovement < forwardTiltAdjust)
            {
                zMove = 1;
            }
            else
            {
                zMove = -1;
            }
        }

        Debug.Log("ZMove: "+zMove);//ToDo:remove after testing
       
            
        //cache the values for use in fixed update
        moveInput.z =  zMove * moveForce;    
    }


    private void FixedUpdate()
    {
        //move the ball forward and back in relation to the camera forward vector
        Vector3 forwardMovement = cameraLogic.GetCameraForwardVector() * moveInput.z;

        //set up height movement (rudimentary can be fleshed out later)
        if (groundChecker.isGrounded)
        {
            heightMovement.y = 0f;
        }
        else
        {
            heightMovement.y = Physics.gravity.y;
        }

        moveDirection = forwardMovement + heightMovement;

        switch (forceType)
        {
            case PlayerForceType.PlainForce:
                playerRB.AddForce(moveDirection, ForceMode.Force);
                break;

            case PlayerForceType.VelocityForce:
                playerRB.AddForce(moveDirection, ForceMode.VelocityChange);
                break;

            case PlayerForceType.ImpulseForce:
                playerRB.AddForce(moveDirection, ForceMode.Impulse);
                break;

            case PlayerForceType.AccelerationForce:
                playerRB.AddForce(moveDirection, ForceMode.Acceleration);
                break;

        }
    }
}


