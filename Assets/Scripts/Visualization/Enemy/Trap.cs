using UnityEngine;

namespace Visualization
{
    public class Trap : MonoBehaviour
    {
        private GameObject[] smallSkeletons;

        private void Start()
        {
            smallSkeletons = GameObject.FindGameObjectsWithTag("SmallSkeleton");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                for(int i = 0; i < smallSkeletons.Length; i++)
                {
                    Debug.Log("Activated skeletons");
                    if (smallSkeletons[i] != null)
                    {
                        smallSkeletons[i].GetComponent<SmallSkeletonController>().trapSet = true;
                    }
                }
            }
        }
    }
}