using System;

namespace Data
{
    [Serializable]
    public class Game
    {
        public bool introLevelCleared;
        public bool level2Cleared;

        public Game()
        {
            introLevelCleared = false;
            level2Cleared = false;
        }
    }
}
