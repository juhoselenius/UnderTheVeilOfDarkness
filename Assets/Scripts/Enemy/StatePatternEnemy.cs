using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StatePatternEnemy : MonoBehaviour
{

    public float searchDuration; // The search duration of the Alert state
    public float searchTurnSpeed; // The turn speed of the Alert state
    public float sightRange;
    public Transform[] waypoints; // The waypoints in a table, so we can have multiple 
    public Transform raycastSource; // The transform that generates the raycast
    public Vector3 lastKnownPlayerPosition;

    [HideInInspector] public Transform chaseTarget; // The target to be chased
    [HideInInspector] public IEnemyState currentState; // Here's the current state 
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public TrackingState trackingState;
    [HideInInspector] public NavMeshAgent navMeshAgent; 

    private void Awake()
    {
        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        chaseState = new ChaseState(this);
        trackingState = new TrackingState(this);


        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = patrolState; 
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }
}
