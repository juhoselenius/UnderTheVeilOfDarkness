using UnityEngine;
using UnityEngine.AI;
using Visualization;

namespace Visualization
{
    public class SkeletonEnemy : MonoBehaviour
    {
        private NavMeshAgent enemyNavMeshAgent;
        private Animator animator;
        private SphereCollider sphereCollider;
        private int nextWaypoint;
        private float waitTimer;

        private float meleeTimer;

        public Transform playerCameraTransform; // Main Camera of the player has to be dragged here
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

        private bool hit;

        private void Awake()
        {
            enemyNavMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.radius = sightRange;
        }

        void FixedUpdate()
        {
            if (playerInDetectionRange)
            {
                if(playerInAttackRange)
                {
                    animator.SetTrigger("Attack");
                    Vector3 targetPosition = new Vector3(playerCameraTransform.position.x, this.transform.position.y, playerCameraTransform.position.z);
                    enemyNavMeshAgent.transform.LookAt(targetPosition);

                    meleeTimer += Time.fixedDeltaTime;
                    if(meleeTimer > meleeRate)
                    {
                        meleeTimer = 0;

                        // Show damage indicator if damage comes from out of view
                        if (!DamageIndicatorUI.CheckIfObjectInSight(gameObject.transform))
                        {
                            DamageIndicatorUI.CreateIndicator(gameObject.transform);
                        }

                        player.TakeDamage(meleeDamage);
                    }
                }
                else
                {
                    Run();
                    Vector3 targetPosition = new Vector3(playerCameraTransform.position.x, this.transform.position.y, playerCameraTransform.position.z);
                    enemyNavMeshAgent.transform.LookAt(targetPosition);
                    enemyNavMeshAgent.SetDestination(playerCameraTransform.position + new Vector3(0, 0, 2f));
                }
            
            }
            else if (hit)
            {
                enemyNavMeshAgent.speed = runSpeed;
                enemyNavMeshAgent.transform.LookAt(playerCameraTransform);
                enemyNavMeshAgent.SetDestination(playerCameraTransform.position + new Vector3(0, 0, 3f));
            }
            else
            {
                animator.SetBool("Run", false);
                Patrol();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                playerInDetectionRange = true;
            }

            if (other.CompareTag("Rock") || other.CompareTag("StickyBullet") || other.CompareTag("PlayerProjectile") || other.CompareTag("FireBullet") || other.CompareTag("IceBullet"))
            {
                hit = true;
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
            Walk();

            // Changing waypoint when current waypoint is reached through the NavMeshAgent
            if (enemyNavMeshAgent.remainingDistance <= enemyNavMeshAgent.stoppingDistance && !enemyNavMeshAgent.pathPending)
            {
                animator.SetBool("Walk", false);
                waitTimer += Time.deltaTime;
                if (waitTimer > patrolTurnWaitTime)
                {
                    // Looping through waypoints
                    nextWaypoint = (nextWaypoint + 1) % patrolPath.waypoints.Length;
                    waitTimer = 0;
                    Walk();
                }
            }
        }

        public void Walk()
        {
        
            enemyNavMeshAgent.speed = walkSpeed;
            animator.SetBool("Walk", true);
        }

        public void Run()
        {
        
            enemyNavMeshAgent.speed = runSpeed;
            animator.SetBool("Run", true);
        }

        public void Die()
        {
            animator.SetTrigger("Die");
        }
    }
}
