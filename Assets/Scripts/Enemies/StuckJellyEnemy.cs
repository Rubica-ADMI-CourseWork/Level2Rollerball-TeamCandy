using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckJellyEnemy : MonoBehaviour
{
    public GameObject jellyEnemyVisuals;
    Collider boxCollider; //Access the attached collider

    public float timeBeforeRespawn = 60f;

    public GameObject specialPickupPrefab;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<Collider>(); //get the collider
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider c)
    {   
        if (c.tag == "Sour")
        {
            StartCoroutine(DestroyThenRespawnEnemies());
        }

    }

    IEnumerator DestroyThenRespawnEnemies()
    {
        jellyEnemyVisuals.SetActive(false); //Deactivate the visuals
        boxCollider.enabled = false; //Deactivate the box collider

        GameObject pickup = Instantiate(specialPickupPrefab, new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z), Quaternion.identity) as GameObject; //Spawn a pickup after death

        
        yield return new WaitForSeconds(timeBeforeRespawn); //Wait sometime before respawning

        jellyEnemyVisuals.SetActive(true); //Activate the visuals
        boxCollider.enabled = true; //Activate the box collider
        

    }
}
