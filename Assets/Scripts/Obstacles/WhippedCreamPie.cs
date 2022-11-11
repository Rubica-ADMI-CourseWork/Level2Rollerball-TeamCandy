using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WhippedCreamPie : MonoBehaviour
{
    [Header("WhippedCream Pie Variables")]
    [SerializeField] GameObject whippedcreamPieExplosion; //reference to the UI whipped cream splat effect
    [SerializeField] GameObject whippedcreamPieVisuals;

    [SerializeField] float explosionTimeOnScreen = 5f; //how long the effect stays on screen

    Collider sphereCollider; //Access the attached collider

    public float timeBeforeRespawn = 20f;

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

    private void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Sour")
        {
            StartCoroutine(DeactivatePieAfterAmmoCollision());
        }
    }

    IEnumerator ShowAndHideWhippedCreamExplosion()
    {
        AudioManager.instance.PlaySound("WhippedCreamSplat");

        whippedcreamPieExplosion.SetActive(true); //activate the whipped cream splat
        yield return new WaitForSeconds(explosionTimeOnScreen); //how long it stays on screen before being deactivated
        whippedcreamPieExplosion.SetActive(false);//deactivate the whipped cream splat
    }

    IEnumerator DeactivatePieAfterAmmoCollision()
    {
        whippedcreamPieVisuals.SetActive(false); //Deactivate the pie after ammo collision
        sphereCollider.enabled = false; //Deactivate the pie collider after ammo collision

        yield return new WaitForSeconds(timeBeforeRespawn);

        whippedcreamPieVisuals.SetActive(true); //Activate the pie after ammo collision
        sphereCollider.enabled = true; //Activate the pie after ammo collision
    }
}
