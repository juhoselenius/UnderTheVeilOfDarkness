using System;

namespace Logic.Game
{
    public interface IGameManager
    {
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

        // Level 2
        
        event Action<bool> Level2Cleared;
        bool GetLevel2Cleared();
        void SetLevel2Cleared();
    }
}
