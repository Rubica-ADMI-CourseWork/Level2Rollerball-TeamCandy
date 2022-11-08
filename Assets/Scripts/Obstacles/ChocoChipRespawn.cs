using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocoChipRespawn : MonoBehaviour
{
    [Header("Choco chip Variables")]
    [SerializeField] GameObject chocoChipVisuals;

    public float timeBeforeRespawn = 20f;

    Collider sphereCollider; //Access the attached collider


    private void Awake()
    {
        sphereCollider = GetComponent<Collider>(); //get the collider attached to the gameobject
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Sour")
        {
            StartCoroutine(DeactivateChipAfterAmmoCollision());
        }
    }

    IEnumerator DeactivateChipAfterAmmoCollision()
    {
        chocoChipVisuals.SetActive(false); //Deactivate the chip after ammo collision
        sphereCollider.enabled = false; //Deactivate the chip collider after ammo collision

        yield return new WaitForSeconds(timeBeforeRespawn);

        chocoChipVisuals.SetActive(true); //Activate the chip after ammo collision
        sphereCollider.enabled = true; //Activate the chip collider after ammo collision
    }
}
