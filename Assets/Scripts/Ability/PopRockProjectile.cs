using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PopRockProjectile : MonoBehaviour
{
    [Header("Pop Rock Ammo Variables")]
    public float delay = 2f;
    public float blastRadius = 5f;
    public float force = 700f;

    float countdown;

    bool hasExploded = false;

    Rigidbody nearbyRb;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f && !hasExploded)
        {
            Explode();

            hasExploded = true;
        }
    }

    void Explode()
    {
        //Get nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        //cache a list of rigidbodies to pass to coroutine later
        List<Rigidbody> rbodies = new List<Rigidbody>();
        foreach (Collider nearbyObject in colliders)
        {
            //Add force
            nearbyRb = nearbyObject.GetComponent<Rigidbody>();

            if (nearbyRb != null)
            {
                if (!nearbyRb.gameObject.CompareTag("SpinningLollies"))
                {
                    rbodies.Add(nearbyRb);
                    nearbyRb.isKinematic = false;
                    nearbyRb.AddExplosionForce(force, transform.position, blastRadius);
                }

            }
        }
        StartCoroutine(TurnOnKinematic(rbodies));

    }

    /// <summary>
    /// Goes through list of rigid bodies, turns isKinematic to true
    /// </summary>
    /// <param name="_rb"></param>
    /// <returns></returns>
    IEnumerator TurnOnKinematic(List<Rigidbody> _rb)
    {
      
        yield return new WaitForSeconds(1f);
       

        foreach (var item in _rb)
        {
           
            item.isKinematic = true;
        }

        //Remove grenade(pop rock ammo)        
        Destroy(gameObject);
    }
}
