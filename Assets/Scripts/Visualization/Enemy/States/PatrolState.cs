using UnityEngine;

public class PatrolState : IAiState
{
    private int nextWaypoint;
    private float waitTimer;

    public AiStateId GetId()
    {
        return AiStateId.PatrolState;
    }

    public void Enter(AiAgent agent)
    {
        
    }

    public void Perform(AiAgent agent)
    {
        Look(agent);
        Patrol(agent);
    }

    public void Exit(AiAgent agent)
    {

    }

    void Look(AiAgent agent)
    {
        // Visualize the ray in the Scene view (for debugging)
        Debug.DrawRay(agent.eye.position, agent.eye.forward * agent.config.sightRange, Color.green);

        RaycastHit hit; 
        if (Physics.Raycast(agent.eye.position, agent.eye.forward, out hit, agent.config.sightRange) && hit.collider.CompareTag("Player"))
        {
            // If the ray hits the player, the enemy gets into the chase state and assing the chase target as the player
            agent.chaseTarget = hit.transform;
            agent.stateMachine.ChangeState(AiStateId.ChaseState);
        }
    }

    void Patrol(AiAgent agent)
    {
        agent.navMeshAgent.destination = agent.path.waypoints[nextWaypoint].position;
        agent.navMeshAgent.isStopped = false;

        // Changing waypoint when current waypoint is reached through the NavMeshAgent
        if (agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance && !agent.navMeshAgent.pathPending)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer > agent.config.patrolTurnWaitTime)
            {
                // Looping through waypoints
                nextWaypoint = (nextWaypoint + 1) % agent.path.waypoints.Length;
                waitTimer = 0;
            }      
        }
    }
}
