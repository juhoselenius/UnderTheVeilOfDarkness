using Logic.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Visualization
{
    public class PresetManager : MonoBehaviour
    {
        public int currentPresetIndex;
        public float totalAttributeValue;

        public GameObject presetMenu;
    
        public Slider sightSlider;
        public Slider hearingSlider;
        public Slider movementSlider;
        public Slider attackSlider;
        public Slider defenseSlider;

        public TextMeshProUGUI totalAttributeValueText;
        public TextMeshProUGUI sightValueText;
        public TextMeshProUGUI hearingValueText;
        public TextMeshProUGUI movementValueText;
        public TextMeshProUGUI attackValueText;
        public TextMeshProUGUI defenseValueText;

        private IPlayerManager _playerManager;
    
        void Start()
        {
            _playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>()._playerManager;

            currentPresetIndex = 0;

            UpdateSliderValues();

            presetMenu.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            // Toggling Preset Menu on and off
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(!presetMenu.activeInHierarchy)
                {
                    Time.timeScale = 0;
                    presetMenu.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    presetMenu.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Time.timeScale = 1;
                }
            }
        
            totalAttributeValue = _playerManager.GetSight(currentPresetIndex) + _playerManager.GetHearing(currentPresetIndex) +
                _playerManager.GetMovement(currentPresetIndex) + _playerManager.GetAttack(currentPresetIndex) +
                _playerManager.GetDefense(currentPresetIndex);

            /* Limiting the slider values so they sum up maximum of 100
            defenseSlider.value = Mathf.Clamp(defenseSlider.value, 0f, 100 - (sightSlider.value + hearingSlider.value + movementSlider.value + attackSlider.value));
            attackSlider.value = Mathf.Clamp(attackSlider.value, 0f, 100 - (sightSlider.value + hearingSlider.value + movementSlider.value + defenseSlider.value));
            movementSlider.value = Mathf.Clamp(movementSlider.value, 0f, 100 - (sightSlider.value + hearingSlider.value + attackSlider.value + defenseSlider.value));
            hearingSlider.value = Mathf.Clamp(hearingSlider.value, 0f, 100 - (sightSlider.value + movementSlider.value + attackSlider.value + defenseSlider.value));
            sightSlider.value = Mathf.Clamp(sightSlider.value, 0f, 100 - (hearingSlider.value + movementSlider.value + attackSlider.value + defenseSlider.value));
            */
            UpdateTextValues();
        }

        public void OnValueChangedSight(float newValue)
        {
            _playerManager.UpdateSight(newValue, currentPresetIndex);
            sightSlider.value = _playerManager.GetSight(currentPresetIndex);
        }

        public void OnValueChangedHearing(float newValue)
        {
            _playerManager.UpdateHearing(newValue, currentPresetIndex);
            hearingSlider.value = _playerManager.GetHearing(currentPresetIndex);
        }

        public void OnValueChangedMovement(float newValue)
        {
            _playerManager.UpdateMovement(newValue, currentPresetIndex);
            movementSlider.value = _playerManager.GetMovement(currentPresetIndex);
        }

        public void OnValueChangedAttack(float newValue)
        {
            _playerManager.UpdateAttack(newValue, currentPresetIndex);
            attackSlider.value = _playerManager.GetAttack(currentPresetIndex);
        }

        public void OnValueChangedDefense(float newValue)
        {
            _playerManager.UpdateDefense(newValue, currentPresetIndex);
            defenseSlider.value = _playerManager.GetDefense(currentPresetIndex);
        }

        public void DropdownMenuChange(int value)
        {
            currentPresetIndex = value;
            UpdateSliderValues();
            UpdateTextValues();
        }

        private void UpdateSliderValues()
        {
            sightSlider.value = _playerManager.GetSight(currentPresetIndex);
            hearingSlider.value = _playerManager.GetHearing(currentPresetIndex);
            movementSlider.value = _playerManager.GetMovement(currentPresetIndex);
            attackSlider.value = _playerManager.GetAttack(currentPresetIndex);
            defenseSlider.value = _playerManager.GetDefense(currentPresetIndex);
        }

        private void UpdateTextValues()
        {
            // Updating the values to the UI texts
            totalAttributeValueText.text = totalAttributeValue.ToString() + " %";
            sightValueText.text = _playerManager.GetSight(currentPresetIndex).ToString() + " %";
            hearingValueText.text = _playerManager.GetHearing(currentPresetIndex).ToString() + " %";
            movementValueText.text = _playerManager.GetMovement(currentPresetIndex).ToString() + " %";
            attackValueText.text = _playerManager.GetAttack(currentPresetIndex).ToString() + " %";
            defenseValueText.text = _playerManager.GetDefense(currentPresetIndex).ToString() + " %";
        }
    }
}
