using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChaseState : IAiState
{

    public AiStateId GetId()
    {
        return AiStateId.ChaseState;
    }

    public void Enter(AiAgent agent)
    {
        
    }

    public void Perform(AiAgent agent)
    {
        Look(agent);
        Chase(agent);
    }

    public void Exit(AiAgent agent)
    {
        
    }

    void Look(AiAgent agent)
    {
        // Vertor from the eye to the player
        Vector3 enemyToTarget = agent.chaseTarget.position - agent.eye.position;

        // Visualize the ray in the Scene view (for debugging)
        Debug.DrawRay(agent.eye.position, enemyToTarget, Color.red);

        RaycastHit hit; 
        if (Physics.Raycast(agent.eye.position, enemyToTarget, out hit, agent.config.sightRange) && hit.collider.CompareTag("Player"))
        {
            // Enemy has its eyes on the player
            agent.chaseTarget = hit.transform;
        }
        else
        {
            // If the enemy loses sight of the player while chasing, the enemy goes to the tracking state 
            agent.lastKnownPlayerPosition = agent.chaseTarget.position;
            agent.stateMachine.ChangeState(AiStateId.TrackingState);
        }
    }

    void Chase(AiAgent agent)
    {
        agent.navMeshAgent.destination = agent.chaseTarget.position;
        agent.navMeshAgent.isStopped = false;
    }

 
}
