using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class SpecialAbility : MonoBehaviour
{
    [Header("Special Ability Variables")]
    //[SerializeField] GameObject launchPoint;
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
        //Shooting();
    }

    void Shooting()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out RaycastHit hit, 30f))
        {
            Debug.Log( hit.point);

            if (Input.GetMouseButtonDown(0))
            {
                
                GameObject bullet = Instantiate(projectile, new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity) as GameObject;

                dir = hit.point - transform.position;
                Vector3 newDir = dir.normalized;

                
                bullet.GetComponent<Rigidbody>().velocity = newDir * bulletSpeed;
            }
        }
                      
    }


}
