using Logic.Player;
using TMPro;
using UnityEngine;

namespace Visualization
{
    public class PresetUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentPresetText;

        private IPlayerManager _playerManager;

        void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            currentPresetText = GetComponent<TextMeshProUGUI>();
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
