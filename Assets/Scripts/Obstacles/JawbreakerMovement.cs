using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawbreakerMovement : MonoBehaviour
{
    public float speed;
    public float magnitude;

    private void Update()
    {
        transform.position = new Vector3 (SineAmount(), SineAmount(), SineAmount());
    }

    public float SineAmount()
    {
        return magnitude * Mathf.Sin(Time.time * speed);
    }
}
