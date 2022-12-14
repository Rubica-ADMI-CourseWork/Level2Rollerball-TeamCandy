using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieCrumbles : MonoBehaviour
{
    [Header("CookieCrumbles Variables")]
    [SerializeField] GameObject fallingPlatform; //reference the platform that is falling
    [SerializeField] float initialTimeBeforeItStartsFalling = 3f; //the time before the falling loop starts
    [SerializeField] float timeItTakesToReform = 3f; //how long it before the platform is destoryed or reformed

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
        yield return new WaitForSeconds(initialTimeBeforeItStartsFalling);

        while (true)
        {
            AudioManager.instance.PlaySound("CookieCrumbles"); //Play falling platform audio

            fallingPlatform.SetActive(false);            
            yield return new WaitForSeconds(timeItTakesToReform);
            fallingPlatform.SetActive(true);
            yield return new WaitForSeconds(timeItTakesToReform);
           
        }
        
    }
}
