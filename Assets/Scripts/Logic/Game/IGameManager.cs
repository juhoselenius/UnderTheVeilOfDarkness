using System;

namespace Logic.Game
{
    public interface IGameManager
    {
        event Action<bool> IntroLevelCleared;
        bool GetIntroLevelCleared();
        void SetIntroLevelCleared();

        event Action<bool> Level2Cleared;
        bool GetLevel2Cleared();
        void SetLevel2Cleared();
    }
}
