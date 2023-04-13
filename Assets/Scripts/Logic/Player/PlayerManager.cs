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

        public event Action<int> LivesChanged;
        public event Action<float> HealthChanged;
        public event Action<int> PresetChanged;
        public event Action<float> SightChanged;
        public event Action<float> HearingChanged;
        public event Action<float> MovementChanged;
        public event Action<float> AttackChanged;
        public event Action<float> DefenseChanged;

        public float GetHealth()
        {
            return _state.health;
        }
        
        public void UpdateHealth(float change)
        {
            Debug.Log($"Damage in PlayerManager: {change}!");
            _state.health = Mathf.Clamp(_state.health + change, 0, _state.maxHealth);
            HealthChanged?.Invoke(_state.health);
        }

        public float GetMaxHealth()
        {
            return _state.maxHealth;
        }

        public int GetLives()
        {
            return _state.lives;
        }

        public void UpdateLives(int change)
        {
            Debug.Log($"Lives in PlayerManager: {change}!");
            _state.lives = Mathf.Clamp(_state.lives + change, 0, _state.maxLives);
            LivesChanged?.Invoke(_state.lives);
        }

        public int GetCurrentPreset()
        {
            return _state.currentPresetIndex;
        }

        public Preset GetPreset(int presetIndex)
        {
            return _state.presets[presetIndex];
        }

        public void ChangePreset(int presetIndex)
        {
            Debug.Log($"Current Preset Index in PlayerManager: {presetIndex}!");
            _state.currentPresetIndex = presetIndex;
            PresetChanged?.Invoke(_state.currentPresetIndex);
            SightChanged?.Invoke(_state.presets[_state.currentPresetIndex].sightAttribute);
            HearingChanged?.Invoke(_state.presets[_state.currentPresetIndex].hearingAttribute);
            MovementChanged?.Invoke(_state.presets[_state.currentPresetIndex].movementAttribute);
            AttackChanged?.Invoke(_state.presets[_state.currentPresetIndex].attackAttribute);
            DefenseChanged?.Invoke(_state.presets[_state.currentPresetIndex].defenseAttribute);
        }

        public float GetSight()
        {
            return _state.presets[_state.currentPresetIndex].sightAttribute;
        }

        public void UpdateSight(float newSightValue)
        {
            Debug.Log($"Sight in PlayerManager: {newSightValue} in Preset{_state.currentPresetIndex}!");
            float otherAttributeTotal = _state.presets[_state.currentPresetIndex].hearingAttribute + 
                _state.presets[_state.currentPresetIndex].movementAttribute +
                _state.presets[_state.currentPresetIndex].attackAttribute + _state.presets[_state.currentPresetIndex].defenseAttribute;

            _state.presets[_state.currentPresetIndex].sightAttribute = Mathf.Clamp(newSightValue, 0f, 100 - otherAttributeTotal);
            SightChanged?.Invoke(_state.presets[_state.currentPresetIndex].sightAttribute);
        }

        public float GetHearing()
        {
            return _state.presets[_state.currentPresetIndex].hearingAttribute;
        }

        public void UpdateHearing(float newHearingValue)
        {
            Debug.Log($"Hearing in PlayerManager: {newHearingValue} in Preset{_state.currentPresetIndex}!");
            float otherAttributeTotal = _state.presets[_state.currentPresetIndex].sightAttribute + 
                _state.presets[_state.currentPresetIndex].movementAttribute +
                _state.presets[_state.currentPresetIndex].attackAttribute + _state.presets[_state.currentPresetIndex].defenseAttribute;

            _state.presets[_state.currentPresetIndex].hearingAttribute = Mathf.Clamp(newHearingValue, 0f, 100 - otherAttributeTotal);
            HearingChanged?.Invoke(_state.presets[_state.currentPresetIndex].hearingAttribute);
        }

        public float GetMovement()
        {
            return _state.presets[_state.currentPresetIndex].movementAttribute;
        }

        public void UpdateMovement(float newMovementValue)
        {
            Debug.Log($"Movement in PlayerManager: {newMovementValue} in Preset{_state.currentPresetIndex}!");
            float otherAttributeTotal = _state.presets[_state.currentPresetIndex].sightAttribute + 
                _state.presets[_state.currentPresetIndex].hearingAttribute +
                _state.presets[_state.currentPresetIndex].attackAttribute + _state.presets[_state.currentPresetIndex].defenseAttribute;

            _state.presets[_state.currentPresetIndex].movementAttribute = Mathf.Clamp(newMovementValue, 0f, 100 - otherAttributeTotal);
            MovementChanged?.Invoke(_state.presets[_state.currentPresetIndex].movementAttribute);
        }

        public float GetAttack()
        {
            return _state.presets[_state.currentPresetIndex].attackAttribute;
        }

        public void UpdateAttack(float newAttackValue)
        {
            Debug.Log($"Attack in PlayerManager: {newAttackValue} in Preset{_state.currentPresetIndex}!");
            float otherAttributeTotal = _state.presets[_state.currentPresetIndex].sightAttribute + 
                _state.presets[_state.currentPresetIndex].hearingAttribute +
                _state.presets[_state.currentPresetIndex].movementAttribute + _state.presets[_state.currentPresetIndex].defenseAttribute;

            _state.presets[_state.currentPresetIndex].attackAttribute = Mathf.Clamp(newAttackValue, 0f, 100 - otherAttributeTotal);
            AttackChanged?.Invoke(_state.presets[_state.currentPresetIndex].attackAttribute);
        }

        public float GetDefense()
        {
            return _state.presets[_state.currentPresetIndex].defenseAttribute;
        }

        public void UpdateDefense(float newDefenseValue)
        {
            Debug.Log($"Defense in PlayerManager: {newDefenseValue} in Preset{_state.currentPresetIndex}!");
            float otherAttributeTotal = _state.presets[_state.currentPresetIndex].sightAttribute + _state.presets[_state.currentPresetIndex].hearingAttribute +
                _state.presets[_state.currentPresetIndex].movementAttribute + _state.presets[_state.currentPresetIndex].attackAttribute;

            _state.presets[_state.currentPresetIndex].defenseAttribute = Mathf.Clamp(newDefenseValue, 0f, 100 - otherAttributeTotal);
            DefenseChanged?.Invoke(_state.presets[_state.currentPresetIndex].defenseAttribute);
        }

        // Attribute Preset base and factor value getters
        public float GetSightFactor()
        {
            return _state.sightFactor;
        }

        public float GetHearingFactor()
        {
            return _state.hearingFactor;
        }

        public float GetBaseWalkSpeed()
        {
            return _state.baseWalkSpeed;
        }

        public float GetBaseSprintSpeed()
        {
            return _state.baseSprintSpeed;
        }

        public float GetWalkSpeedFactor()
        {
            return _state.walkSpeedFactor;
        }

        public float GetSprintSpeedFactor()
        {
            return _state.sprintSpeedFactor;
        }

        public float GetBaseProjectileDamage()
        {
            return _state.baseProjectileDamage;
        }

        public float GetBaseProjectileSpeed()
        {
            return _state.baseProjectileSpeed;
        }

        public float GetBaseFireRate()
        {
            return _state.baseFireRate;
        }

        public float GetBaseParticleStartSize()
        {
            return _state.baseParticleStartSize;
        }

        public float GetBaseProjectileColliderRadius()
        {
            return _state.baseProjectileColliderRadius;
        }

        public float GetProjectileDamageFactor()
        {
            return _state.projectileDamageFactor;
        }

        public float GetProjectileSpeedFactor()
        {
            return _state.projectileSpeedFactor;
        }

        public float GetFireRateFactor()
        {
            return _state.fireRateFactor;
        }

        public float GetParticleStartSizeFactor()
        {
            return _state.particleStartSizeFactor;
        }

        public float GetProjectileColliderRadiusFactor()
        {
            return _state.projectileColliderRadiusFactor;
        }

        public float GetDefenseFactor()
        {
            return _state.defenseFactor;
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
