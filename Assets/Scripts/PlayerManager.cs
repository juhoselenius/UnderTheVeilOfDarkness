using System;
using UnityEngine;

namespace Logic.Player
{
    public class PlayerManager : IPlayerManager
    {
        private Data.Player _state;

        public PlayerManager() : this(new Data.Player()) { }

        public PlayerManager(Data.Player initialState)
        {
            _state = initialState;
        }

        public event Action<int> livesChanged;
        public event Action<float> healthChanged;
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
        

        public float getHealth()
        {
            return _state.health;
        }

        
        public void updateHealth(float change)
        {
            Debug.Log($"Damage in PlayerManager: {change}!");
            _state.health = Mathf.Clamp(_state.health + change, 0, _state.maxHealth);
            healthChanged?.Invoke(_state.health);
        }

        public int getLives()
        {
            return _state.lives;
        }


        public void updateLives(int change)
        {
            Debug.Log($"Lives in PlayerManager: {change}!");
            _state.lives = Mathf.Clamp(_state.lives + change, 0, _state.maxLives);
            livesChanged?.Invoke(_state.lives);
        }

        public Preset getPreset1()
        {
            return _state.preset1;
        }

        public void updatePreset1(Preset change)
        {
            _state.preset1 = change;
            preset1Changed?.Invoke(_state.preset1);
        }

        public Preset getPreset2()
        {
            return _state.preset2;
        }

        public void updatePreset2(Preset change)
        {
            _state.preset2 = change;
            preset2Changed?.Invoke(_state.preset2);
        }

        public Preset getPreset3()
        {
            return _state.preset3;
        }

        public void updatePreset3(Preset change)
        {
            _state.preset3 = change;
            preset3Changed?.Invoke(_state.preset3);
        }

        /*
        public float getSight()
        {
            return _state.sight;
        }

        public void updateSight(float change)
        {
            Debug.Log($"Sight in PlayerManager: {change}!");
            _state.sight = Mathf.Clamp(_state.sight + change, 0, _state.maxSight);
            sightChanged?.Invoke(_state.sight);
        }

        public float getMovement()
        {
            return _state.movement;
        }

        public void updateMovement(float change)
        {
            Debug.Log($"Movement in PlayerManager: {change}!");
            _state.movement = Mathf.Clamp(_state.movement + change, 0, _state.maxMovement);
            movementChanged?.Invoke(_state.movement);
        }

        public float getHearing()
        {
            return _state.hearing;
        }

        public void updateHearing(float change)
        {
            Debug.Log($"Hearing in PlayerManager: {change}!");
            _state.hearing = Mathf.Clamp(_state.hearing + change, 0, _state.maxHearing);        
            hearingChanged?.Invoke(_state.hearing);
        }

        public float getAttack()
        {
            return _state.attack;
        }

        public void updateAttack(float change)
        {
            Debug.Log($"Attack in PlayerManager: {change}!");
            _state.attack = Mathf.Clamp(_state.attack + change, 0, _state.maxAttack);
            attackChanged?.Invoke(_state.attack);
        }
        public float getDefense()
        {
            return _state.defense;
        }

        public void updateDefense(float change)
        {
            Debug.Log($"Defense in PlayerManager: {change}!");
            _state.defense = Mathf.Clamp(_state.defense + change, 0, _state.maxDefense);
            defenseChanged?.Invoke(_state.defense);
        }

        */

        public void SaveState()
        {
            JsonUtility.ToJson(_state);          
        }

        public void LoadState()
        {        
            var stateFromFile = "";
            _state = JsonUtility.FromJson<Data.Player>(stateFromFile);
        }
    }
}
