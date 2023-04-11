using Logic.Player;
using UnityEngine;

namespace Visualization
{
    public class PlayerCharacter : MonoBehaviour
    {
        public IPlayerManager _playerManager;

        private void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
        }

        private void TakeDamage(int amount)
        {
            _playerManager.UpdateHealth(-amount);
        }

        private void OnTriggerEnter(Collider other)
        {
            /*if()
            {
                TakeDamage();
            if(_playerManager.updateHealth(-amount) <= 0)
            {
                _playerManager.updateLives(-1);
                _playerManger.updateHealth(+100);
                Kuole
            }
        
            }
       */
            }
            
    }
}