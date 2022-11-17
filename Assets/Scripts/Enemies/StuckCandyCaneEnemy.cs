using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckCandyCaneEnemy : MonoBehaviour
{
    public GameObject candyCaneEnemyVisuals;
    Collider capsuleCollider; //Access the attached collider

    public float timeBeforeRespawn = 60f;

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<Collider>(); //get the collider
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
        candyCaneEnemyVisuals.SetActive(false); //Deactivate the visuals
        capsuleCollider.enabled = false; //Deactivate the box collider
        
        CandyGameManager.instance.currentNumberOfEnemies++; //update the number of enemies killed
        CandyGameManager.instance.enemiesText.text = CandyGameManager.instance.currentNumberOfEnemies.ToString(); //update the number of enemies killed on the UI

        yield return new WaitForSeconds(timeBeforeRespawn); //Wait sometime before respawning

        candyCaneEnemyVisuals.SetActive(true); //Activate the visuals
        capsuleCollider.enabled = true; //Activate the box collider        

    }
}
