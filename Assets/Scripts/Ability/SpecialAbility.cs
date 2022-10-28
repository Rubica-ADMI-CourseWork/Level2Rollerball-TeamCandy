using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class SpecialAbility : MonoBehaviour
{
    [Header("Special Ability Variables")]
    [SerializeField] GameObject launchPosition;
    [SerializeField] GameObject projectile;
    [SerializeField] float bulletSpeed;
    

    Vector3 dir; //The direction we want the bullet to follow


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.cyan);

                        
            if (Physics.Raycast(ray, out RaycastHit hit, 30f))
            {
                dir = hit.point - launchPosition.transform.position;
            }

            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(dir * bulletSpeed, ForceMode.Impulse);
        }
    }
}
