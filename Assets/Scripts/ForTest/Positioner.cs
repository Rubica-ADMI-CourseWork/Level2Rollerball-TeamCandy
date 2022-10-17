using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positioner : MonoBehaviour
{
    public Transform target;
    //Transform targetOrientation;

    private float startHeight;
    private float sumHeight;

    private Transform positioner;
        
    //public float rotationSpeed;

    void Awake()
    {
        //targetOrientation = target;
        positioner = transform;
    }

    void Start()
    {

        startHeight = positioner.position.y;
        sumHeight = startHeight - target.position.y;
    }

    void Update()
    {

        positioner.position = new Vector3(target.position.x, target.position.y + sumHeight, target.position.z);

        /*
        //rotate orientation
        Vector3 viewDir = positioner.position;
        targetOrientation.forward = viewDir.normalized;

        //rotate player object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = targetOrientation.forward * verticalInput + targetOrientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
        {
            target.forward = Vector3.Slerp(target.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }*/
    }

}

