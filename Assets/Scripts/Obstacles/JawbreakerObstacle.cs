using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawbreakerObstacle : MonoBehaviour
{
    [Header("Jawbreaker Movement Variables")]
    [SerializeField] float jawbreakerSpeed = 5f;

    Vector3 movement;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * jawbreakerSpeed * Time.deltaTime);
    }

   
}
