using Logic.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

namespace Visualization
{
    public class ShieldSpawner : MonoBehaviour
    {
        public IPlayerManager _playerManager;
        private float currentShieldCooldown;
        public float maxShieldCooldown;
        public Sprite[] shieldSprite;
        public Image shieldIcon;
        public Image filler;

        public GameObject[] shieldPrefabs;

        void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            currentShieldCooldown = 0;
            shieldIcon.sprite = shieldSprite[(int)_playerManager.GetDefense()];
        }

        void Update()
        {
            if(currentShieldCooldown > 0)
            {
                currentShieldCooldown -= Time.deltaTime;
            }
            else
            {
                currentShieldCooldown = 0;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if(_playerManager.GetDefense() != 0)
                    {
                        if(_playerManager.GetDefense() == 1)
                        {
                            GameObject shield = Instantiate(shieldPrefabs[(int)_playerManager.GetDefense()], gameObject.transform.position, gameObject.transform.rotation);
                            currentShieldCooldown = maxShieldCooldown;
                        }
                        else
                        {
                            GameObject shield = Instantiate(shieldPrefabs[(int)_playerManager.GetDefense()], gameObject.transform, false);
                            shield.transform.parent = gameObject.transform;
                            shield.transform.localPosition = Vector3.zero;
                            currentShieldCooldown = maxShieldCooldown;
                        }
                    }
                }
            }
            
            filler.fillAmount = currentShieldCooldown / maxShieldCooldown;
        }

        private void OnEnable()
        {
            _playerManager.PresetChanged += UpdateShieldFromPreset;
            _playerManager.DefenseChanged += UpdateShieldFromDefense;
            UpdateShieldFromPreset(_playerManager.GetCurrentPreset());
            UpdateShieldFromDefense(_playerManager.GetDefense());
        }

        private void OnDisable()
        {
            _playerManager.PresetChanged -= UpdateShieldFromPreset;
            _playerManager.DefenseChanged -= UpdateShieldFromDefense;
        }

        void UpdateShieldFromPreset(int currentPreset)
        {
            shieldIcon.sprite = shieldSprite[(int)_playerManager.GetDefense()];
        }

        void UpdateShieldFromDefense(float currentDefense)
        {
            shieldIcon.sprite = shieldSprite[(int)_playerManager.GetDefense()];
        }
    }

}
