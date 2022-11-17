using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputReceiver : MonoBehaviour
{
    float yAxisAccelerometerInputValue;
    float xAxisAccelerometerInputValue;
    float zAxisAcceloremeterInputValue;

    [Header("serialized for testing purposes")]
    public bool isMovingForward = false;
    //this is the value passed on to the PCMovementScript to determine direction of Force applied in the forward and back axis
    [Header("Serialized for debug purposes.")]
    public float finalForwardBackInput;
    private float finalSideToSideInput;
    float prevValue;

    public event Action<float> OnForwardBackAccelerometerMovement;
    public event Action<float> OnSideToSideAccelerometerMovement;

    [SerializeField] float minValueRangeLimit;//1
    [SerializeField] float maxValueRangeLimit;//0

    [SerializeField] float backForwardCutOff;
    private void Update()
    {
        yAxisAccelerometerInputValue = Input.acceleration.y;
        finalSideToSideInput = Input.acceleration.x;
        zAxisAcceloremeterInputValue= Input.acceleration.z;

        
        //Push input values up to the positive range
        yAxisAccelerometerInputValue = yAxisAccelerometerInputValue + 1;

       
       // only give me values in the positive range
        if (yAxisAccelerometerInputValue < minValueRangeLimit && yAxisAccelerometerInputValue > maxValueRangeLimit)
        {
            prevValue = yAxisAccelerometerInputValue;
           
            if( prevValue > backForwardCutOff)
            {
                finalForwardBackInput = 1f;
                isMovingForward = false;
            }
            if(prevValue < backForwardCutOff)
            {
                finalForwardBackInput = -1f;
                isMovingForward = true;
            }          
        }
        else
        {
            yAxisAccelerometerInputValue = 0f;
            
        }

        //pass this value onward to whichever script is interested in the final value
        OnForwardBackAccelerometerMovement?.Invoke(finalForwardBackInput);
        OnSideToSideAccelerometerMovement?.Invoke(finalSideToSideInput);
    
        
    }

    private void LateUpdate()
    {
       //ToDo:commented out because not in use(apparently)
        //if(yAxisAccelerometerInputValue > prevValue)
        //{
        //    isMovingForward = true;
        //}
        //if(yAxisAccelerometerInputValue < prevValue)
        //{
        //    isMovingForward = false;
        //}
   
    }
}
