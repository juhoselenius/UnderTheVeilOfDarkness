using UnityEngine;
using UnityEngine.AI;
using Visualization;

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

    private bool hit;

    private void Awake()
    {
        GetReferences();
        trapSet = false;
    }

    private void Update()
    {
        if (trapSet == true)
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
        else if (hit)
        {
            enemyNavMeshAgent.speed = runSpeed;
            enemyNavMeshAgent.transform.LookAt(target);
            enemyNavMeshAgent.SetDestination(target.position + new Vector3(0, 0, 3f));
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
        enemyNavMeshAgent.speed = runSpeed;
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

        if (other.CompareTag("Rock") || other.CompareTag("StickyBullet") || other.CompareTag("PlayerProjectile") || other.CompareTag("FireBullet") || other.CompareTag("IceBullet"))
        {
            hit = true;
        }
    }
}
