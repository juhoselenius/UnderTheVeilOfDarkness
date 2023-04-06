public enum AiStateId
{
    PatrolState,
    ChaseState,
    TrackingState,
    AlertState
}

public interface IAiState
{
    AiStateId GetId();
    void Enter(AiAgent agent);
    void Perform(AiAgent agent);
    void Exit(AiAgent agent);
}
