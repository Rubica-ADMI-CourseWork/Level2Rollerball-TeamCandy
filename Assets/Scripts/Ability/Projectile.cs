using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Sticky Ammo variables")]
    public GameObject stickyAmmoPrefab;
    GameObject stickyAmmo;

    [Header("Liqorice Ammo variables")]
    public GameObject bridgeAmmoPrefab;
    GameObject bridgeAmmo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            AudioManager.instance.PlaySound("StickyAmmo");

            stickyAmmo = Instantiate(stickyAmmoPrefab, transform.position, Quaternion.identity) as GameObject;
                        
            Destroy(gameObject);
                       
        }

        if (collision.gameObject.CompareTag("Bridge"))
        {
            AudioManager.instance.PlaySound("StickyAmmo");

            bridgeAmmo = Instantiate(bridgeAmmoPrefab, collision.transform.position, Quaternion.identity) as GameObject;

           Destroy (collision.gameObject);

           Destroy (gameObject);

        }
    }
        
}
