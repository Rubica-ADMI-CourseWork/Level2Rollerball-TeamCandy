using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieCrumbles : MonoBehaviour
{
    [Header("CookieCrumbles Variables")]
    public GameObject fallingPlatform;
    public float timeBeforeItStartsFalling = 3f;
    public float timeItTakesToReform = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FallingAndReformingPlatform());  
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FallingAndReformingPlatform()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBeforeItStartsFalling);
            fallingPlatform.SetActive(false);
            Debug.Log("I'm falling");
            yield return new WaitForSeconds(timeItTakesToReform);
            fallingPlatform.SetActive(true);
            Debug.Log("I'm reforming");
        }
        
    }
}
