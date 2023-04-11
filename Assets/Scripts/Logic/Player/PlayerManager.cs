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
        //public event Action<Preset> presetChanged;
        public event Action<float> sightChanged;
        public event Action<float> hearingChanged;
        public event Action<float> movementChanged;
        public event Action<float> attackChanged;
        public event Action<float> defenseChanged;

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

        public Preset getPreset(int presetIndex)
        {
            return _state.presets[presetIndex];
        }

        public void updatePreset(Preset change)
        {
            //_state.presets[presetIndex] = change;
            //presetChanged?.Invoke(_state.preset1);
        }

        public float getSight(int presetIndex)
        {
            return _state.presets[presetIndex].sightAttribute;
        }

        public void updateSight(float newSightValue, int presetIndex)
        {
            Debug.Log($"Sight in PlayerManager: {newSightValue} in Preset{presetIndex}!");
            _state.presets[presetIndex].sightAttribute = newSightValue;
            sightChanged?.Invoke(_state.presets[presetIndex].sightAttribute);
        }

        public float getHearing(int presetIndex)
        {
            return _state.presets[presetIndex].hearingAttribute;
        }

        public void updateHearing(float newHearingValue, int presetIndex)
        {
            Debug.Log($"Hearing in PlayerManager: {newHearingValue} in Preset{presetIndex}!");
            _state.presets[presetIndex].hearingAttribute = newHearingValue;
            hearingChanged?.Invoke(_state.presets[presetIndex].hearingAttribute);
        }

        public float getMovement(int presetIndex)
        {
            return _state.presets[presetIndex].movementAttribute;
        }

        public void updateMovement(float newMovementValue, int presetIndex)
        {
            Debug.Log($"Movement in PlayerManager: {newMovementValue} in Preset{presetIndex}!");
            _state.presets[presetIndex].movementAttribute = newMovementValue;
            movementChanged?.Invoke(_state.presets[presetIndex].movementAttribute);
        }

        public float getAttack(int presetIndex)
        {
            return _state.presets[presetIndex].attackAttribute;
        }

        public void updateAttack(float newAttackValue, int presetIndex)
        {
            Debug.Log($"Attack in PlayerManager: {newAttackValue} in Preset{presetIndex}!");
            _state.presets[presetIndex].attackAttribute = newAttackValue;
            attackChanged?.Invoke(_state.presets[presetIndex].attackAttribute);
        }

        public float getDefense(int presetIndex)
        {
            return _state.presets[presetIndex].defenseAttribute;
        }

        public void updateDefense(float newDefenseValue, int presetIndex)
        {
            Debug.Log($"Defense in PlayerManager: {newDefenseValue} in Preset{presetIndex}!");
            _state.presets[presetIndex].defenseAttribute = newDefenseValue;
            defenseChanged?.Invoke(_state.presets[presetIndex].defenseAttribute);
        }

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
