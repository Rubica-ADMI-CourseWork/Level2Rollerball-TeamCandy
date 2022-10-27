using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : MonoBehaviour
{
    [Header("PC Movement Variables")]
    [SerializeField] float pcSpeed = 3f;
    //[SerializeField] float maxPcSpeed = 5f;
    [SerializeField] float forwardTiltAdjust = 8.5f;
    [SerializeField] float forwardTiltCutoff = 5f;


    [Header("Select which physics force to use to move the ball")]
    [SerializeField] ForceType forceType;

    float zMove;
    Rigidbody rb; //the rigidbody of the PC

    Vector3 movement;


    [Header("Level Checkpoint Variables")]
    List<Transform> levelCheckpoints;

    [Header("Special Ability Variables")]
    [SerializeField] GameObject projectile;
    [SerializeField] float bulletSpeed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        levelCheckpoints = new List<Transform>();
        
    }

    private void Update()
    {
        ForwardBackAxis();
        var forwardMovement = Camera.main.GetComponent<NewThirdPersonCamera>().GetCameraForwardVector() * zMove;
        movement = new Vector3(0f, Physics.gravity.y, 0f) + forwardMovement; //get the accelerometer values in the real world  x and y and place them against Unity's x and y

        Shooting();

    }

    private void FixedUpdate()
    {
        //Determine the physics force to apply to ball based on the 'ForceType' Selected
        switch (forceType)
        {
            case ForceType.AccelerationForce:
                rb.AddForce(movement * pcSpeed, ForceMode.Acceleration);
                break;
            case ForceType.PushForce:
                rb.AddForce(movement * pcSpeed, ForceMode.Force);
                break;
            case ForceType.VelocityForce:
                rb.AddForce(movement * pcSpeed, ForceMode.VelocityChange);
                break;
            case ForceType.TorqueForce:
                rb.AddTorque(movement * pcSpeed, ForceMode.Acceleration);
                break;
            case ForceType.ImpulseForce:
                rb.AddForce(movement * pcSpeed, ForceMode.Impulse);
                break;
            default:
                rb.velocity = movement * pcSpeed;
                break;
        }
        //move the pc using the rigidbody by applying a velocity to it



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

    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Death")
        {
            OnDeath();
        }

        if (c.tag == "LevelCheckpoint")
        {
            Transform newCheckpoint = c.gameObject.transform; //store the collided checkpoint
            levelCheckpoints.Add(newCheckpoint); //add it to the list
        }

        if(c.tag == "FinalLevelCheckpoint")
        {
            UIManager.instance.victoryScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void OnDeath()
    {
        int i = levelCheckpoints.Count - 1;
        Transform recentCheckpoint = levelCheckpoints[i];
        gameObject.transform.position = recentCheckpoint.position; //on death the player position is moved to the recently passed checkpoint as stored in the list
                
    }

    void Shooting()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch (0).phase == TouchPhase.Began)
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint (Input.GetTouch(0).position);
                Vector2 dir = touchPos - (new Vector2 (transform.position.x, transform.position.y));
                dir.Normalize ();

                GameObject bullet = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
                bullet.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;
            }
        }
    }
}
