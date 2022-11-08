using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiiningLolliesAmmoEffect : MonoBehaviour
{
    [Header("Spinning Lollies Stuck Variables")]
    public bool stick = false;
    public GameObject temp;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stick && temp == null)
        {
            stick = false;
            rb.isKinematic = false;
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Sticky")
        {
            stick = true;
            rb.isKinematic = true;
            
            temp = c.gameObject;
        }
    }
}
