using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningLollies : MonoBehaviour
{
    [Header("Rotating Platform Variables")]
    [SerializeField] float speed = 3f;
    [SerializeField] GameObject rotatingBeam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotatingBeam.transform.Rotate(0f, 0f, speed * Time.deltaTime / 0.01f, Space.Self);
    }
}
