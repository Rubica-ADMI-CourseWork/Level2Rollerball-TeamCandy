using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SourSplashProjectile : MonoBehaviour
{
    [Header("Sour Splash Ammo variables")]
    public GameObject sourSplashAmmoPrefab;
    GameObject sourSplashAmmo;

    public float TimeBeforeSourExplosion = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LaunchSourSplashExplosion());
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    IEnumerator LaunchSourSplashExplosion()
    {
        yield return new WaitForSeconds(TimeBeforeSourExplosion);

        AudioManager.instance.PlaySound("SourSplashAmmo");

        sourSplashAmmo = Instantiate(sourSplashAmmoPrefab, transform.position, Quaternion.identity) as GameObject;

        Destroy(gameObject);
    }

    void LaunchOnEnemyCollision()
    {
        AudioManager.instance.PlaySound("SourSplashAmmo");

        sourSplashAmmo = Instantiate(sourSplashAmmoPrefab, transform.position, Quaternion.identity) as GameObject;

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "JellyEnemy")
        {
            LaunchOnEnemyCollision();
        }
        if(collision.gameObject.tag == "CandyCaneEnemy")
        {
            LaunchOnEnemyCollision();
        }
    }
}
