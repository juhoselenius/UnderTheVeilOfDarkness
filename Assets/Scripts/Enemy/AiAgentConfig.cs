using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float sightRange;
    public float patrolTurnWaitTime;
    public float searchTurnSpeed;
    public float searchDuration;
}
