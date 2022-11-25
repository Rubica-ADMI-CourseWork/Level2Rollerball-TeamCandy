using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CheckpointColorChange : MonoBehaviour
{
    [Header("VFX Variables")]
    public GameObject checkpointVfxList;
    VisualEffect checkpointVfxGraph;

    Color cylinderColor;
    Color ringColor;

    public Color newCylinderColor;
    public Color newRingColor;

    // Start is called before the first frame update
    void Start()
    {
        //checkpointVfxList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Ball")
        {
            Debug.Log("Ball has passed through checkpoint");
            //Change the color of the vfxgraph on collision
            ChangeCheckpointColor();
        }
    }

    void ChangeCheckpointColor()
    {
        Debug.Log("I'm changing color");

        checkpointVfxGraph = checkpointVfxList.GetComponent<VisualEffect>();

        checkpointVfxGraph.SetVector4("Cylinder Color", newCylinderColor); //Vector 4 is to change color variable, so get the string name of the current variable and change it to the new variable
        checkpointVfxGraph.SetVector4("RingColor", newRingColor); //Vector 4 is to change color variable, so get the string name of the current variable and change it to the new variable
    }
}
