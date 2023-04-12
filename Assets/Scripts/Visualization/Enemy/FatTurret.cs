using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatTurret : MonoBehaviour
{
    public Transform shootPoint;
    public float turretSightRange;
    public GameObject ammoSpawn;
    public GameObject turretAmmo;
    public GameObject turretRotator;
    public float foce;
    public Vector3 gravity;
    private int angleMultiplier;
    
    // Start is called before the first frame update
    void Start()
    {
        gravity = Physics.gravity;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    void Look()
    {
        // Visualize the ray in the Scene view (for debugging)
        Debug.DrawRay(shootPoint.position, shootPoint.forward * turretSightRange, Color.green);

        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, turretSightRange) && hit.collider.CompareTag("Player"))
        {
            // If the ray hits the player, the enemy gets into the chase state and assing the chase target as the player
            
        }
    }

    IEnumerator ShootBalls()
    {


        Debug.Log("Ammutaan ammus");
        Vector3[] direction = HitTargetBySpeed(ammoSpawn.transform.position, targetLocation.transform.position, gravity, force);

        if (gameObject.transform.position.z < targetLocation.transform.position.z)
        {
            angleMultiplier = -1;
        }
        else
        {
            angleMultiplier = 1;
        }

        // Ennen ensimm‰ist‰ ammusta lasketaan kulma, mihin piipun pit‰isi k‰‰nty‰. Ilmoitetaan se RotateGuniin ja odotetaan coroutinessa kunnes piippu on k‰‰ntynyt
        // ja vasta sitten ammutaan.
        //Debug.Log("Piipun tulisi k‰‰nty‰ kulmaan: " + Mathf.Atan(direction[0].y / direction[0].z) * Mathf.Rad2Deg * angleMultiplier);
        //barrelRotator.GetComponent<RotateBarrel>().xAngle = Mathf.Atan(direction[0].y / direction[0].z) * Mathf.Rad2Deg * angleMultiplier;

        //Odotetaan, ett k‰‰ntyminen p‰‰ttyy. K‰ytet‰‰n Coroutinen ominaisuuksia
        //yield return new WaitUntil(() => barrelRotator.GetComponent<RotateBarrel>().rotating == false);

        

            // Eka ammus
            /*
            GameObject projectile = Instantiate(ammo, ammoSpawn.transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().AddRelativeForce(direction[0], ForceMode.Impulse);

            yield return new WaitForSeconds(2);
            */
            Debug.Log("Piipun tulisi k‰‰nty‰ kulmaan: " + Mathf.Atan(direction[1].y / direction[1].z) * Mathf.Rad2Deg * angleMultiplier);
            turretRotator.GetComponent<TurretRotator>().xAngle = Mathf.Atan(direction[1].y / direction[1].z) * Mathf.Rad2Deg * angleMultiplier;

            //Odotetaan, ett k‰‰ntyminen p‰‰ttyy. K‰ytet‰‰n Coroutinen ominaisuuksia
            yield return new WaitUntil(() => turretRotator.GetComponent<TurretRotator>().rotating == false);

            // Toka ammus
            GameObject projectile2 = Instantiate(turretAmmo, ammoSpawn.transform.position, Quaternion.identity);
            projectile2.GetComponent<Rigidbody>().AddRelativeForce(direction[1], ForceMode.Impulse);
        
        yield return new WaitForSeconds(10);
        
    }

    public Vector3[] HitTargetBySpeed(Vector3 startPosition, Vector3 targetPosition, Vector3 gravityBase, float launchSpeed)
    {
        Vector3 AtoB = targetPosition - startPosition;
        Vector3 horizontal = GetHorizontalVector(AtoB, gravityBase, startPosition);
        float horizontalDistance = horizontal.magnitude;

        Vector3 vertical = GetVerticalVector(AtoB, gravityBase, startPosition);
        float verticalDistance = vertical.magnitude * Mathf.Sign(Vector3.Dot(vertical, -gravityBase));

        float x2 = horizontalDistance * horizontalDistance;
        float v2 = launchSpeed * launchSpeed;
        float v4 = launchSpeed * launchSpeed * launchSpeed * launchSpeed;
        float gravMag = gravityBase.magnitude;

        // LAUNCHTEST
        // Jos launchtest on negatiivinen, niin ei ole mit‰‰n mahdollisuutta osua kohteeseen annetulla forcella.
        // Jos launchtest on positiivinen, osuminen on mahdollista ja voidaan laskea kaksi mahdollista kulmaa.

        float launchTest = v4 - (gravMag * ((gravMag * x2) + (2 * verticalDistance)));
        Debug.Log("LAUNCHTEST: " + launchTest);

        Vector3[] launch = new Vector3[2];

        if (launchTest < 0)
        {
            Debug.Log("Ei voida osua maaliin. Ammutaan kuitenkin 45 asteen kulmassa kaksi palloa.");
            launch[0] = (horizontal.normalized * launchSpeed * Mathf.Cos(45.0f * Mathf.Deg2Rad)) - gravityBase.normalized * launchSpeed * Mathf.Sin(45.0f * Mathf.Deg2Rad);
            launch[1] = (horizontal.normalized * launchSpeed * Mathf.Cos(45.0f * Mathf.Deg2Rad)) - gravityBase.normalized * launchSpeed * Mathf.Sin(45.0f * Mathf.Deg2Rad);


        }
        else
        {
            Debug.Log("Voidaan osua, lasketaan kulmat");
            float[] tanAngle = new float[2];
            tanAngle[0] = (v2 - Mathf.Sqrt(v4 - gravMag * ((gravMag * x2) + (2 * verticalDistance * v2)))) / (gravMag * horizontalDistance);
            tanAngle[1] = (v2 + Mathf.Sqrt(v4 - gravMag * ((gravMag * x2) + (2 * verticalDistance * v2)))) / (gravMag * horizontalDistance);

            float[] finalAngle = new float[2];
            finalAngle[0] = Mathf.Atan(tanAngle[0]);
            finalAngle[1] = Mathf.Atan(tanAngle[1]);
            Debug.Log("Kulmat joihin ammutaan ovat: " + finalAngle[0] * Mathf.Rad2Deg + " ja " + finalAngle[1] * Mathf.Rad2Deg);

            launch[0] = (horizontal.normalized * launchSpeed * Mathf.Cos(finalAngle[0])) - gravityBase.normalized * launchSpeed * Mathf.Sin(finalAngle[0]);
            launch[1] = (horizontal.normalized * launchSpeed * Mathf.Cos(finalAngle[1])) - gravityBase.normalized * launchSpeed * Mathf.Sin(finalAngle[1]);
        }

        return launch;
    }

    public Vector3 GetHorizontalVector(Vector3 AtoB, Vector3 gravityBase, Vector3 startPosition)
    {
        Vector3 output;
        Vector3 perpendicular = Vector3.Cross(AtoB, gravityBase);
        perpendicular = Vector3.Cross(gravityBase, perpendicular);
        output = Vector3.Project(AtoB, perpendicular);
        Debug.DrawRay(startPosition, output, Color.green, 10f);
        return output;
    }

    public Vector3 GetVerticalVector(Vector3 AtoB, Vector3 gravityBase, Vector3 startPosition)
    {
        Vector3 output;
        output = Vector3.Project(AtoB, gravityBase);
        Debug.DrawRay(startPosition, output, Color.cyan, 10f);


        return output;
    }
}
