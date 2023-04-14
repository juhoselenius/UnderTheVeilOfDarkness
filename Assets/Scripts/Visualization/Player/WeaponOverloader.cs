using Logic.Player;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Visualization
{
    public class WeaponOverloader : MonoBehaviour
    {
        public float overLoadMax;
        public float overLoadMin;
        public float currentOverLoad;
        public float overLoadSpeed;
        public IPlayerManager _playerManager;
        public GameObject obj;
        private WeaponManager wpnMngr;
        private Projectile projectile;
        // Start is called before the first frame update
        void Start()
        {
            overLoadMin = 0f;
            overLoadMax = 100f;
            currentOverLoad= 0f;
            overLoadSpeed = 1f;
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            wpnMngr = GetComponent<WeaponManager>();
            projectile = GetComponent<Projectile>();
        }

        // Update is called once per frame
        void Update()
        {
            if(wpnMngr.shooting)
            {
                if (projectile.tag == "PlayerProjectile")
                {
                    
                }
                else if (projectile.tag == "IceBullet")
                {
                    
                }
            }
        }
    }
}
