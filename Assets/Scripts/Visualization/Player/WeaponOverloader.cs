using Logic.Player;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace Visualization
{
    public class WeaponOverloader : MonoBehaviour
    {
        public float overLoadMax;
        public float overLoadMin;
        public float currentOverLoad;
        public float overLoadSpeed;
        public bool overLoaded;              
        public WeaponManager wpnMngr;
        public GameObject projectile;
        public float cooldownTimer;     
        [SerializeField] private float cooldown = 3f;
        // Start is called before the first frame update
        void Start()
        {
            overLoaded= false;
            overLoadMin = 0f;
            overLoadMax = 100f;
            currentOverLoad = 0f;
            overLoadSpeed = 1f;          
            
            
        }

        // Update is called once per frame
        void Update()
        {
            if (wpnMngr.shooting)
            {
                Debug.Log("Tuleeko ees ekaan");
                if (gameObject.GetComponent<WeaponManager>().projectile.tag == "PlayerProjectile")
                {
                    overLoadSpeed = 5;
                }
                else if (gameObject.GetComponent<WeaponManager>().projectile.tag == "IceBullet")
                {
                    overLoadSpeed = 8;
                }
                if(currentOverLoad < overLoadMax)
                {
                    currentOverLoad += overLoadSpeed * Time.deltaTime;
                    Debug.Log("Mitä vittua");
                }
                
                Debug.Log("Current overload: " + currentOverLoad);
                if (currentOverLoad >= overLoadMax)
                {
                    overLoaded = true;
                    cooldownTimer -= Time.deltaTime;
                    if (cooldownTimer > 0) return;
                    cooldownTimer = cooldown;
                    overLoaded = false;
                }
            }
            else
            {
                if ((currentOverLoad -3 * Time.deltaTime) >= overLoadMin)
                {
                    currentOverLoad -= 3 * Time.deltaTime;
                    Debug.Log("Current overload: " + currentOverLoad);
                }

                

            }
        }
    }
}
