using System;

namespace Logic.Player
{
    public interface IPlayerManager
    {
        event Action<float> healthChanged;
        float getHealth();
        void updateHealth(float change);

        event Action<int> livesChanged;
        int getLives();
        void updateLives(int change);

        //event Action<Preset> presetChanged;
        Preset getPreset(int presetIndex);
        void updatePreset(Preset change);

        event Action<float> sightChanged;
        float getSight(int presetIndex);
        void updateSight(float newValue, int presetIndex);

        event Action<float> hearingChanged;
        float getHearing(int presetIndex);
        void updateHearing(float newValue, int presetIndex);

        event Action<float> movementChanged;
        float getMovement(int presetIndex);
        void updateMovement(float newValue, int presetIndex);

        event Action<float> attackChanged;
        float getAttack(int presetIndex);
        void updateAttack(float newValue, int presetIndex);

        event Action<float> defenseChanged;
        float getDefense(int presetIndex);
        void updateDefense(float newValue, int presetIndex);
    }
}