using Logic.Player;
using UnityEngine;

namespace Visualization
{
    public class SightChange : MonoBehaviour
    {
        [SerializeField] private Light playerLight;
        
        private IPlayerManager _playerManager;

        void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            playerLight = GetComponent<Light>();
        }

        private void OnEnable()
        {
            _playerManager.SightChanged += ChangeSight;
            ChangeSight(_playerManager.GetSight());
        }

        private void OnDisable()
        {
            _playerManager.SightChanged -= ChangeSight;
        }

        private void ChangeSight(float newValue)
        {
            playerLight.range = 0.3f * newValue;
        }
    }
}
