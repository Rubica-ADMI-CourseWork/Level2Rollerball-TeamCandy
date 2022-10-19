using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocyShoot : MonoBehaviour
{
    [Header("ChocyShoot Variables")]
    [SerializeField] GameObject protrudingWall;
    [SerializeField] float initialTimeBeforeItStartsRising = 3f;
    [SerializeField] float timeItTakesToReform = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FallingAndReformingWall());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FallingAndReformingWall()
    {
        yield return new WaitForSeconds(initialTimeBeforeItStartsRising);

        while (true)
        {
            yield return new WaitForSeconds(timeItTakesToReform);
            protrudingWall.SetActive(false);
            //Debug.Log("I'm falling");
            yield return new WaitForSeconds(timeItTakesToReform);
            protrudingWall.SetActive(true);
            //Debug.Log("I'm reforming");
        }

    }
}
