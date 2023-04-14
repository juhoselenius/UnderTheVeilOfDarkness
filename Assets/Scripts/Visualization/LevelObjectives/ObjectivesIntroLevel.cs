using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visualization
{
    public class ObjectivesIntroLevel : MonoBehaviour
    {
        public LevelEndDoor endDoor;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                endDoor.levelObjectivesDone = true;
            }
        }
    }

}
