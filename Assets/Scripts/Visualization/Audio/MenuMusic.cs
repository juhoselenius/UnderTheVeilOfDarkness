using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Logic.Options;

namespace Visualization
{
    public class MenuMusic : MonoBehaviour
    {

        private AudioSource AudioSource;
        private IOptionsManager _optionsManager;

        public Slider volumeSlider;
        private void Awake()
        {         
            _optionsManager = ServiceLocator.GetService<IOptionsManager>(); 
            AudioSource = GameObject.FindGameObjectWithTag("MainMenuMusic").GetComponent<AudioSource>();
            AudioSource.volume = _optionsManager.getMusicVolume();
            //AudioSource.Play();
        }
       

        
        void Update()
        {
            AudioSource.volume = _optionsManager.getMusicVolume(); 
        }

        
    }
}
