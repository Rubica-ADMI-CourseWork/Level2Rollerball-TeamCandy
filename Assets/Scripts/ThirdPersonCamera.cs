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
}
