using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
	[Header ("Pendulum Swing Variables")]
	public float speed = 1.5f;
	public float limit = 75f; //Limit in degrees of the movement
	public bool randomStart = true; //If you want to modify the start position
	private float random = 0;

	//[Header("Player Variables")]
	//Rigidbody playerRb;
	//[SerializeField] float pushingForce;

	// Start is called before the first frame update
	void Awake()
    {
		if(randomStart)
			random = Random.Range(0f, 1f);
	}

    private void Start()
    {
        AudioManager.instance.PlaySound("JawbreakerCanyon");
    }

    // Update is called once per frame
    void Update()
    {
		float angle = limit * Mathf.Sin((Time.time + random)* speed);
		transform.localRotation = Quaternion.Euler(angle, 0, 0);
	}

    //private void OnCollisionEnter(Collision collision)
    //{
    //	if (collision.collider.CompareTag("Ball"))
    //	{
    //		playerRb = collision.gameObject.GetComponent<PCController>().rb;

    //		playerRb.AddForce(transform.forward * pushingForce);

    //	}
    //}

    //public void OnCollisionEnter(Collision other)
    //{
    //    if (other.collider.CompareTag("Ball"))
    //    {
    //        // how much the character should be knocked back
    //        var magnitude = 5000;
    //        // calculate force vector
    //        var force = transform.position - other.transform.position;
    //        // normalize force vector to get direction only and trim magnitude
    //        force.Normalize();
    //        other.gameObject.GetComponent<PCController>().rb.AddForce(force * magnitude);

    //    }
    //}
}
