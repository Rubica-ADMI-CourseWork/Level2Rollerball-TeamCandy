using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [Header("Input receiver")]
    [Tooltip("reference to the Input receiving component")]
    /// that receives the accelerometer input)]
    public InputReceiver inputReceiver;
    public float TurnInput { get; private set; }
    [SerializeField]private float lookSensitivity;
    [SerializeField]private float lookSpeed;

    [Header("Target for camera to follow")]
    public GameObject target;

    [Header("Camera height offset above player")]
    public float heightOffsetAbovePlayer;

    [Header("Distance in Z of camera behind the player")]
    public float distanceInZbehindPlayer;
    Vector3 ballHeading;

    /// <summary>
    /// Cache of the camera offset from the ball/character every frame
    /// </summary>
    private Vector3 offset;

    #region Monobehaviour Callbacks
    private void OnEnable()
    {
        inputReceiver.OnSideToSideAccelerometerMovement += SetTurnInput;
    }
    private void Update()
    {
        offset = target.transform.position;
        offset.y += heightOffsetAbovePlayer;

    }

    private void LateUpdate()
    {
        Vector3 dist = new Vector3(0f, 0f, -distanceInZbehindPlayer);
        Quaternion rot = Quaternion.Euler(0f, TurnInput, 0f);
        transform.position = offset + rot * dist;

        transform.LookAt(target.transform);
    }

    private void OnDisable()
    {
        inputReceiver.OnSideToSideAccelerometerMovement -= SetTurnInput;
    }

    #endregion

    #region Utility Callbacks
    /// <summary>
    /// Input receiving callback Receives input from the input receiver in the accelerometer x axis
    /// </summary>
    /// <param name="inputValue"></param>   
    private void SetTurnInput(float inputValue)
    {
        Debug.Log("Inside SetTurnInpu");
        TurnInput += inputValue * lookSensitivity * lookSpeed;
    } 
    #endregion

    #region Public setters
    /// <summary>
    /// Receives velocity data from the PC Rigidbody and attempts to 
    /// use that to determine rotation about the y axis
    /// </summary>
    /// <param name="VelocityValue"></param>
    public void TurnAccordingToBallMovement(Vector3 VelocityValue)
    {
        //SetTurnInput(VelocityValue.x);
        //ballHeading = VelocityValue;
    } 
    #endregion

    #region Public Getters
    /// <summary>
    /// Returns the forward facing vector of the camera
    /// </summary>
    /// <returns></returns>
    public Vector3 GetForwardVector()
    {
        Quaternion rot = Quaternion.Euler(0f, TurnInput, 0f);
        return rot * Vector3.forward;
    }

    /// <summary>
    /// Returns the right facing vector of the camera
    /// </summary>
    /// <returns></returns>
    internal Vector3 GetRightVector()
    {
        Quaternion rot = Quaternion.Euler(0f, TurnInput, 0f);
        return rot * Vector3.right;
    }
} 
#endregion
