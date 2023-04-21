using Logic.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Visualization
{
    public class PresetUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentPresetText;

        private IPlayerManager _playerManager;
        public Image filler;
        private PlayerCharacter pCharacter;

        void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            currentPresetText = GetComponent<TextMeshProUGUI>();
            pCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        }

        private void Update()
        {
            filler.fillAmount = pCharacter.currentPresetCooldown / pCharacter.maxPresetCooldown;
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
