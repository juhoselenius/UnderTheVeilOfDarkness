using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=T5y7L1siFSY

public class WeaponManager : MonoBehaviour
{
    public Camera playerCam;
    public Transform projectileSpawn;
    public GameObject projectile;
    public float projectileSpeed;
    public float fireRate = 4f;
    public float arcRange = 0f;

    private Vector3 destination;
    private float timeToFire;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(100);
        }

        InstantiateProjectile();
    }

    void InstantiateProjectile()
    {
        GameObject firedProjectile = Instantiate(projectile, projectileSpawn.position, Quaternion.identity);
        firedProjectile.GetComponent<Rigidbody>().velocity = (destination - projectileSpawn.position).normalized * projectileSpeed;

        iTween.PunchPosition(firedProjectile, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 2f));
    }
}
