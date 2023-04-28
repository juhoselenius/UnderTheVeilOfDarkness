using Logic.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuControl : MonoBehaviour
{
    public GameObject pauseMenu;
    private IGameManager _gameManager;

    private void Awake()
    {
        _gameManager = ServiceLocator.GetService<IGameManager>();
    }

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        // Toggling Pause Menu on and off
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeInHierarchy)
            {
                Pause();
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
}
