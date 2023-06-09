using Logic.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Visualization
{
    public class SmallEnemy : MonoBehaviour
    {
        private NavMeshAgent enemyNavMeshAgent;
        private SphereCollider sphereCollider;
        private int nextWaypoint;
        private float waitTimer;

        private float meleeTimer;

        public Transform playerCameraTransform; // Main Camera of the player has to be dragged here
        public PlayerCharacter player;
        public Path patrolPath;
        public bool playerInDetectionRange = false;
        public bool playerInAttackRange = false;
        public float walkSpeed;
        public float runSpeed;
        public float sightRange;
        public float patrolTurnWaitTime;
        public float meleeDamage;
        public float meleeRate;

        public GameObject enemyProjectilePrefab;
        public float fireRateTimer;
        public float fireRate; // The pause between shots in seconds
        public Transform projectileSpawn;
        public float arcRange;
        public AudioSource audioSource;
        public AudioClip clip;
        public GameObject soundTextShoot;
        public GameObject soundTextMove;
        public float soundTextOffset;
        public float soundTime;
        private float soundTimer;
        
        private bool hitDetected;
        private Vector3 detectedPlayerPosition;

        private IPlayerManager _playerManager;

        private void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            enemyNavMeshAgent = GetComponent<NavMeshAgent>();
            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.radius = sightRange;
        }

        void FixedUpdate()
        {
            Look();
            
            if (playerInDetectionRange)
            {
                enemyNavMeshAgent.speed = runSpeed;
                if (playerInAttackRange)
                {
                    //animator.SetTrigger("Attack");
                    enemyNavMeshAgent.transform.LookAt(playerCameraTransform);

                    meleeTimer += Time.fixedDeltaTime;
                    if (meleeTimer > meleeRate)
                    {
                        meleeTimer = 0;

                        // Show damage indicator if damage comes from out of view
                        if (!DamageIndicatorUI.CheckIfObjectInSight(gameObject.transform))
                        {
                            DamageIndicatorUI.CreateIndicator(gameObject.transform);
                        }

                        player.TakeDamage(meleeDamage);
                    }
                }
                else
                {
                    fireRateTimer += Time.deltaTime;

                    if (fireRateTimer > fireRate)
                    {
                        fireRateTimer = 0;
                        Shoot(playerCameraTransform.position);
                    }

                    enemyNavMeshAgent.speed = runSpeed;
                    enemyNavMeshAgent.transform.LookAt(playerCameraTransform);
                    enemyNavMeshAgent.SetDestination(playerCameraTransform.position + new Vector3(0, 0, 1f));
                }
            }
            else if (hitDetected)
            {
                enemyNavMeshAgent.speed = runSpeed;
                Vector3 targetPosition = new Vector3(detectedPlayerPosition.x, this.transform.position.y, detectedPlayerPosition.z);
                enemyNavMeshAgent.transform.LookAt(targetPosition);
                enemyNavMeshAgent.SetDestination(targetPosition);

                // Changing waypoint when last seen player position is reached through the NavMeshAgent
                if (enemyNavMeshAgent.remainingDistance <= enemyNavMeshAgent.stoppingDistance && !enemyNavMeshAgent.pathPending)
                {
                    hitDetected = false;
                    Patrol();
                }
            }
            else
            {
                Patrol();
            }

            if (_playerManager.GetHearing() == 2f)
            {
                soundTimer += Time.deltaTime;
                if(soundTimer > soundTime)
                {
                    soundTimer = 0;
                    soundTextMove.GetComponent<SoundToText>().textSound = "Wob";
                    Instantiate(soundTextMove, gameObject.transform.position, Quaternion.identity);
                }
            }
            else
            {
                soundTimer = 0;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                playerInDetectionRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                playerInDetectionRange = false;
            }
        }

        void Patrol()
        {
            enemyNavMeshAgent.destination = patrolPath.waypoints[nextWaypoint].position;
            enemyNavMeshAgent.isStopped = false;
            enemyNavMeshAgent.speed = walkSpeed;

            // Changing waypoint when current waypoint is reached through the NavMeshAgent
            if (enemyNavMeshAgent.remainingDistance <= enemyNavMeshAgent.stoppingDistance && !enemyNavMeshAgent.pathPending)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer > patrolTurnWaitTime)
                {
                    // Looping through waypoints
                    nextWaypoint = (nextWaypoint + 1) % patrolPath.waypoints.Length;
                    waitTimer = 0;
                    enemyNavMeshAgent.speed = walkSpeed;
                }
            }
        }

        void Shoot(Vector3 targetPosition)
        {
            playSound();
            GameObject firedProjectile = GameObject.Instantiate(enemyProjectilePrefab, projectileSpawn.position, Quaternion.identity);
            firedProjectile.GetComponent<Projectile>().originTransform = gameObject.transform;
            float projectileSpeed = firedProjectile.GetComponent<Projectile>().projectileSpeed;
            firedProjectile.GetComponent<Rigidbody>().velocity = (targetPosition - projectileSpawn.position).normalized * projectileSpeed;

            iTween.PunchPosition(firedProjectile, new Vector3(Random.Range(-arcRange, arcRange), Random.Range(-arcRange, arcRange), 0), Random.Range(0.5f, 2f));
        }

        public void playSound()
        {
            audioSource.PlayOneShot(clip);

            if (_playerManager.GetHearing() == 2f)
            {
                soundTextShoot.GetComponent<SoundToText>().textSound = "Pew";
                Vector3 newTextPosition = new Vector3(transform.position.x, transform.position.y + soundTextOffset, transform.position.z);
                Instantiate(soundTextShoot, newTextPosition, Quaternion.identity);
            }
        }

        private void OnEnable()
        {
            GetComponent<Enemy>().EnemyGotHit += DetectPlayerPosition;
        }

        private void OnDisable()
        {
            GetComponent<Enemy>().EnemyGotHit -= DetectPlayerPosition;
        }

        private void DetectPlayerPosition(bool detected)
        {
            hitDetected = detected;
            detectedPlayerPosition = playerCameraTransform.position;
            Debug.Log("Player detected");
        }

        void Look()
        {
            // Visualize the ray in the Scene view (for debugging)
            Debug.DrawRay(projectileSpawn.position, projectileSpawn.forward * 30f, Color.green);

            RaycastHit hit;
            if (Physics.Raycast(projectileSpawn.position, projectileSpawn.forward, out hit, 30f) && hit.collider.CompareTag("Player"))
            {
                // If the ray hits the player, the enemy gets into the chase state and assing the chase target as the player
                playerInDetectionRange = true;
            }
        }
    }
}