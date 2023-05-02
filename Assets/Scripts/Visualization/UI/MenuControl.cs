using Logic.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public GameObject presetInfoCanvas;
    public GameObject presetMenu;

    public GameObject[] attributeInfoCanvas;

    private IGameManager _gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        _gameManager = ServiceLocator.GetService<IGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
        Destroy(GameObject.FindGameObjectWithTag("MainMenuMusic"));
    }

    public void LoadPresetManagement()
    {
        _gameManager.ResetLevel2EnemiesLeft();
        _gameManager.ResetLevel2ObjectivesLeft();
        SceneManager.LoadScene("PresetManagement");
    }

    public void LoadIntroLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3()
    {
        _gameManager.ResetLevel2EnemiesLeft();
        _gameManager.ResetLevel2ObjectivesLeft();
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Level3");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void Retry()
    {
        _gameManager.ResetLevel2EnemiesLeft();
        _gameManager.ResetLevel2ObjectivesLeft();
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Functions for the Preset Management scene
    public void TogglePresetInfo()
    {
        if (!presetInfoCanvas.activeInHierarchy)
        {
            presetInfoCanvas.SetActive(true);
            presetMenu.SetActive(false);
        }
        else
        {
            presetInfoCanvas.SetActive(false);
            presetMenu.SetActive(true);
        }
    }

    public void DropdownMenuChange(int value)
    {
        int i = 0;

        while (i < attributeInfoCanvas.Length)
        {
            if(i == value)
            {
                attributeInfoCanvas[i].SetActive(true);
            }
            else
            {
                attributeInfoCanvas[i].SetActive(false);
            }

            i++;
        }
    }
}
