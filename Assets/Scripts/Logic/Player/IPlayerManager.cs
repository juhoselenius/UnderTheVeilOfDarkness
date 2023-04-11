using System;

namespace Logic.Player
{
    public interface IPlayerManager
    {
        event Action<float> healthChanged;
        float GetHealth();
        void UpdateHealth(float change);

        event Action<int> livesChanged;
        int GetLives();
        void UpdateLives(int change);

        //event Action<Preset> presetChanged;
        Preset GetPreset(int presetIndex);
        void UpdatePreset(Preset change);

        event Action<float> sightChanged;
        float GetSight(int presetIndex);
        void UpdateSight(float newValue, int presetIndex);

        event Action<float> hearingChanged;
        float GetHearing(int presetIndex);
        void UpdateHearing(float newValue, int presetIndex);

        event Action<float> movementChanged;
        float GetMovement(int presetIndex);
        void UpdateMovement(float newValue, int presetIndex);

        event Action<float> attackChanged;
        float GetAttack(int presetIndex);
        void UpdateAttack(float newValue, int presetIndex);

        event Action<float> defenseChanged;
        float GetDefense(int presetIndex);
        void UpdateDefense(float newValue, int presetIndex);
    }
}