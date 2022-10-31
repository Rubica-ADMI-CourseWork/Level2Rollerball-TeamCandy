using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PCController : MonoBehaviour
{
    public enum AmmoStates
    {
        NoBullets,
        StickyandLiquorice,
        PopRock,
        SourSplash
    }

    public AmmoStates currentAmmoState;

    [Header("PC Variables")] 
    Rigidbody rb;

    [Header("Special Ability Variables")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float bulletSpeed;

    GameObject clone; //the bullet being instantiated

    public float curveHight = 25f;
    public float gravity = -18f;
        
    Vector3 dir; //The direction we want the bullet to follow

    
    [Header("Level Checkpoint Variables")]
    List<Transform> levelCheckpoints;


    [Header("Score Variables")]
    public int score;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        levelCheckpoints = new List<Transform>();

        score = 0; // at the beginning score is 0

    }

    private void Update()
    {
                
        switch (currentAmmoState)
        {
            case AmmoStates.NoBullets:

                break;

            case AmmoStates.StickyandLiquorice:

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 30f))
                {
                    //Debug.Log(hit.point);

                    if (Input.GetMouseButtonDown(0))
                    {
                        Launch(hit.point);

                    }

                }

                break;

            case AmmoStates.PopRock:

                Shooting();

                break;

                case AmmoStates.SourSplash:

                break;
        }
                
    }


    void Shooting()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 30f))
        {
            Debug.Log(hit.point);
            
            if (Input.GetMouseButtonDown(0))
            {

                GameObject bullet = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity) as GameObject;

                dir = hit.point - transform.position;
                Vector3 newDir = dir.normalized;


                bullet.GetComponent<Rigidbody>().velocity = newDir * bulletSpeed;
            }
        }

    }

    void Launch(Vector3 target)
    {

        clone = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity) as GameObject;

        clone.GetComponent<Rigidbody>().AddForce(Vector3.up * gravity);

        clone.GetComponent<Rigidbody>().velocity = CalculateLaunchData(target).initialVelocity;

        
    }

    LaunchData CalculateLaunchData(Vector3 target)
    {
        float displacementY = target.y - transform.position.y;
        Vector3 displacementXZ = new Vector3(target.x - transform.position.x, 0, target.z - transform.position.z);
        float time = Mathf.Sqrt(-2 * curveHight / gravity) + Mathf.Sqrt(2 * (displacementY - curveHight) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * curveHight);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);

        
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }

      

    private void OnTriggerEnter(Collider c)
    {
        if(c.tag == "RegularPickup")
        {
            score += 1; //increase the score count by 1
            UIManager.instance.scoreText.text = score.ToString(); //update the UI score text
            UIManager.instance.scoreVictoryScreenText.text = score.ToString(); //update the UI score text on the victory screen

            Destroy(c.gameObject); //Destroy the pickup after collision
        }

        if(c.tag == "SpecialPickup")
        {
            score += 5; //increase the score count by 5
            UIManager.instance.scoreText.text = score.ToString(); //update the UI score text
            UIManager.instance.scoreVictoryScreenText.text = score.ToString(); //update the UI score text on the victory screen

            Destroy(c.gameObject); //Destroy the pickup after collision
        }

        if (c.tag == "Death")
        {
            OnDeath();
        }

        if (c.tag == "LevelCheckpoint")
        {
            Transform newCheckpoint = c.gameObject.transform; //store the collided checkpoint
            levelCheckpoints.Add(newCheckpoint); //add it to the list
        }

        if(c.tag == "FinalLevelCheckpoint")
        {
            UIManager.instance.victoryScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void OnDeath()
    {
        int i = levelCheckpoints.Count - 1;
        Transform recentCheckpoint = levelCheckpoints[i];
        gameObject.transform.position = recentCheckpoint.position; //on death the player position is moved to the recently passed checkpoint as stored in the list
                
    }

    
}
