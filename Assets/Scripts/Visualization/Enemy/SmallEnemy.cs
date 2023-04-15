using UnityEngine;
using UnityEngine.AI;
using Visualization;

public class SmallEnemy : MonoBehaviour
{
    private NavMeshAgent enemyNavMeshAgent;
    private Animator animator;
    private SphereCollider sphereCollider;
    private int nextWaypoint;
    private float waitTimer;

    private float meleeTimer;

    public Transform playerTransform; // Main Camera of the player has to be dragged here
    public PlayerCharacter player;
    public Path patrolPath;
    public bool playerInDetectionRange = false;
    public bool playerInAttackRange = false;
    public float walkSpeed;
    public float runSpeed;
    public float sightRange;
    public float patrolTurnWaitTime;
    public float meleeDamage;
    public float meleeRate;

    public GameObject enemyProjectilePrefab;
    public float fireRateTimer;
    public float fireRate; // The pause between shots in seconds
    public Transform projectileSpawn;
    public float arcRange;

    private void Awake()
    {
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = sightRange;
    }

    void FixedUpdate()
    {
        if (playerInDetectionRange)
        {
            if (playerInAttackRange)
            {
                //animator.SetTrigger("Attack");
                enemyNavMeshAgent.transform.LookAt(playerTransform);

                meleeTimer += Time.fixedDeltaTime;
                if (meleeTimer > meleeRate)
                {
                    meleeTimer = 0;
                    player.TakeDamage(meleeDamage);
                }
            }
            else
            {
                fireRateTimer += Time.deltaTime;

                if (fireRateTimer > fireRate)
                {
                    fireRateTimer = 0;
                    Shoot(playerTransform.position);
                }

                enemyNavMeshAgent.speed = runSpeed;
                enemyNavMeshAgent.transform.LookAt(playerTransform);
                enemyNavMeshAgent.SetDestination(playerTransform.position + new Vector3(0, 0, 1f));
            }
        }
        else
        {
            Patrol();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInDetectionRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInDetectionRange = false;
        }
    }

    void Patrol()
    {
        enemyNavMeshAgent.destination = patrolPath.waypoints[nextWaypoint].position;
        enemyNavMeshAgent.isStopped = false;
        enemyNavMeshAgent.speed = walkSpeed;

        // Changing waypoint when current waypoint is reached through the NavMeshAgent
        if (enemyNavMeshAgent.remainingDistance <= enemyNavMeshAgent.stoppingDistance && !enemyNavMeshAgent.pathPending)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > patrolTurnWaitTime)
            {
                // Looping through waypoints
                nextWaypoint = (nextWaypoint + 1) % patrolPath.waypoints.Length;
                waitTimer = 0;
                enemyNavMeshAgent.speed = walkSpeed;
            }
        }
    }

    void Shoot(Vector3 targetPosition)
    {
        GameObject firedProjectile = GameObject.Instantiate(enemyProjectilePrefab, projectileSpawn.position, Quaternion.identity);
        float projectileSpeed = firedProjectile.GetComponent<Projectile>().projectileSpeed;
        firedProjectile.GetComponent<Rigidbody>().velocity = (targetPosition - projectileSpawn.position).normalized * projectileSpeed;

        iTween.PunchPosition(firedProjectile, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 2f));
    }
}