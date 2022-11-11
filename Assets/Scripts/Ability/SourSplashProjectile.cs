using System.Collections;
using System.Collections.Generic;
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

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        sourSplashAmmo = Instantiate(sourSplashAmmoPrefab, transform.position, Quaternion.identity) as GameObject;

    //        Destroy(gameObject);

    //    }
    //}

    IEnumerator LaunchSourSplashExplosion()
    {
        yield return new WaitForSeconds(TimeBeforeSourExplosion);

        AudioManager.instance.PlaySound("SourSplashAmmo");

        sourSplashAmmo = Instantiate(sourSplashAmmoPrefab, transform.position, Quaternion.identity) as GameObject;

        Destroy(gameObject);
    }
}
