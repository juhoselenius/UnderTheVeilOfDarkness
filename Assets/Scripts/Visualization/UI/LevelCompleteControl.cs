using Logic.Game;
using Logic.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteControl : MonoBehaviour
{
    public TextMeshProUGUI thisRoundTime;
    public TextMeshProUGUI bestTime;

    private IGameManager _gameManager;
    private IPlayerManager _playerManager;

    private void Awake()
    {
        _playerManager = ServiceLocator.GetService<IPlayerManager>();
        _gameManager = ServiceLocator.GetService<IGameManager>();
    }

    void Start()
    {
        thisRoundTime.text = _gameManager.GetLevel2CurrentTime().ToString();
        bestTime.text = _gameManager.GetLevel2BestTime().ToString();
    }

    public void LoadMainMenu()
    {
        _playerManager.UpdateHealth(1000f);
        _gameManager.ResetLevel2EnemiesLeft();
        _gameManager.ResetLevel2ObjectivesLeft();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadPresetManagement()
    {
        _playerManager.UpdateHealth(1000f);
        _gameManager.ResetLevel2EnemiesLeft();
        _gameManager.ResetLevel2ObjectivesLeft();
        SceneManager.LoadScene("PresetManagement");
    }
}
