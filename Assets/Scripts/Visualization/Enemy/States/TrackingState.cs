using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingState : IAiState
{
    public AiStateId GetId()
    {
        return AiStateId.TrackingState;
    }
    public void Enter(AiAgent agent)
    {
        
    }

    public void Perform(AiAgent agent)
    {
        Look(agent);
        Hunt(agent);
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

    void Hunt(AiAgent agent)
    {
        agent.navMeshAgent.destination = agent.lastKnownPlayerPosition;
        agent.navMeshAgent.isStopped = false;

        // Change to Alert State, when arrived at the last known player location
        if (agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance && !agent.navMeshAgent.pathPending)
        {
            agent.stateMachine.ChangeState(AiStateId.AlertState);
        }
    }
}
