using Logic.Game;
using Logic.Player;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private int tempCurrentPreset;

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
                    tempCurrentPreset = _playerManager.GetCurrentPreset();
                    _gameManager.SetGamePaused();
                    Time.timeScale = 0;
                    pauseMenu.SetActive(true);
                    UpdateSliderValues();
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    _playerManager.ChangePreset(tempCurrentPreset);
                    _gameManager.SetGamePaused();
                    pauseMenu.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Time.timeScale = 1;
                }
            }
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

        public void LoadMainMenu()
        {
            _gameManager.ResetLevel2EnemiesLeft();
            _gameManager.ResetLevel2ObjectivesLeft();
            Time.timeScale = 1;
            _gameManager.SetGamePaused();
            SceneManager.LoadScene("MainMenu");
        }

        public void LoadPresetManagement()
        {
            _gameManager.ResetLevel2EnemiesLeft();
            _gameManager.ResetLevel2ObjectivesLeft();
            Time.timeScale = 1;
            _gameManager.SetGamePaused();
            SceneManager.LoadScene("PresetManagement");
        }

        public void LoadLevel3()
        {
            _gameManager.ResetLevel2EnemiesLeft();
            _gameManager.ResetLevel2ObjectivesLeft();
            Time.timeScale = 1;
            _gameManager.SetGamePaused();
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene("Level3");
        }
    }
}
