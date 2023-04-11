using System;

namespace Data
{
    [Serializable]
    public class Player
    {
        public float health = 100;
        public float maxHealth = 100;
        public int lives;
        public int maxLives = 10;
        //public float sight;
        //public float maxSight = 100;
        //public float movement;
        //public float maxMovement = 100;
        //public float hearing;
        //public float maxHearing = 100;
        //public float attack;
        //public float maxAttack = 100;
        //public float defense;
        //public float maxDefense = 100;
        public Preset preset1 = new Preset();
        public Preset preset2 = new Preset();
        public Preset preset3 = new Preset();
    }
}
