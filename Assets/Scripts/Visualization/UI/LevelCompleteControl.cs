using Logic.Game;
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

    private void Awake()
    {
        _gameManager = ServiceLocator.GetService<IGameManager>();
    }

    void Start()
    {
        thisRoundTime.text = _gameManager.GetLevel2CurrentTime().ToString();
        bestTime.text = _gameManager.GetLevel2BestTime().ToString();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevel3()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Level3");
    }
}
