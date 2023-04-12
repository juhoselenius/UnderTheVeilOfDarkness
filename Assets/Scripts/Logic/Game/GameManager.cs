using System;
using UnityEngine;

namespace Logic.Game
{
    public class GameManager : IGameManager
    {
        private Data.Game _gameState;

        public GameManager() : this(new Data.Game()) { }

        public GameManager(Data.Game initialGameState)
        {
            _gameState = initialGameState;
        }

        public event Action<bool> IntroLevelCleared;
        public event Action<bool> Level2Cleared;

        public bool GetIntroLevelCleared()
        {
            return _gameState.introLevelCleared;
        }

        public bool GetLevel2Cleared()
        {
            return _gameState.level2Cleared;
        }

        public void SetIntroLevelCleared()
        {
            Debug.Log($"Finished Intro Level!");
            _gameState.introLevelCleared = true;
            IntroLevelCleared?.Invoke(_gameState.introLevelCleared);
        }

        public void SetLevel2Cleared()
        {
            Debug.Log($"Finished Level 2!");
            _gameState.level2Cleared = true;
            Level2Cleared?.Invoke(_gameState.level2Cleared);
        }
    }
}
