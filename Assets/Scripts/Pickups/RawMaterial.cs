using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMaterial : MonoBehaviour
{
    public float proximityDstFrmPlayer = 10f;

    Transform player;
    public GameObject lightBeamVfx;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Ball").transform;                
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) > proximityDstFrmPlayer)
        {            
            lightBeamVfx.SetActive(false);
        }
        else
        {
            lightBeamVfx.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, proximityDstFrmPlayer / 2);
    }
}
