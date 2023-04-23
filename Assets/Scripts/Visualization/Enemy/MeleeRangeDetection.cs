using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRangeDetection : MonoBehaviour
{
    public SkeletonEnemy skelly;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            skelly.playerInAttackRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            skelly.playerInAttackRange = false;
        }
    }
}
