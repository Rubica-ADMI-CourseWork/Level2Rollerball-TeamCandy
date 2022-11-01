using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashAmmo : MonoBehaviour
{
    public float timeOnScreen = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyStickyAmmo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroyStickyAmmo()
    {
        yield return new WaitForSeconds(timeOnScreen);
        Destroy(gameObject);
    }
}
