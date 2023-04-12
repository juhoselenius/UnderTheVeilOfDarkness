using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public float timer;
    ChaseState chaseState;
    // Start is called before the first frame update
    void Awake()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >2)
        {
            timer = 0;
            shoot();
        }
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
