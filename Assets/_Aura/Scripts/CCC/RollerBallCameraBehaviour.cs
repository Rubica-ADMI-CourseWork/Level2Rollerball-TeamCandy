using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerBallCameraBehaviour : MonoBehaviour
{

    [Header("Distance above player")]
    [SerializeField] float heightOffset;

    [Header("Distance behind player")]
    [SerializeField] float zOffsetBehindPlayer;

    [Header("Sensitivity of the accelerometer readings")]
    [SerializeField] float rotationSensitivity;

    GameObject player;
    Vector3 cameraTarget = Vector3.zero;
    float horizontalMovement;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //Set Camera offset behind player at heightOffsetDistance
        SetUpCameraTarget();

        HandleAccelerometerInput();
    }
    private void LateUpdate()
    {
        Vector3 camOffset = new Vector3(0, 0, -zOffsetBehindPlayer);

        Quaternion camRotation = Quaternion.Euler(0,horizontalMovement,0);
        transform.position = cameraTarget + camRotation * camOffset;

        transform.LookAt(cameraTarget);
    }

    private void HandleAccelerometerInput()
    {
        horizontalMovement += Input.acceleration.x * rotationSensitivity;
    }

    private void SetUpCameraTarget()
    {
        cameraTarget = player.transform.position;
        cameraTarget.y += heightOffset;
    }


    /// <summary>
    /// Returns the forward vector of the camera regardless of rotation
    /// </summary>
    /// <returns>Vector3</returns>
    public Vector3 GetCameraForwardVector()
    {
        Quaternion rot = Quaternion.Euler(0f, horizontalMovement, 0f);

        return rot * Vector3.forward;
    }
}
