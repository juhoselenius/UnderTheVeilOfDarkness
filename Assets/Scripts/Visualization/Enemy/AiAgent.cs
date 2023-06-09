using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    public NavMeshAgent navMeshAgent;
    public Path path;

    public GameObject enemyProjectilePrefab;
    public float fireRateTimer;
    public float fireRate; // The pause between shots in seconds
    public Transform projectileSpawn;
    public float arcRange;
    public LayerMask ignoreLayerProjectile;

    public Transform eye;
    [HideInInspector] public Transform chaseTarget;
    public Vector3 lastKnownPlayerPosition;

    public Vector3 alertPlayerPosition;
    public bool backFromTracking;

    public AiAgentConfig config;

    public AiStateId currentState;

    private void Awake()
    {
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new PatrolState());
        stateMachine.RegisterState(new ChaseState());
        stateMachine.RegisterState(new AlertState());
        stateMachine.RegisterState(new TrackingState());
    }

    void Start()
    {
        
        stateMachine.ChangeState(initialState);
        backFromTracking = false;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.PerformState();

        if (stateMachine.currentState == AiStateId.ChaseState)
        {
            fireRateTimer += Time.deltaTime;
        }
        else
        {
            fireRateTimer = 0;
        }

        currentState = stateMachine.currentState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && (stateMachine.currentState == AiStateId.PatrolState || stateMachine.currentState == AiStateId.TrackingState))
        {
            // The player enters the sensing area of the enemy, so the enemy enters the Alert State
            backFromTracking = false;
            alertPlayerPosition = other.transform.position;
            stateMachine.ChangeState(AiStateId.AlertState);
            Debug.Log("Meni ontriggeriin AiAgentissa");
           
        }
         
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && (stateMachine.currentState == AiStateId.PatrolState || stateMachine.currentState == AiStateId.TrackingState))
        {
            // The player enters the sensing area of the enemy, so the enemy enters the Alert State
            backFromTracking = false;
            alertPlayerPosition = other.transform.position;
            stateMachine.ChangeState(AiStateId.AlertState);
        }
    }
}
