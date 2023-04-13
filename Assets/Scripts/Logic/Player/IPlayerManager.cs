using System;

namespace Logic.Player
{
    public interface IPlayerManager
    {
        event Action<float> HealthChanged;
        float GetHealth();
        void UpdateHealth(float change);

        event Action<int> LivesChanged;
        int GetLives();
        void UpdateLives(int change);

        event Action<int> PresetChanged;
        Preset GetPreset(int presetIndex);
        int GetCurrentPreset();
        void ChangePreset(int presetIndex);

        event Action<float> SightChanged;
        float GetSight();
        void UpdateSight(float newValue);

        event Action<float> HearingChanged;
        float GetHearing();
        void UpdateHearing(float newValue);

        event Action<float> MovementChanged;
        float GetMovement();
        void UpdateMovement(float newValue);

        event Action<float> AttackChanged;
        float GetAttack();
        void UpdateAttack(float newValue);

        event Action<float> DefenseChanged;
        float GetDefense();
        void UpdateDefense(float newValue);

        // Attribute Preset base and factor value getters
        float GetSightFactor();

        float GetHearingFactor();

        float GetBaseWalkSpeed();
        float GetBaseSprintSpeed();
        float GetWalkSpeedFactor();
        float GetSprintSpeedFactor();

        float GetBaseProjectileDamage();
        float GetBaseProjectileSpeed();
        float GetBaseFireRate();
        float GetBaseParticleStartSize();
        float GetBaseProjectileColliderRadius();
        float GetProjectileDamageFactor();
        float GetProjectileSpeedFactor();
        float GetFireRateFactor();
        float GetParticleStartSizeFactor();
        float GetProjectileColliderRadiusFactor();

        float GetDefenseFactor();
    }
}