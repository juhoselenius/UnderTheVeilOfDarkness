using UnityEngine;

public class AlertState : IAiState
{
    private float searchTimer;

    public AiStateId GetId()
    {
        return AiStateId.AlertState;
    }

    public void Enter(AiAgent agent)
    {
        
    }

    public void Perform(AiAgent agent)
    {
        Look(agent);
        Search(agent);
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
            searchTimer = 0;
            agent.stateMachine.ChangeState(AiStateId.ChaseState);
        }
    }

    void Search(AiAgent agent)
    {
        // Stop the enemy in the Alert State
        agent.navMeshAgent.isStopped = true;

        Vector3 direction = (agent.alertPlayerPosition - agent.transform.position).normalized;
        Debug.Log(direction);
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        agent.transform.rotation = Quaternion.RotateTowards(agent.transform.rotation, targetRotation, agent.config.searchTurnSpeed * Time.deltaTime);
        searchTimer += Time.deltaTime;
        

        if (searchTimer >= agent.config.searchDuration)
        {
            // The enemy gets tired of searching and goes back to the Patrol State 
            searchTimer = 0;
            agent.stateMachine.ChangeState(AiStateId.PatrolState);
        }
    }
}
