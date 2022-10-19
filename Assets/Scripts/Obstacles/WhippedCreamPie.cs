using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhippedCreamPie : MonoBehaviour
{
    [Header("WhippedCream Pie Variables")]
    [SerializeField] GameObject whippedcreamPieExplosion;
    [SerializeField] float explosionTimeOnScreen = 5f;

    private void Awake()
    {
        whippedcreamPieExplosion.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            StartCoroutine(ShowAndHideWhippedCreamExplosion());
        }
    }

    IEnumerator ShowAndHideWhippedCreamExplosion()
    {
        whippedcreamPieExplosion.SetActive(true);
        yield return new WaitForSeconds(explosionTimeOnScreen);
        whippedcreamPieExplosion.SetActive(false);
    }
}
