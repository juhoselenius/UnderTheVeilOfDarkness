using System;

namespace Data
{
    [Serializable]
    public class Game
    {
        // Intro Level objectives
        public bool moveLeft;
        public bool moveRight;
        public bool moveForward;
        public bool moveBackward;
        public bool jump;
        public bool shoot;
        public bool changePreset;

        public bool introLevelObjectivesCleared;

        public bool introLevelCleared;

        //Level 2 objectives
        public int allEnemiesCleared;
        public int objectivesCollected;
        
        //Level 2 objective booleans here
        public bool level2Cleared;

        public Game()
        {
            // Intro level
            moveLeft = false;
            moveRight = false;
            moveForward = false;
            moveBackward = false;
            jump = false;
            shoot = false;
            changePreset = false;

            introLevelObjectivesCleared = false;

            introLevelCleared = false;

            // Level 2
            allEnemiesCleared = 0;
            objectivesCollected = 0;
            level2Cleared = false;
        }
    }
}
