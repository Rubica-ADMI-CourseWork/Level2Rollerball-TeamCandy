using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Camera Movement Variables")]

    //position the camera above the player
    [SerializeField]float heightOffset;

    [SerializeField] Transform target;
    [SerializeField] float distanceBehindTarget = 2;
    [SerializeField] float lookSensitivity;
    [SerializeField] float constantSensitivityModifier;

    //cache of position of camera relative to the target(player)
    Vector3 cameraOffset = Vector3.zero;

    //cache of rotation input along xAxis of accelerometer
    float xAxisInput;

    private void Update()
    {
        //Positioning the camera at target position and add a height
        SetPositionAndHeightOffset();

        //recieve input from xAxis of accelerometer
        xAxisInput += Input.acceleration.x * lookSensitivity * constantSensitivityModifier * Time.deltaTime;

        //the ball's velocity to influnce our pitch
    }


    // Update is called once per frame
    void LateUpdate()
    {
        //move camera behind player
        Vector3 locationBehindplayer = new Vector3(0, 0, -distanceBehindTarget);

        //create rotation to handle the accelerometer input
        Quaternion rotation = Quaternion.Euler(0, xAxisInput, 0);

        //set the position by adding the rotation around y axis and the location behind the target
        transform.position = cameraOffset + (rotation * locationBehindplayer);

        //as we rotate around player, also look at player
        transform.LookAt(target.position);



    }
    private void SetPositionAndHeightOffset()
    {
        cameraOffset = target.position;
        cameraOffset.y += heightOffset;
        transform.position = cameraOffset;
    }

    //[Header("Camera Movement Variables")]
    //float yaw;
    //float pitch;
    //[SerializeField] float forwardTiltAdjust = 8.5f;
    //[SerializeField] float forwardTiltCutoff = 5f;
    //public Transform target;
    //public float dstFromTarget = 2;
    //public Vector2 pitchMinMax = new Vector2(-40, 85);
    //public float rotationSmoothTime = 0.12f;
    //Vector3 rotationSmoothVelocity;
    //Vector3 currentRotation;


    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void LateUpdate()
    //{
    //    //records whether movement is forward or back, Forward = 1, Back = -1
    //    float zMove = 0f;

    //    //Get's input from the accelerometer
    //    float forwardBackMovement = Input.acceleration.y;

    //    //scale movement by a factor of 10
    //    float scaledForwardBackMovement = forwardBackMovement * 10f;

    //    //cancel out all rotation past zero(device flat on the table)
    //    if (scaledForwardBackMovement > 0)
    //    {
    //        scaledForwardBackMovement = 0;
    //    }

    //    //only work with positive values
    //    scaledForwardBackMovement = Mathf.Abs(scaledForwardBackMovement);


    //    //cut of values less than tilt cutoff as these the device ergonomics at that angle
    //    //are undesirable
    //    if (scaledForwardBackMovement < forwardTiltCutoff)
    //    {
    //        scaledForwardBackMovement = 0;
    //        zMove = 0f;
    //    }
    //    else if (scaledForwardBackMovement > forwardTiltCutoff)
    //    {
    //        //use the forwardTiltAdjust to determine forward and back movement
    //        //forward zMove = 1; backward zMove = -1;
    //        if (scaledForwardBackMovement < forwardTiltAdjust)
    //        {
    //            zMove = 1;
    //        }
    //        else
    //        {
    //            zMove = -1;
    //        }
    //    }

    //    yaw += Input.acceleration.x;
    //    pitch -= zMove;
    //    pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

    //    currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

    //    transform.eulerAngles = currentRotation;

    //    transform.position = target.position - transform.forward * dstFromTarget;

    //}
}
