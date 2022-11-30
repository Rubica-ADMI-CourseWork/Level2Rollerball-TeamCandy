using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static JellyEnemy;

[RequireComponent(typeof(NavMeshAgent))]

public class JellyEnemy : MonoBehaviour
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

    public GameObject jellyEnemyVisuals;
    Collider boxCollider; //Access the attached collider

    public float timeBeforeRespawn = 10f;
       
    [Header("Enemy Idle/Patrol Variables")]
    [SerializeField] float proximityDstFromTarget =10f;

    [SerializeField] Transform[] waypoints;
    int currentWaypoint = 0;


    [Header("Enemy Attack Variables")]
    Vector3 originalPosition;
    Vector3 attackPosition;

    float attackDistanceThreshold = 1.5f;
    float timeBetweenAttacks = 1;
    float nextAttackTime;

    float percent;


    // Start is called before the first frame update
    void Start()
    {        
        boxCollider = GetComponent<Collider>(); //get the collider

        pathfinder = GetComponent<NavMeshAgent>();
        InvokeRepeating("SetDestination", 1.5f, decisionDelay);

        currentEnemyState = EnemyState.Idle;
        target = GameObject.FindGameObjectWithTag("Ball").transform;

        if (currentEnemyState == EnemyState.Idle) pathfinder.SetDestination(waypoints[currentWaypoint].position);

        //StartCoroutine(UpdatePath());
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

        if (Time.time > nextAttackTime)
        {
            float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;

            if (sqrDstToTarget < Mathf.Pow (attackDistanceThreshold, 2))
            {
                nextAttackTime = Time.time + timeBetweenAttacks;                
                StartCoroutine(Attack());
            }

        }

        if(stick && temp == null)
        {
            stick = false;
            StopAllCoroutines();
            pathfinder.enabled = true;
                       
        }
       
    }

    void SetDestination()
    {
        if (currentEnemyState == EnemyState.Chasing) pathfinder.SetDestination(target.position);
    }

    IEnumerator Attack()
    {
        currentEnemyState = EnemyState.Attacking;        
        pathfinder.enabled = false; //Disable the pathfinder before the attack

        originalPosition = transform.position;
        attackPosition = target.position;

        float attackSpeed = 3f;
        percent = 0;

        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            AudioManager.instance.PlaySound("JellyCollision");

            yield return null;
        }

        currentEnemyState = EnemyState.Chasing;
        pathfinder.enabled = true; //Enable the pathfinder after the attack
        
    }

    //Tried this worked, but kept giving the set destination error way too frequently, guessing cause of the higher refresh rate
    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;

        while (target != null)
        {
            if (currentEnemyState == EnemyState.Chasing)
            {
                Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
                pathfinder.SetDestination(targetPosition);
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, proximityDstFromTarget/2);
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Sticky")
        {            
            stick = true;            
            pathfinder.enabled=false;

            temp = c.gameObject;
        }

        if (c.tag == "Sour")
        {
            StartCoroutine(DestroyThenRespawnEnemies());
        }
                
    }

    IEnumerator DestroyThenRespawnEnemies()
    {
        jellyEnemyVisuals.SetActive(false); //Deactivate the visuals
        boxCollider.enabled = false; //Deactivate the box collider
        pathfinder.enabled = false;

        CandyGameManager.instance.currentNumberOfEnemies++; //update the number of enemies killed
        CandyGameManager.instance.enemiesText.text = CandyGameManager.instance.currentNumberOfEnemies.ToString(); //update the number of enemies killed on the UI

        yield return new WaitForSeconds(timeBeforeRespawn); //Wait sometime before respawning

        jellyEnemyVisuals.SetActive(true); //Activate the visuals
        boxCollider.enabled = true; //Activate the box collider
        pathfinder.enabled = true;

    }
   
}
