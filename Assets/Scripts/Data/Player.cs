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

        public Preset[] presets;
        public int currentPresetIndex;

        // Attribute Preset Manager base values and factor values

        public float sightFactor;

        public float hearingFactor;

        public float baseWalkSpeed;
        public float baseSprintSpeed;
        public float walkSpeedFactor;
        public float sprintSpeedFactor;

        public float baseProjectileDamage;
        public float baseProjectileSpeed;
        public float baseFireRate;
        public float baseParticleStartSize;
        public float baseProjectileColliderRadius;
        public float projectileDamageFactor;
        public float projectileSpeedFactor;
        public float fireRateFactor;
        public float particleStartSizeFactor;
        public float projectileColliderRadiusFactor;

        public float defenseFactor;

        

        public Player()
        {
            health = 100;
            maxHealth = 100;
            lives = 10;
            maxLives = 10;

            presets = new Preset[3];
            presets[0] = new Preset();
            presets[1] = new Preset();
            presets[2] = new Preset();

            currentPresetIndex = 0;

            sightFactor = 0.5f;

            hearingFactor = 0.5f;

            baseWalkSpeed = 1f;
            baseSprintSpeed = 2f;
            walkSpeedFactor = 0.1f;
            sprintSpeedFactor = 0.2f;

            baseProjectileDamage = 5f;
            baseProjectileSpeed = 40f;
            baseFireRate = 6f;
            baseParticleStartSize = 0.1f;
            baseProjectileColliderRadius = 0.01f;
            projectileDamageFactor = 0.1f;
            projectileSpeedFactor = 0.35f;
            fireRateFactor = 0.05f;
            particleStartSizeFactor = 0.049f;
            projectileColliderRadiusFactor = 0.0079f;

            defenseFactor = 0.5f;

            
        }
    }
}
