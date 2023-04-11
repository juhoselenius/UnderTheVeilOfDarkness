using System;
using UnityEditor.Presets;

namespace Logic.Player
{
    class FakePlayerManager : IPlayerManager
    {
        public event Action<float> healthChanged;
        public event Action<int> livesChanged;
        public event Action<Preset> preset1Changed;
        public event Action<Preset> preset2Changed;
        public event Action<Preset> preset3Changed;
        /*
        public event Action<float> sightChanged;
        public event Action<float> hearingChanged;
        public event Action<float> movementChanged;
        public event Action<float> attackChanged;
        public event Action<float> defenseChanged;
        */
        public Preset preset1f = new Preset();
        public Preset preset2f = new Preset();
        public Preset preset3f = new Preset();

        

        public float getHealth()
        {
            return 100;
        }

        public void updateHealth(float change)
        {
            healthChanged?.Invoke(change);
        }

        public int getLives()
        {
            return 5;
        }

        public void updateLives(int change)
        {
            livesChanged?.Invoke(change);
        }

        public Preset getPreset1()
        {
            return preset1f;
        }

        public void updatePreset1(Preset change)
        {
            preset1Changed?.Invoke(change);
        }

        public Preset getPreset2()
        {
            return preset2f;
        }

        public void updatePreset2(Preset change)
        {
            preset2Changed?.Invoke(change);
        }

        public Preset getPreset3()
        {
            return preset3f;
        }

        public void updatePreset3(Preset change)
        {
            preset3Changed?.Invoke(change);
        }




        /*
        public float getSight()
        {
            return 20;
        }
    
        public void updateSight(float change)
        {
            sightChanged?.Invoke(change);
        }

        
        public float getHearing()
        {
            return 20;
        }
        public void updateHearing(float change)
        {
            hearingChanged?.Invoke(change);
        }

        
        public float getMovement()
        {
            return 20;
        }
        public void updateMovement(float change)
        {
            movementChanged?.Invoke(change);
        }

        
        public float getAttack()
        {
            return 20;
        }
        public void updateAttack(float change)
        {
            attackChanged?.Invoke(change);
        }

        
        public float getDefense()
        {
            return 20;
        }
        public void updateDefense(float change)
        {
            defenseChanged?.Invoke(change);
        }

        */
    }
}
