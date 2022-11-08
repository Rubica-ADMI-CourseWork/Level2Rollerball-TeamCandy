using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class CandyCaneEnemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chasing,
        Attacking
    }

    EnemyState currentEnemyState;


    [Header("Enemy Variables")]
    [SerializeField] float decisionDelay = 1f;
    NavMeshAgent pathfinder;
    Transform target;

    public bool stick = false;
    public GameObject temp;

    public GameObject candyCaneEnemyVisuals;
    Collider capsuleCollider; //Access the attached collider

    public float timeBeforeRespawn = 10f;

    [Header("Enemy Idle/Patrol Variables")]
    [SerializeField] float proximityDstFromTarget = 10f;

    [SerializeField] Transform[] waypoints;
    int currentWaypoint = 0;


    [Header("Enemy Attack Variables")]
    [SerializeField] float pushingForce = 400f;
    [SerializeField] Transform launchDirection;

    float launchDirectionZ; //The position of the launchDirection in the Z
    float launchDirectionY; //The position of the launchDirection in the Y
    float positionZ; //The position of the enemy in the Z
    float positionY; //The position of the enemy in the Y

    Vector3 launchDir; //The value gotten after subtracting the position of the enemy from the position of the launch direction object


        
    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<Collider>(); //get the collider

        pathfinder = GetComponent<NavMeshAgent>();
        InvokeRepeating("SetDestination", 1.5f, decisionDelay);

        currentEnemyState = EnemyState.Idle;
        target = GameObject.FindGameObjectWithTag("Ball").transform;

        if (currentEnemyState == EnemyState.Idle) pathfinder.SetDestination(waypoints[currentWaypoint].position);
    }

    // Update is called once per frame
    void Update()
    {
        ChangingStates();

        if (stick && temp == null)
        {
            stick = false;
            StopAllCoroutines();
            pathfinder.enabled = true;

        }
    }

    private void ChangingStates()
    {
        if (Vector3.Distance(transform.position, target.position) > proximityDstFromTarget)
        {
            currentEnemyState = EnemyState.Idle;
        }
        else
        {
            currentEnemyState = EnemyState.Chasing;
        }

        if (currentEnemyState == EnemyState.Idle)
        {
            if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.6f)
            {
                currentWaypoint++;
                if (currentWaypoint == waypoints.Length)
                {
                    currentWaypoint = 0;
                }
            }
            pathfinder.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    void SetDestination()
    {
        if (currentEnemyState == EnemyState.Chasing) pathfinder.SetDestination(target.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, proximityDstFromTarget / 2);
    }


    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Ball"))
        {
            LaunchPlayer();
            collision.gameObject.GetComponent<Rigidbody>().AddForce(launchDir * pushingForce, ForceMode.Impulse);

        }
       

    }
       
   
    void LaunchPlayer()
    {
        launchDirectionZ = launchDirection.position.z;
        launchDirectionY = launchDirection.position.y;
        positionZ = transform.position.z;
        positionY = transform.position.y;


        //Launch direction is the difference between our destination and our current position
        launchDir = new Vector3(0f, launchDirectionY - positionY, launchDirectionZ - positionZ);
                   

    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Sticky")
        {
            stick = true;
            pathfinder.enabled = false;

            temp = c.gameObject;
        }

        if (c.tag == "Sour")
        {
            StartCoroutine(DestroyThenRespawnEnemies());
        }

    }

    IEnumerator DestroyThenRespawnEnemies()
    {
        candyCaneEnemyVisuals.SetActive(false); //Deactivate the visuals
        capsuleCollider.enabled = false; //Deactivate the box collider
        pathfinder.enabled = false;

        CandyGameManager.instance.currentNumberOfEnemies++; //update the number of enemies killed
        CandyGameManager.instance.enemiesText.text = CandyGameManager.instance.currentNumberOfEnemies.ToString(); //update the number of enemies killed on the UI

        yield return new WaitForSeconds(timeBeforeRespawn); //Wait sometime before respawning

        candyCaneEnemyVisuals.SetActive(true); //Activate the visuals
        capsuleCollider.enabled = true; //Activate the box collider
        pathfinder.enabled = true;

    }

}
