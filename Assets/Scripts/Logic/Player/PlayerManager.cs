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

        public float GetHealth()
        {
            return _state.health;
        }
        
        public void UpdateHealth(float change)
        {
            Debug.Log($"Damage in PlayerManager: {change}!");
            _state.health = Mathf.Clamp(_state.health + change, 0, _state.maxHealth);
            healthChanged?.Invoke(_state.health);
        }

        public int GetLives()
        {
            return _state.lives;
        }


        public void UpdateLives(int change)
        {
            Debug.Log($"Lives in PlayerManager: {change}!");
            _state.lives = Mathf.Clamp(_state.lives + change, 0, _state.maxLives);
            livesChanged?.Invoke(_state.lives);
        }

        public Preset GetPreset(int presetIndex)
        {
            return _state.presets[presetIndex];
        }

        public void UpdatePreset(Preset change)
        {
            //_state.presets[presetIndex] = change;
            //presetChanged?.Invoke(_state.preset1);
        }

        public float GetSight(int presetIndex)
        {
            return _state.presets[presetIndex].sightAttribute;
        }

        public void UpdateSight(float newSightValue, int presetIndex)
        {
            Debug.Log($"Sight in PlayerManager: {newSightValue} in Preset{presetIndex}!");
            float otherAttributeTotal = _state.presets[presetIndex].hearingAttribute + _state.presets[presetIndex].movementAttribute +
                _state.presets[presetIndex].attackAttribute + _state.presets[presetIndex].defenseAttribute;

            _state.presets[presetIndex].sightAttribute = Mathf.Clamp(newSightValue, 0f, 100 - otherAttributeTotal);
            sightChanged?.Invoke(_state.presets[presetIndex].sightAttribute);
        }

        public float GetHearing(int presetIndex)
        {
            return _state.presets[presetIndex].hearingAttribute;
        }

        public void UpdateHearing(float newHearingValue, int presetIndex)
        {
            Debug.Log($"Hearing in PlayerManager: {newHearingValue} in Preset{presetIndex}!");
            float otherAttributeTotal = _state.presets[presetIndex].sightAttribute + _state.presets[presetIndex].movementAttribute +
                _state.presets[presetIndex].attackAttribute + _state.presets[presetIndex].defenseAttribute;

            _state.presets[presetIndex].hearingAttribute = Mathf.Clamp(newHearingValue, 0f, 100 - otherAttributeTotal);
            hearingChanged?.Invoke(_state.presets[presetIndex].hearingAttribute);
        }

        public float GetMovement(int presetIndex)
        {
            return _state.presets[presetIndex].movementAttribute;
        }

        public void UpdateMovement(float newMovementValue, int presetIndex)
        {
            Debug.Log($"Movement in PlayerManager: {newMovementValue} in Preset{presetIndex}!");
            float otherAttributeTotal = _state.presets[presetIndex].sightAttribute + _state.presets[presetIndex].hearingAttribute +
                _state.presets[presetIndex].attackAttribute + _state.presets[presetIndex].defenseAttribute;
            _state.presets[presetIndex].movementAttribute = Mathf.Clamp(newMovementValue, 0f, 100 - otherAttributeTotal);
            movementChanged?.Invoke(_state.presets[presetIndex].movementAttribute);
        }

        public float GetAttack(int presetIndex)
        {
            return _state.presets[presetIndex].attackAttribute;
        }

        public void UpdateAttack(float newAttackValue, int presetIndex)
        {
            Debug.Log($"Attack in PlayerManager: {newAttackValue} in Preset{presetIndex}!");
            float otherAttributeTotal = _state.presets[presetIndex].sightAttribute + _state.presets[presetIndex].hearingAttribute +
                _state.presets[presetIndex].movementAttribute + _state.presets[presetIndex].defenseAttribute;
            _state.presets[presetIndex].attackAttribute = Mathf.Clamp(newAttackValue, 0f, 100 - otherAttributeTotal);
            attackChanged?.Invoke(_state.presets[presetIndex].attackAttribute);
        }

        public float GetDefense(int presetIndex)
        {
            return _state.presets[presetIndex].defenseAttribute;
        }

        public void UpdateDefense(float newDefenseValue, int presetIndex)
        {
            Debug.Log($"Defense in PlayerManager: {newDefenseValue} in Preset{presetIndex}!");
            float otherAttributeTotal = _state.presets[presetIndex].sightAttribute + _state.presets[presetIndex].hearingAttribute +
                _state.presets[presetIndex].movementAttribute + _state.presets[presetIndex].attackAttribute;
            _state.presets[presetIndex].defenseAttribute = Mathf.Clamp(newDefenseValue, 0f, 100 - otherAttributeTotal);
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
