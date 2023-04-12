using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public GameObject presetInfoCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        
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

    public void LoadIntroLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    // Functions for the Preset Management scene
    public void TogglePresetInfo()
    {
        if (!presetInfoCanvas.activeInHierarchy)
        {
            presetInfoCanvas.SetActive(true);
        }
        else
        {
            presetInfoCanvas.SetActive(false);
        }
    }
}
