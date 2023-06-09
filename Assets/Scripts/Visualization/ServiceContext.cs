using Logic.Player;
using Logic.Game;
using UnityEngine;
using Logic.Options;

namespace Visualization
{
    public class ServiceContext : MonoBehaviour
    {
        public static ServiceContext context;

        [SerializeField] private Data.Player initialPlayerState;
        [SerializeField] private Data.Game initialGameState;
        [SerializeField] private Data.Options initialOptionsState;

        private void Awake()
        {
            if (context == null)
            {
                DontDestroyOnLoad(gameObject);
                context = this;
            }
            else
            {
                Destroy(gameObject);
            }

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
            ServiceLocator.RegisterService<IOptionsManager, OptionsManager>(new OptionsManager(initialOptionsState));

        }
    }
}