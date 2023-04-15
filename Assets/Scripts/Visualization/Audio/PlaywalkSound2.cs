using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaywalkSound2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("EnemyWalk2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
