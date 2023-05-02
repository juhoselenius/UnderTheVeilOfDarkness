using Logic.Game;
using Logic.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    
    private GameObject[] musicObj;
    private IOptionsManager _optionsManager;
    public AudioSource audios;
    // Start is called before the first frame update
    void Awake()
    {
        _optionsManager = ServiceLocator.GetService<IOptionsManager>();
        musicObj = GameObject.FindGameObjectsWithTag("MainMenuMusic");
        audios.volume = _optionsManager.getMusicVolume();

        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);                 
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            
        }

    }
 
}
