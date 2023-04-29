using UnityEngine;
using UnityEngine.AI;

namespace Visualization
{
    public class SmallSkeletonController: MonoBehaviour
    {
        private Animator animator;
        private NavMeshAgent enemyNavMeshAgent = null;
        public Transform target;
        public float walkSpeed;
        public float runSpeed;
        public float meleeDamage;
        public float meleeRate;
        private float meleeTimer;
        public bool playerInAttackRange = false;

        public PlayerCharacter player;

        public bool trapSet;

        private bool hitDetected;

        private void Awake()
        {
            GetReferences();
            trapSet = false;
        }

        private void Update()
        {
            if (trapSet == true || hitDetected)
            {
                MoveToTarget();
            }
        }

        private void MoveToTarget()
        {
            enemyNavMeshAgent.SetDestination(target.position);
            Walk();
            Run();
            RotateToTarget();
            float distanceToTarget = Vector3.Distance(target.position, transform.position);

            if (playerInAttackRange)
            {
                animator.SetBool("Run", false);
                animator.SetBool("Walk", false);
        
                animator.SetTrigger("Attack");
                Vector3 targetPosition = new Vector3(target.position.x, this.transform.position.y, target.position.z);
                enemyNavMeshAgent.transform.LookAt(targetPosition);

                meleeTimer += Time.fixedDeltaTime;
                if (meleeTimer > meleeRate)
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
                Vector3 targetPosition = new Vector3(target.position.x, this.transform.position.y, target.position.z);
                enemyNavMeshAgent.transform.LookAt(targetPosition);
                enemyNavMeshAgent.SetDestination(target.position);
            }
        }
        private void GetReferences()
        {
            enemyNavMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        private void RotateToTarget()
        {
            Vector3 targetPosition = new Vector3(target.position.x, this.transform.position.y, target.position.z);
            enemyNavMeshAgent.transform.LookAt(targetPosition);
        }

        public void Walk()
        {
            enemyNavMeshAgent.speed = walkSpeed;
            animator.SetBool("Walk", true);
        }

        public void Run()
        {
            enemyNavMeshAgent.speed = runSpeed * gameObject.GetComponent<Enemy>().enemyBaseSpeed;
            animator.SetBool("Run", true);
        }

        public void Die()
        {
            animator.SetTrigger("Die");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                playerInAttackRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                playerInAttackRange = false;
            }
        }

        private void OnEnable()
        {
            GetComponent<Enemy>().EnemyGotHit += DetectPlayerPosition;
        }

        private void OnDisable()
        {
            GetComponent<Enemy>().EnemyGotHit -= DetectPlayerPosition;
        }

        private void DetectPlayerPosition(bool detected)
        {
            hitDetected = detected;
            Debug.Log("Player detected");
        }
    }
}
