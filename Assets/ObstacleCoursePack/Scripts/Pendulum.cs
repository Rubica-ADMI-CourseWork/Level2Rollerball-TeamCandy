using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
	public float speed = 1.5f;
	public float limit = 75f; //Limit in degrees of the movement
	

    // Update is called once per frame
    void Update()
    {
		float angle = limit * Mathf.Sin(Time.time * speed);
		transform.localRotation = Quaternion.Euler(angle, 0, 0);
	}
}
