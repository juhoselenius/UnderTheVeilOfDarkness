using UnityEngine;
using UnityEngine.AI;
using Visualization;

public class SmallSkeletonController: MonoBehaviour
{
    private NavMeshAgent enemyNavMeshAgent;
    private Animator animator;
    private SphereCollider sphereCollider;
    private int nextWaypoint;
    private float waitTimer;

    private float meleeTimer;

    public Transform playerTransform; // Main Camera of the player has to be dragged here
    public PlayerCharacter player;
    
    public bool playerInDetectionRange = false;
    public bool playerInAttackRange = false;
    public float walkSpeed;
    public float runSpeed;
    public float sightRange;
    public float patrolTurnWaitTime;
    public float meleeDamage;
    public float meleeRate;

    private void Awake()
    {
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = sightRange;
    }

    void FixedUpdate()
    {

            Run();
                enemyNavMeshAgent.transform.LookAt(playerTransform);
                enemyNavMeshAgent.SetDestination(playerTransform.position + new Vector3(0, 0, 2f));
            if (playerInAttackRange)
            {
                animator.SetTrigger("Attack");
                enemyNavMeshAgent.transform.LookAt(playerTransform);

                meleeTimer += Time.fixedDeltaTime;
                if (meleeTimer > meleeRate)
                {
                    meleeTimer = 0;
                    player.TakeDamage(meleeDamage);
                }
            }
                
            

        
       
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
