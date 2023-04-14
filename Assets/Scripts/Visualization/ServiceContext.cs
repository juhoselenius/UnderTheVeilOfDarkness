using Logic.Player;
using Logic.Game;
using UnityEngine;

namespace Visualization
{
    public class ServiceContext : MonoBehaviour
    {
        [SerializeField] private Data.Player initialPlayerState;
        [SerializeField] private Data.Game initialGameState;

        private void Awake()
        {
            RegisterServices();
        }

        private void OnDestroy()
        {
            ServiceLocator.Reset();
        }

        private void RegisterServices()
        {
            ServiceLocator.RegisterService<IPlayerManager, PlayerManager>(new PlayerManager(initialPlayerState));
            ServiceLocator.RegisterService<IGameManager, GameManager>(new GameManager(initialGameState));
        }
    }
}