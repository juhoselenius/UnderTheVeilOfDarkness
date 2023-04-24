using UnityEngine;
using Logic.Game;
using TMPro;
using UnityEngine.SceneManagement;
using Logic.Player;
using UnityEngine.UI;

namespace Visualization
{
    public class LevelTimerUI : MonoBehaviour
    {
        public IGameManager _gameManager;
        public IPlayerManager _playerManager;

        public float levelTime;
        public float currentLevelTime;

        public GameObject timeBar;
        public TextMeshProUGUI timeText;
        public Image filler;

        private void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            _gameManager = ServiceLocator.GetService<IGameManager>();
        }

        void Start()
        {
            currentLevelTime = levelTime;
        }

        void Update()
        {
            if(SceneManager.GetActiveScene().name == "Level2")
            {
                if(_gameManager.GetallEnemiesCleared() < 18)
                {
                    currentLevelTime -= Time.deltaTime;
                    timeText.text = ((int)Mathf.Round(currentLevelTime)).ToString();
                }
                else
                {
                    timeBar.SetActive(false);
                }

                if(currentLevelTime <= 0)
                {
                    // Code for what happens when player dies
                    Debug.Log("Player is dead!");
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    _playerManager.UpdateHealth(1000f);
                    SceneManager.LoadScene("GameOver");
                }

                filler.fillAmount = currentLevelTime / levelTime;
            }
        }
    }
}
