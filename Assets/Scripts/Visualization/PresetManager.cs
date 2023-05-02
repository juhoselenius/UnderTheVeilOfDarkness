using Logic.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Logic.Game;

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

        public TextMeshProUGUI infoTitleText;
        public TextMeshProUGUI infoDescriptionText;

        private IPlayerManager _playerManager;
        private IGameManager _gameManager;
    
        void Start()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            _gameManager = ServiceLocator.GetService<IGameManager>();

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
                    _gameManager.SetGamePaused();
                    Time.timeScale = 0;
                    presetMenu.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    _gameManager.SetGamePaused();
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

            if(SceneManager.GetActiveScene().name == "PresetManagement")
            {
                Image sliderFillImage = sightSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
                Image sliderHandleImage = sightSlider.transform.GetChild(2).GetChild(0).GetComponent<Image>();

                switch ((int)_playerManager.GetSight())
                {
                    case 0:
                        infoTitleText.text = "Sight - 0";
                        infoDescriptionText.text = "See no evil...";
                        sliderFillImage.color = new Color(0.8f, 0.2f, 0);
                        sliderHandleImage.color = new Color(0.8f, 0.2f, 0);
                        break;
                    case 1:
                        infoTitleText.text = "Sight - 1";
                        infoDescriptionText.text = "See evil at a small distance...\n\nlike, really really small";
                        sliderFillImage.color = new Color(1f, 0.6f, 0.4f);
                        sliderHandleImage.color = new Color(1f, 0.4f, 0.1f);
                        break;
                    case 2:
                        infoTitleText.text = "Sight - 2";
                        infoDescriptionText.text = "See evil a biiiit further away";
                        sliderFillImage.color = new Color(1f, 0.8f, 0.4f);
                        sliderHandleImage.color = new Color(1f, 0.8f, 0);
                        break;
                    case 3:
                        infoTitleText.text = "Sight - 3";
                        infoDescriptionText.text = "See evil much further away";
                        sliderFillImage.color = new Color(0.6f, 0.8f, 0.4f);
                        sliderHandleImage.color = new Color(0.4f, 0.8f, 0.2f);
                        break;
                    case 4:
                        infoTitleText.text = "Sight - 4";
                        infoDescriptionText.text = "Okay, you know how this escalates by now, right?";
                        sliderFillImage.color = new Color(0.3f, 0.6f, 0);
                        sliderHandleImage.color = new Color(0.2f, 0.4f, 0);
                        break;
                }
            }
        }

        public void OnValueChangedHearing(float newValue)
        {
            _playerManager.UpdateHearing(newValue);
            hearingSlider.value = _playerManager.GetHearing();
            UpdateTextValues();

            if (SceneManager.GetActiveScene().name == "PresetManagement")
            {
                Image sliderFillImage = hearingSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
                Image sliderHandleImage = hearingSlider.transform.GetChild(2).GetChild(0).GetComponent<Image>();

                switch ((int)_playerManager.GetHearing())
                {
                    case 0:
                        infoTitleText.text = "Hearing - 0";
                        infoDescriptionText.text = "Hear no evil... Wait, what?";
                        sliderFillImage.color = new Color(0.8f, 0.2f, 0);
                        sliderHandleImage.color = new Color(0.8f, 0.2f, 0);
                        break;
                    case 1:
                        infoTitleText.text = "Hearing - 1";
                        infoDescriptionText.text = "You hear things... like, normally";
                        sliderFillImage.color = new Color(1f, 0.6f, 0.4f);
                        sliderHandleImage.color = new Color(1f, 0.4f, 0.1f);
                        break;
                    case 2:
                        infoTitleText.text = "Hearing - 2";
                        infoDescriptionText.text = "Read sounds?\n\nNoises made by enemies are displayed as text";
                        sliderFillImage.color = new Color(1f, 0.8f, 0.4f);
                        sliderHandleImage.color = new Color(1f, 0.8f, 0);
                        break;
                    case 3:
                        infoTitleText.text = "Hearing - 3";
                        infoDescriptionText.text = "You can see with your ears...kinda...\n\nActivate a sonar pulse that briefly reveals your enemies";
                        sliderFillImage.color = new Color(0.6f, 0.8f, 0.4f);
                        sliderHandleImage.color = new Color(0.4f, 0.8f, 0.2f);
                        break;
                    case 4:
                        infoTitleText.text = "Hearing - 4";
                        infoDescriptionText.text = "Sonar on for longer period of time\n\nGo at 'em like an effing dolphin";
                        sliderFillImage.color = new Color(0.3f, 0.6f, 0);
                        sliderHandleImage.color = new Color(0.2f, 0.4f, 0);
                        break;
                }
            }
        }

        public void OnValueChangedMovement(float newValue)
        {
            _playerManager.UpdateMovement(newValue);
            movementSlider.value = _playerManager.GetMovement();
            UpdateTextValues();

            if (SceneManager.GetActiveScene().name == "PresetManagement")
            {
                Image sliderFillImage = movementSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
                Image sliderHandleImage = movementSlider.transform.GetChild(2).GetChild(0).GetComponent<Image>();

                switch ((int)_playerManager.GetMovement())
                {
                    case 0:
                        infoTitleText.text = "Movement - 0";
                        infoDescriptionText.text = "You're slow... and got no skills";
                        sliderFillImage.color = new Color(0.8f, 0.2f, 0);
                        sliderHandleImage.color = new Color(0.8f, 0.2f, 0);
                        break;
                    case 1:
                        infoTitleText.text = "Movement - 1";
                        infoDescriptionText.text = "Increased movement speed from the previous point level\n\nBut hey, we added a jump skill into the mix";
                        sliderFillImage.color = new Color(1f, 0.6f, 0.4f);
                        sliderHandleImage.color = new Color(1f, 0.4f, 0.1f);
                        break;
                    case 2:
                        infoTitleText.text = "Movement - 2";
                        infoDescriptionText.text = "Increased movement speed from the previous point level\n\nAnd now you can double jump";
                        sliderFillImage.color = new Color(1f, 0.8f, 0.4f);
                        sliderHandleImage.color = new Color(1f, 0.8f, 0);
                        break;
                    case 3:
                        infoTitleText.text = "Movement - 3";
                        infoDescriptionText.text = "Increased movement speed from the previous point level...again...\n\nAdded double jump and sprint skills";
                        sliderFillImage.color = new Color(0.6f, 0.8f, 0.4f);
                        sliderHandleImage.color = new Color(0.4f, 0.8f, 0.2f);
                        break;
                    case 4:
                        infoTitleText.text = "Movement - 4";
                        infoDescriptionText.text = "Max speed\n\nAdded double jump, sprint and dodge skills";
                        sliderFillImage.color = new Color(0.3f, 0.6f, 0);
                        sliderHandleImage.color = new Color(0.2f, 0.4f, 0);
                        break;
                }
            }
        }

        public void OnValueChangedAttack(float newValue)
        {
            _playerManager.UpdateAttack(newValue);
            attackSlider.value = _playerManager.GetAttack();
            UpdateTextValues();

            if (SceneManager.GetActiveScene().name == "PresetManagement")
            {
                Image sliderFillImage = attackSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
                Image sliderHandleImage = attackSlider.transform.GetChild(2).GetChild(0).GetComponent<Image>();

                switch ((int)_playerManager.GetAttack())
                {
                    case 0:
                        infoTitleText.text = "Attack - 0";
                        infoDescriptionText.text = "Throw stones... that's it.";
                        sliderFillImage.color = new Color(0.8f, 0.2f, 0);
                        sliderHandleImage.color = new Color(0.8f, 0.2f, 0);
                        break;
                    case 1:
                        infoTitleText.text = "Attack - 1";
                        infoDescriptionText.text = "You get a gun... wow!\n\nBetter fire rate, damage and ammo that stick to surfaces";
                        sliderFillImage.color = new Color(1f, 0.6f, 0.4f);
                        sliderHandleImage.color = new Color(1f, 0.4f, 0.1f);
                        break;
                    case 2:
                        infoTitleText.text = "Attack - 2";
                        infoDescriptionText.text = "Increased fire rate and damage from the previous point level\n\nAmmo with no perks";
                        sliderFillImage.color = new Color(1f, 0.8f, 0.4f);
                        sliderHandleImage.color = new Color(1f, 0.8f, 0);
                        break;
                    case 3:
                        infoTitleText.text = "Attack - 3";
                        infoDescriptionText.text = "Increased fire rate and damage from the previous point level\n\nFire ammo that deals damage over time";
                        sliderFillImage.color = new Color(0.6f, 0.8f, 0.4f);
                        sliderHandleImage.color = new Color(0.4f, 0.8f, 0.2f);
                        break;
                    case 4:
                        infoTitleText.text = "Attack - 4";
                        infoDescriptionText.text = "Increased fire rate and damage from the previous point level\n\nIce ammo that slow down the enemy";
                        sliderFillImage.color = new Color(0.3f, 0.6f, 0);
                        sliderHandleImage.color = new Color(0.2f, 0.4f, 0);
                        break;
                }
            }
        }

        public void OnValueChangedDefense(float newValue)
        {
            _playerManager.UpdateDefense(newValue);
            defenseSlider.value = _playerManager.GetDefense();
            UpdateTextValues();

            if (SceneManager.GetActiveScene().name == "PresetManagement")
            {
                Image sliderFillImage = defenseSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
                Image sliderHandleImage = defenseSlider.transform.GetChild(2).GetChild(0).GetComponent<Image>();

                switch ((int)_playerManager.GetDefense())
                {
                    case 0:
                        infoTitleText.text = "Defense - 0";
                        infoDescriptionText.text = "No additional defense. You're on your own, kid.";
                        sliderFillImage.color = new Color(0.8f, 0.2f, 0);
                        sliderHandleImage.color = new Color(0.8f, 0.2f, 0);
                        break;
                    case 1:
                        infoTitleText.text = "Defense - 1";
                        infoDescriptionText.text = "Your enemies do a little less damage...\n\nStill better be careful";
                        sliderFillImage.color = new Color(1f, 0.6f, 0.4f);
                        sliderHandleImage.color = new Color(1f, 0.4f, 0.1f);
                        break;
                    case 2:
                        infoTitleText.text = "Defense - 2";
                        infoDescriptionText.text = "Taking a quarter of the normal damage...\n\nYou little sissy";
                        sliderFillImage.color = new Color(1f, 0.8f, 0.4f);
                        sliderHandleImage.color = new Color(1f, 0.8f, 0);
                        break;
                    case 3:
                        infoTitleText.text = "Defense - 3";
                        infoDescriptionText.text = "The enemies do a little less damage than with 2 points...\n\nNow who's a big sissy?";
                        sliderFillImage.color = new Color(0.6f, 0.8f, 0.4f);
                        sliderHandleImage.color = new Color(0.4f, 0.8f, 0.2f);
                        break;
                    case 4:
                        infoTitleText.text = "Defense - 4";
                        infoDescriptionText.text = "Your enemies do half the normal damage\n\nGo tank it out like we know you want to";
                        sliderFillImage.color = new Color(0.3f, 0.6f, 0);
                        sliderHandleImage.color = new Color(0.2f, 0.4f, 0);
                        break;
                }
            }
        }

        public void DropdownMenuChange(int value)
        {
            _playerManager.ChangePreset(value);
            UpdateSliderValues();
            UpdateTextValues();
        }

        private void UpdateSliderValues()
        {
            defenseSlider.value = _playerManager.GetDefense();
            attackSlider.value = _playerManager.GetAttack();
            movementSlider.value = _playerManager.GetMovement();
            hearingSlider.value = _playerManager.GetHearing();
            sightSlider.value = _playerManager.GetSight();
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
