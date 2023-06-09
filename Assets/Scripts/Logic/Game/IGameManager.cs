using System;

namespace Logic.Game
{
    public interface IGameManager
    {
        event Action<bool> GamePaused;
        bool GetGamePaused();
        void SetGamePaused();
        
        // Intro Level
        bool GetMoveLeft();
        bool GetMoveRight();
        bool GetMoveForward();
        bool GetMoveBackward();
        bool GetJump();
        bool GetShoot();
        bool GetChangePreset();
        bool GetIntroLevelObjectivesCleared();
        void UpdateMoveLeft();
        void UpdateMoveRight();
        void UpdateMoveForward();
        void UpdateMoveBackward();
        void UpdateJump();
        void UpdateShoot();
        void UpdateChangePreset();
        void UpdateIntroLevelObjectivesCleared();
        event Action<bool> IntroLevelObjectivesCleared;

        event Action<bool> IntroLevelCleared;
        bool GetIntroLevelCleared();
        void SetIntroLevelCleared();

        // Level 2 Objectives
        event Action<int> Level2EnemiesLeftChanged;
        int GetLevel2EnemiesLeft();
        void ResetLevel2EnemiesLeft();
        void DecreaseLevel2EnemiesLeft();
        
        event Action<int> Level2ObjectivesLeftChanged;
        int GetLevel2ObjectivesLeft();
        void ResetLevel2ObjectivesLeft();
        void DecreaseLevel2ObjectivesLeft();
        
        event Action<bool> Level2Cleared;
        bool GetLevel2Cleared();
        void SetLevel2Cleared();

        int GetLevel2CurrentTime();
        void SetLevel2CurrentTime(int currentLevelTime);
        int GetLevel2BestTime();
        void SetLevel2BestTime(int newTime);
    }
}
