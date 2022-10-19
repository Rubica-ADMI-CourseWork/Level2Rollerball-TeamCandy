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
        whippedcreamPieExplosion.SetActive(false); //deactivate it on start
        sphereCollider = GetComponent<Collider>(); //get the collider attached to the gameobject
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            StartCoroutine(ShowAndHideWhippedCreamExplosion());
            whippedcreamPieVisuals.SetActive(false); //Deactivate the pie after pc collision
            sphereCollider.enabled = !sphereCollider.enabled; //Deactivate the pie after pc collision

        }
    }

    IEnumerator ShowAndHideWhippedCreamExplosion()
    {
        whippedcreamPieExplosion.SetActive(true); //activate the whipped cream splat
        yield return new WaitForSeconds(explosionTimeOnScreen); //how long it stays on screen before being deactivated
        whippedcreamPieExplosion.SetActive(false);//deactivate the whipped cream splat
    }
}
