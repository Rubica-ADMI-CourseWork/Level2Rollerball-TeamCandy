using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCMovement : MonoBehaviour
{
    #region Public Fields
    [Header("Reference to the input receiver that is handling the device input")]
    [SerializeField] InputReceiver inputReceiver;
    [Header("Values modify forward and side speed seperately")]
    [SerializeField] float speedModifier;
    [SerializeField] float sideSpeedModifier;
    [Header("Fixed minimum and maximum pc speeds")]
    [SerializeField] float minSpeed, maxSpeed;
    [Tooltip("Serialized for testing purposes")]
    [SerializeField]Vector2 currentPcSpeed;

    [Header("Determines force mode type applied to Rigid body")]
    [SerializeField] ForceTypes forwardMovementForceType;
    [SerializeField] ForceTypes sideMovementForceType;

    [Header("Determines whether the PC character or the Camera is used to determine right vector")]
    [SerializeField] RightVectorType rightVectorType;

    [Header("Determines whether the PC character or the Camera is used to determine forward vector")]
    [SerializeField] ForwardVectorType forwardVectorType;

    [Header("Reference to the Camera and the Movement Focus Object")]
    [SerializeField] CameraControls cameraControls;
    [SerializeField] MovementUtility movementFocus;
    #endregion

    #region Properties
    public float ForwardBackInput { get; private set; }
    public float SideInput { get; private set; } 
    #endregion

    //reference to rigid body attached to the PC
    Rigidbody playerRB;

    #region Monobehavior Callbacks
    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //Subscribe to input receiver events
        inputReceiver.OnForwardBackAccelerometerMovement += SetForwardMoveInput;
        inputReceiver.OnSideToSideAccelerometerMovement += SetSideInput;
    }
    private void FixedUpdate()
    {
        //local cache of ForceMode data to later set using the private utility methods below
        ForceMode forwardForceMode = ForceMode.Force;
        ForceMode sideForceMode = ForceMode.Force;
        //Determine the force mode with which to modify the rigidbody AddForce() method
        forwardForceMode = DetermineForwardForceType(forwardForceMode);
        sideForceMode = DetermineSideForceType(sideForceMode);

        //determine final forward as well as side movement using recieved input * speed modifier
        //using the movementFocus to determine forward and right vectors
        Vector3 ForwardMovement = Vector3.zero;
        Vector3 SideMovement = Vector3.zero;

        //Determine how calculate forward and side vectors and apply force accordingly
        switch (forwardVectorType)
        {
            case ForwardVectorType.MovementFocusForwardVector:
                ForwardMovement = ForwardBackInput * movementFocus.GetForwardVector() * speedModifier;

                break;
            case ForwardVectorType.CamForwardVector:
                ForwardMovement = ForwardBackInput * cameraControls.GetForwardVector() * speedModifier;
                break;
        }
        switch (rightVectorType)
        {
            case RightVectorType.MovementFocusRightVector:
                SideMovement = SideInput * movementFocus.GetRightVector() * sideSpeedModifier;
                break;
            case RightVectorType.CamRightVector:
                SideMovement = SideInput * cameraControls.GetRightVector() * sideSpeedModifier;
                break;
        }

        //Apply forward and side force seperately for easier tweaking in editor
        playerRB.AddForce(ForwardMovement, forwardForceMode);
        playerRB.AddForce(SideMovement, sideForceMode);

        //Debug.Log($"PCMovement: Fixed Update() Forward Movement: {ForwardMovement}");
        Debug.Log($"PCMovement: Fixed Update() Forward Velocity: {playerRB.velocity.z}");
        currentPcSpeed = playerRB.velocity;
        SpeedClamp(currentPcSpeed);
        //TurnCamBasedOnVelocity(playerRB.velocity);
    }
    private void OnDisable()
    {
        //unsubscribe to input receiver events
        inputReceiver.OnForwardBackAccelerometerMovement -= SetForwardMoveInput;
        inputReceiver.OnSideToSideAccelerometerMovement -= SetSideInput;
    }
    #endregion

    #region Private Utility
    private ForceMode DetermineForwardForceType(ForceMode forwardForceMode)
    {
        switch (forwardMovementForceType)
        {
            case ForceTypes.AccelerationType:
                forwardForceMode = ForceMode.Acceleration;
                break;
            case ForceTypes.VelocityChangeType:
                forwardForceMode = ForceMode.VelocityChange;
                break;
            case ForceTypes.ForcePushType:
                forwardForceMode = ForceMode.Force;
                break;
        }

        return forwardForceMode;
    }

    private ForceMode DetermineSideForceType(ForceMode sideForceMode)
    {
        switch (forwardMovementForceType)
        {
            case ForceTypes.AccelerationType:
                sideForceMode = ForceMode.Acceleration;
                break;
            case ForceTypes.VelocityChangeType:
                sideForceMode = ForceMode.VelocityChange;
                break;
            case ForceTypes.ForcePushType:
                sideForceMode = ForceMode.Force;
                break;
        }

        return sideForceMode;
    }
    #endregion

    //private void TurnCamBasedOnVelocity(Vector3 velocity)
    //{
    //    Debug.Log(velocity);
    //    var xVelocityValue = velocity.x;
    //    var zVelocityValue = velocity.z;

    //    cameraControls.TurnAccordingToBallMovement(xVelocityValue);

    //    Debug.Log("Ball Velocity " + velocity);
    //    Debug.Log("XVelocityValue: " + xVelocityValue);

    //}//TODO:May be deprecated in future

    #region Setter Callbacks
    private void SetForwardMoveInput(float moveInput)
    {
        ForwardBackInput = moveInput;
    }

    private void SetSideInput(float moveInput)
    {
        SideInput = moveInput;
    }
    #endregion

    void SpeedClamp(Vector2 currentPcSpeed)
    {
        //if (currentPcSpeed >= maxSpeed)
        //{
        //    currentPcSpeed = maxSpeed;
        //}
        //else if (currentPcSpeed <= minSpeed)
        //{
        //    currentPcSpeed = minSpeed;
        //}
    }
}
