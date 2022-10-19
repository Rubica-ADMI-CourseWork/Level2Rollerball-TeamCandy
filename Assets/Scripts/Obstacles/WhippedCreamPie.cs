using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhippedCreamPie : MonoBehaviour
{
    [Header("WhippedCream Pie Variables")]
    [SerializeField] GameObject whippedcreamPieExplosion; //reference to the UI whipped cream splat effect
    [SerializeField] GameObject whippedcreamPieVisuals;
    [SerializeField] float explosionTimeOnScreen = 5f; //how long the effect stays on screen
    Collider sphereCollider; //Access the attached collider

    private void Awake()
    {
        whippedcreamPieExplosion.SetActive(false);
        sphereCollider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            StartCoroutine(ShowAndHideWhippedCreamExplosion());
            whippedcreamPieVisuals.SetActive(false);
            sphereCollider.enabled = !sphereCollider.enabled;
            
        }
    }

    IEnumerator ShowAndHideWhippedCreamExplosion()
    {
        whippedcreamPieExplosion.SetActive(true);
        yield return new WaitForSeconds(explosionTimeOnScreen);
        whippedcreamPieExplosion.SetActive(false);
    }
}
