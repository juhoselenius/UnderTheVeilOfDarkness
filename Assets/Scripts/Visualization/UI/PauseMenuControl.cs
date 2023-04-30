using Logic.Game;
using Logic.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Visualization
{
    public class PauseMenuControl : MonoBehaviour
    {
        private IPlayerManager _playerManager;
        private IGameManager _gameManager;
    
        public GameObject pauseMenu;

        public Slider sightSlider;
        public Slider hearingSlider;
        public Slider movementSlider;
        public Slider attackSlider;
        public Slider defenseSlider;

        public TextMeshProUGUI sightValueText;
        public TextMeshProUGUI hearingValueText;
        public TextMeshProUGUI movementValueText;
        public TextMeshProUGUI attackValueText;
        public TextMeshProUGUI defenseValueText;

        private void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            _gameManager = ServiceLocator.GetService<IGameManager>();
        }

        void Start()
        {
            pauseMenu.SetActive(false);
            UpdateSliderValues();
        }

        void Update()
        {
            // Toggling Pause Menu on and off
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!pauseMenu.activeInHierarchy)
                {
                    Pause();
                    UpdateSliderValues();
                    pauseMenu.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Resume();
                    pauseMenu.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }

        private void Pause()
        {
            Time.timeScale = 0;
            _gameManager.SetGamePaused();
        }

        private void Resume()
        {
            Time.timeScale = 1;
            _gameManager.SetGamePaused();
        }

        public void UpdateSliderValues()
        {
            defenseSlider.value = _playerManager.GetDefense();
            attackSlider.value = _playerManager.GetAttack();
            movementSlider.value = _playerManager.GetMovement();
            hearingSlider.value = _playerManager.GetHearing();
            sightSlider.value = _playerManager.GetSight();

            sightValueText.text = _playerManager.GetSight().ToString();
            hearingValueText.text = _playerManager.GetHearing().ToString();
            movementValueText.text = _playerManager.GetMovement().ToString();
            attackValueText.text = _playerManager.GetAttack().ToString();
            defenseValueText.text = _playerManager.GetDefense().ToString();
        }

        public void DropdownMenuChange(int value)
        {
            _playerManager.ChangePreset(value);
            UpdateSliderValues();
        }
    }
}
