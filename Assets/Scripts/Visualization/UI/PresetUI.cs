using Logic.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Visualization
{
    public class PresetUI : MonoBehaviour
    {
        private IPlayerManager _playerManager;

        public TextMeshProUGUI currentPresetText;
        public Image filler;
        public float currentPresetCooldown;
        public float maxPresetCooldown;

        void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            currentPresetCooldown = 0;
        }

        private void Update()
        {
            if (currentPresetCooldown > 0)
            {
                currentPresetCooldown -= Time.deltaTime;
            }
            else
            {
                currentPresetCooldown = 0;
                if (Input.GetKeyDown("1") && _playerManager.GetCurrentPreset() != 0)
                {
                    _playerManager.ChangePreset(0);
                    currentPresetCooldown = maxPresetCooldown;
                }
                else if (Input.GetKeyDown("2") && _playerManager.GetCurrentPreset() != 1)
                {
                    _playerManager.ChangePreset(1);
                    currentPresetCooldown = maxPresetCooldown;
                }
                else if (Input.GetKeyDown("3") && _playerManager.GetCurrentPreset() != 2)
                {
                    _playerManager.ChangePreset(2);
                    currentPresetCooldown = maxPresetCooldown;
                }
            }

            filler.fillAmount = currentPresetCooldown / maxPresetCooldown;
        }

        private void OnEnable()
        {
            _playerManager.PresetChanged += UpdatePresetText;
            UpdatePresetText(_playerManager.GetCurrentPreset());
        }

        private void OnDisable()
        {
            _playerManager.PresetChanged -= UpdatePresetText;
        }

        private void UpdatePresetText(int newValue)
        {
            currentPresetText.text = "Preset " + (newValue + 1);
        }
    }
}
