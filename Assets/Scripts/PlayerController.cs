using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("PC Movement Variables")]
    [SerializeField] float pcSpeed = 10f;
    [SerializeField] float forwardTiltAdjust = 8f;
    [SerializeField] float forwardTiltCutoff = 6f;
    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;
    Rigidbody rb;

    Vector2 inputDir;
    [Header("Camera Movement Variables")]
    Transform cameraT;


    // Start is called before the first frame update
    void Start()
    {
        cameraT = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        //records whether movement is forward or back, Forward = 1, Back = -1
        float zMove = 0f;

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

        Vector2 input = new Vector2(Input.acceleration.x, zMove);
        inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {

            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);

        }


        //rb.AddForce(inputDir * pcSpeed);
        //rb.MovePosition((Vector2)transform.position + (inputDir * pcSpeed * Time.deltaTime));
        //rb.velocity = inputDir * pcSpeed;     

    }

    /// <summary>
    /// Perform physics calculations for movement using rigidbody
    /// </summary>
    private void FixedUpdate()
    {
        rb.AddForce(inputDir * pcSpeed, ForceMode.Force);
    }
}
