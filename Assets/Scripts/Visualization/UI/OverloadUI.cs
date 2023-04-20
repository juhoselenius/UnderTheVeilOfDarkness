using Logic.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Visualization
{
    public class OverloadUI : MonoBehaviour
    {
        public WeaponManager wManager;
        public WeaponManager wManager1;
        public WeaponManager wManager2;
        public WeaponManager[] wManagers;
        public Image filler;
        public Sprite overloadedFiller;
        public Sprite notOverloadedFiller;
        public Image overloadIcon;
        public Sprite overloadedIcon;
        public Sprite notOverloadedIcon;
        private float maxOverload;
        public IPlayerManager _playerManager;

        // Start is called before the first frame update
        private void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
        }
        void Start()
        {
            if(_playerManager.GetAttack() >= 1 && _playerManager.GetAttack() <= 2)
            {
                wManager = wManager1;
            }
            else if(_playerManager.GetAttack() >= 3 && _playerManager.GetAttack() <= 4)
            {
                wManager = wManager2;
            }
            maxOverload = wManager.overLoadMax;
        }

        // Update is called once per frame
        void Update()
        {
            if (_playerManager.GetAttack() >= 1 && _playerManager.GetAttack() <= 2)
            {
                wManager = wManager1;
            }
            else if (_playerManager.GetAttack() >= 3 && _playerManager.GetAttack() <= 4)
            {
                wManager = wManager2;
            }
            if (wManager.overLoaded)
            {
                filler.sprite = overloadedFiller;
                overloadIcon.sprite = overloadedIcon;
            }
            else
            {
                filler.sprite = notOverloadedFiller;
                overloadIcon.sprite = notOverloadedIcon;
            }

            filler.fillAmount = wManager.currentOverLoad / maxOverload;
        }
    }
}
