using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyGameManager : MonoBehaviour
{
    public static CandyGameManager instance;

    [Header("Level Checkpoint Variables")]
    [SerializeField] GameObject player;
    GameObject currentPlayer;
    
    Vector3 checkpoint;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }

   
}
