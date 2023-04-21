using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaywalkSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("EnemyWalk");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
