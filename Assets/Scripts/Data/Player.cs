using System;

namespace Data
{
    [Serializable]
    public class Player
    {
        public float health;
        public float maxHealth;
        public int lives;
        public int maxLives;

        public float walkSpeed;
        public float sprintSpeed;

        public Preset[] presets;
        public int currentPresetIndex;

        public Player()
        {
            health = 100;
            maxHealth = 100;
            lives = 10;
            maxLives = 10;

            walkSpeed = 1;
            sprintSpeed = 1;

            presets = new Preset[3];
            presets[0] = new Preset();
            presets[1] = new Preset();
            presets[2] = new Preset();

            currentPresetIndex = 0;
        }
    }
}
