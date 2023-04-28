using System;

namespace Data
{
    [Serializable]
    public class Game
    {
        public bool gamePaused;
        
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
        public int level2EnemiesLeft;
        public int level2ObjectivesLeft;
        public bool level2Cleared;

        public int level2CurrentTime;
        public int level2BestTime;

        public Game()
        {
            gamePaused = false;
            
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
            level2EnemiesLeft = 12;
            level2ObjectivesLeft = 2;
            level2Cleared = false;

            level2CurrentTime = 0;
            level2BestTime = 1000000;
        }
    }
}
