using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visualization
{
    public class MeleeRangeSmall : MonoBehaviour
    {
        public SmallEnemy smally;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                smally.playerInAttackRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                smally.playerInAttackRange = false;
            }
        }
    }
}
