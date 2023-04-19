using Logic.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

namespace Visualization
{
    public class PresetManager : MonoBehaviour
    {
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
            _playerManager = ServiceLocator.GetService<IPlayerManager>();

            UpdateSliderValues();

            if(SceneManager.GetActiveScene().name != "PresetManagement")
            {
                presetMenu.SetActive(false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Toggling Preset Menu on and off
            if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "PresetManagement")
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
        }

        public void OnValueChangedSight(float newValue)
        {
            _playerManager.UpdateSight(newValue);
            sightSlider.value = _playerManager.GetSight();
            UpdateTextValues();
        }

        public void OnValueChangedHearing(float newValue)
        {
            _playerManager.UpdateHearing(newValue);
            hearingSlider.value = _playerManager.GetHearing();
            UpdateTextValues();
        }

        public void OnValueChangedMovement(float newValue)
        {
            _playerManager.UpdateMovement(newValue);
            movementSlider.value = _playerManager.GetMovement();
            UpdateTextValues();
        }

        public void OnValueChangedAttack(float newValue)
        {
            _playerManager.UpdateAttack(newValue);
            attackSlider.value = _playerManager.GetAttack();
            UpdateTextValues();           
        }

        public void OnValueChangedDefense(float newValue)
        {
            _playerManager.UpdateDefense(newValue);
            defenseSlider.value = _playerManager.GetDefense();
            UpdateTextValues();
        }

        public void DropdownMenuChange(int value)
        {
            _playerManager.ChangePreset(value);
            UpdateSliderValues();
            UpdateTextValues();
        }

        private void UpdateSliderValues()
        {
            sightSlider.value = _playerManager.GetSight();
            hearingSlider.value = _playerManager.GetHearing();
            movementSlider.value = _playerManager.GetMovement();
            attackSlider.value = _playerManager.GetAttack();
            defenseSlider.value = _playerManager.GetDefense();
        }

        private void UpdateTextValues()
        {
            // Calculating the total attribute value
            totalAttributeValue = 10f - (_playerManager.GetSight() + _playerManager.GetHearing() +
                _playerManager.GetMovement() + _playerManager.GetAttack() + _playerManager.GetDefense());

            // Updating the values to the UI texts
            totalAttributeValueText.text = totalAttributeValue.ToString();
            sightValueText.text = _playerManager.GetSight().ToString();
            hearingValueText.text = _playerManager.GetHearing().ToString();
            movementValueText.text = _playerManager.GetMovement().ToString();
            attackValueText.text = _playerManager.GetAttack().ToString();
            defenseValueText.text = _playerManager.GetDefense().ToString();
        }
    }
}
