using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : MonoBehaviour
{
    [Header("PC Movement Variables")]
    Rigidbody rb; //the rigidbody of the PC
    [SerializeField] float pcSpeed;
  

   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 movement = new Vector3(Input.acceleration.y, -1f, - Input.acceleration.x); //get the accelerometer values in the real world  x and y and place them against Unity's x and y
        rb.velocity = movement * pcSpeed; //move the pc using the rigidbody by applying a velocity to it
        
    }

 
}
