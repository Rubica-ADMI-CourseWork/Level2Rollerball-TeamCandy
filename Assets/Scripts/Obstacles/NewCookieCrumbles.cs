using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCookieCrumbles : MonoBehaviour
{
    public float fallTime = 5f;
    public float respawnTime = 30f;

    public GameObject cookieVisuals;
    Collider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            //Debug.DrawRay(contact.point, contact.normal, Color.white);
            if (collision.gameObject.tag == "Ball")
            {
                StartCoroutine(Fall());
            }
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallTime);

        AudioManager.instance.PlaySound("CookieCrumbles"); //Play falling platform audio

        cookieVisuals.SetActive(false);
        boxCollider.enabled = false;

        yield return new WaitForSeconds(respawnTime);

        cookieVisuals.SetActive(true);
        boxCollider.enabled = true;
    }
}
