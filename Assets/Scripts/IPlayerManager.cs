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

        event Action<Preset> preset1Changed;
        Preset getPreset1();
        void updatePreset1(Preset change);

        event Action<Preset> preset2Changed;
        Preset getPreset2();
        void updatePreset2(Preset change);

        event Action<Preset> preset3Changed;
        Preset getPreset3();
        void updatePreset3(Preset change);

        /*
        event Action<float> sightChanged;
        float getSight();
        void updateSight(float change);

        event Action<float> hearingChanged;
        float getHearing();
        void updateHearing(float change);

        event Action<float> movementChanged;
        float getMovement();
        void updateMovement(float change);

        event Action<float> attackChanged;
        float getAttack();
        void updateAttack(float change);

        event Action<float> defenseChanged;
        float getDefense();
        void updateDefense(float change);   
        */
    }
}