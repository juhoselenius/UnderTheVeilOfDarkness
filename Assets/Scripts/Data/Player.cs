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
        //public float sight;
        //public float maxSight;
        //public float movement;
        //public float maxMovement;
        //public float hearing;
        //public float maxHearing;
        //public float attack;
        //public float maxAttack;
        //public float defense;
        //public float maxDefense;

        public Preset[] presets;

        public Player()
        {
            health = 100;
            maxHealth = 100;
            lives = 10;
            maxLives = 10;
            //float sight;
            //float maxSight = 100;
            //float movement;
            //float maxMovement = 100;
            //float hearing;
            //float maxHearing = 100;
            //float attack;
            //float maxAttack = 100;
            //float defense;
            //float maxDefense = 100;

            presets = new Preset[3];
            presets[0] = new Preset();
            presets[1] = new Preset();
            presets[2] = new Preset();
        }
    }
}
