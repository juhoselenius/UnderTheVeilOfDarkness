using System.Collections;
using UnityEngine;

public class FatTurret : MonoBehaviour
{
    public Transform shootPoint;
    public float turretSightRange;
    public GameObject turretAmmo;
    public Vector3 gravity;
    public float force;
    private Vector3 currentEuler;
    private float turretAngleMax;
    private float turretAngleMin;
    public float rotationSpeed;
    private bool rotating2Max;
    private RaycastHit hit;  
    private float cooldownTimer;
    public Transform turretRotator;   
    public float resetSpeed;
    public FatTurrets fatturret;
    public ParticleSystem ps1;
    
    public enum FatTurrets
    {
        Turret1,
        Turret2
    }
    
    [SerializeField] private float cooldown = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        
        currentEuler = turretRotator.transform.eulerAngles;
        if (fatturret == FatTurrets.Turret1)
        {
            turretAngleMax = 180;
            turretAngleMin = 105;
        }
        else if(fatturret == FatTurrets.Turret2)
        {
            turretAngleMin = 180;
            turretAngleMax = 265;
        }
        
        
        rotating2Max = true;
        StartCoroutine(Look());
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(shootPoint.position, shootPoint.forward * turretSightRange, Color.green);

        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, turretSightRange) && (hit.collider.CompareTag("Player")))
        {
            Shoot();

        }
        currentEuler = turretRotator.transform.eulerAngles;
        
    }
         

        IEnumerator Look()
    {

        while (true)
        {
            
            if (rotating2Max)
            {
                while (currentEuler.y < turretAngleMax)
                {
                    currentEuler.y = Mathf.MoveTowards(currentEuler.y, turretAngleMax, rotationSpeed * Time.deltaTime);
                    turretRotator.transform.rotation = Quaternion.Euler(currentEuler.x, currentEuler.y, currentEuler.z);
                    yield return null;

                    if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, turretSightRange) && hit.collider.CompareTag("Player"))
                    {
                        StopCoroutine(Look());
                        StartCoroutine(hold());
                        rotating2Max = false;
                        yield break;
                       
                    }
                }
                yield return new WaitForSeconds(5);
                rotating2Max = false;
            }
            else
            {
                while (currentEuler.y > turretAngleMin)
                {
                    currentEuler.y = Mathf.MoveTowards(currentEuler.y, turretAngleMin, rotationSpeed * Time.deltaTime);
                    turretRotator.transform.rotation = Quaternion.Euler(currentEuler.x, currentEuler.y, currentEuler.z);
                    yield return null;

                    if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, turretSightRange) && hit.collider.CompareTag("Player"))
                    {
                        StopCoroutine(Look());
                        StartCoroutine(hold());
                        yield break;                      
                    }
                }
                yield return new WaitForSeconds(5);
                rotating2Max = true;
            }
        }
    }

    IEnumerator chasePlayer()
    {       
        float turnPlus = currentEuler.y +3;
        float turnMinus = currentEuler.y +3;     
        
        if (!(Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, turretSightRange) && hit.collider.CompareTag("Player")))
        {
            for (int i = 0; i < 2; i++)
            {
                turnPlus+=3;
                turnMinus-= 3;
                while (currentEuler.y < turnPlus)
                {
                    currentEuler.y = Mathf.MoveTowards(currentEuler.y, turretAngleMax, (rotationSpeed +3) * Time.deltaTime);
                    turretRotator.transform.rotation = Quaternion.Euler(currentEuler.x, currentEuler.y, currentEuler.z);
                    yield return null;                  
                    
                }
                while (currentEuler.y > turnMinus)
                {
                    currentEuler.y = Mathf.MoveTowards(currentEuler.y, turretAngleMin, (rotationSpeed +3) * Time.deltaTime);
                    turretRotator.transform.rotation = Quaternion.Euler(currentEuler.x, currentEuler.y, currentEuler.z);
                    yield return null;
                   
                }
            }
        }
        else if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, turretSightRange) && hit.collider.CompareTag("Player"))
        {
            StopCoroutine(chasePlayer());
            StartCoroutine(hold());
            yield break;
        }

            if (turretRotator.transform.eulerAngles.y > 180 || turretRotator.transform.eulerAngles.y < 105)
            {
            StopCoroutine(chasePlayer());
            StartCoroutine(resetLookingAngle());
                
            }
            StartCoroutine(Look());
        StopCoroutine(chasePlayer());
        yield break;          
        
        }


    IEnumerator resetLookingAngle()
    {      
        if (turretRotator.transform.eulerAngles.y > 180)
        {
            while (currentEuler.y > turretAngleMin)
            {
                currentEuler.y = Mathf.MoveTowards(currentEuler.y, turretAngleMin, (rotationSpeed +2) * Time.deltaTime);
                turretRotator.transform.rotation = Quaternion.Euler(currentEuler.x, currentEuler.y, currentEuler.z);
                yield return null;
            }          
        }
        else if (turretRotator.transform.eulerAngles.y < 110)
        {
            while (currentEuler.y < turretAngleMax)
            {
                currentEuler.y = Mathf.MoveTowards(currentEuler.y, turretAngleMax, (rotationSpeed +2) * Time.deltaTime);
                turretRotator.transform.rotation = Quaternion.Euler(currentEuler.x, currentEuler.y, currentEuler.z);
                yield return null;
            }
        }
        StopCoroutine(resetLookingAngle());
        StartCoroutine(Look());
    }
    IEnumerator hold()
    {
        currentEuler = turretRotator.transform.eulerAngles;      
        while(Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, turretSightRange) && hit.collider.CompareTag("Player"))
        {
            yield return null;
            turretRotator.transform.rotation = turretRotator.transform.rotation = Quaternion.Euler(currentEuler.x, currentEuler.y, currentEuler.z);
        }
        
            StopCoroutine(hold());
            StartCoroutine(chasePlayer());
      
    }

    private void Shoot()
    {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer > 0) return;
            cooldownTimer = cooldown;
            Instantiate(turretAmmo, shootPoint.position, shootPoint.rotation);
            ps1.Play();
        
        
    }

    
}

    