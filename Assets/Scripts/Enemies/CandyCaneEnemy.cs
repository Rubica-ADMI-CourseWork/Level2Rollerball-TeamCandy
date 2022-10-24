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


    [Header("Enemy Idle/Patrol Variables")]
    [SerializeField] float proximityDstFromTarget = 10f;

    [SerializeField] Transform[] waypoints;
    int currentWaypoint = 0;


    [Header("Enemy Attack Variables")]
    [SerializeField] float pushingForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        InvokeRepeating("SetDestination", 1.5f, decisionDelay);

        currentEnemyState = EnemyState.Idle;
        target = GameObject.FindGameObjectWithTag("Ball").transform;

        if (currentEnemyState == EnemyState.Idle) pathfinder.SetDestination(waypoints[currentWaypoint].position);
    }

    // Update is called once per frame
    void Update()
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
        if (collision.collider.CompareTag("Ball"))
        {
            //currentEnemyState = EnemyState.Attacking;
            //pathfinder.enabled = false; //Disable the pathfinder before the attack 

            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.up * pushingForce;

            Debug.Log("I have collided with Player");

            //currentEnemyState = EnemyState.Chasing;
            //pathfinder.enabled = true;

        }
    }
}
