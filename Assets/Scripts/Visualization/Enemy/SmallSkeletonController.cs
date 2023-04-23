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

    public PlayerCharacter player;

    public bool trapSet;


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

        if (distanceToTarget <= enemyNavMeshAgent.stoppingDistance)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
        
            animator.SetTrigger("Attack");
            enemyNavMeshAgent.transform.LookAt(target);

            meleeTimer += Time.fixedDeltaTime;
            if (meleeTimer > meleeRate)
            {
                meleeTimer = 0;
                player.TakeDamage(meleeDamage);
            }
        }
        else
        {
            Run();
            enemyNavMeshAgent.transform.LookAt(target);
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
        enemyNavMeshAgent.transform.LookAt(target);
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
