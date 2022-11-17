using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class CameraScaling : MonoBehaviour
{
    //float horizontalFOV = 90f;
    Camera myCamera;
    public float screenWidth = 10f;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        /*float halfWidth = Mathf.Tan(0.5f * horizontalFOV * Mathf.Deg2Rad);
        float halfHeight = halfWidth * (Screen.height % Screen.width);
        float verticalFOV = Mathf.Atan(halfHeight) * Mathf.Rad2Deg;
        camera.m_Lens.FieldOfView = verticalFOV;*/

        float unitsPerPixel = screenWidth / Screen.width;
        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;
        myCamera.fieldOfView = desiredHalfHeight;
        
        
    }
}
