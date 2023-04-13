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

        public event Action<bool> IntroLevelObjectivesCleared;
        public event Action<bool> IntroLevelCleared;
        public event Action<bool> Level2Cleared;

        public bool GetMoveLeft()
        {
            return _gameState.moveLeft;
        }

        public bool GetMoveRight()
        {
            return _gameState.moveRight;
        }

        public bool GetMoveForward()
        {
            return _gameState.moveForward;
        }

        public bool GetMoveBackward()
        {
            return _gameState.moveBackward;
        }

        public bool GetJump()
        {
            return _gameState.jump;
        }

        public bool GetShoot()
        {
            return _gameState.shoot;
        }

        public bool GetChangePreset()
        {
            return _gameState.changePreset;
        }

        public bool GetIntroLevelObjectivesCleared()
        {
            return _gameState.introLevelObjectivesCleared;
        }

        public void UpdateMoveLeft()
        {
            _gameState.moveLeft = true;
        }

        public void UpdateMoveRight()
        {
            _gameState.moveRight = true;
        }

        public void UpdateMoveForward()
        {
            _gameState.moveForward = true;
        }

        public void UpdateMoveBackward()
        {
            _gameState.moveBackward = true;
        }

        public void UpdateJump()
        {
            _gameState.jump = true;
        }

        public void UpdateShoot()
        {
            _gameState.shoot = true;
        }

        public void UpdateChangePreset()
        {
            _gameState.changePreset = true;
        }

        public void UpdateIntroLevelObjectivesCleared()
        {
            _gameState.introLevelObjectivesCleared = true;
        }

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
