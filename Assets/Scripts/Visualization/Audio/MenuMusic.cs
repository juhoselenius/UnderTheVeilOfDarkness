using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Logic.Options;

namespace Visualization
{
    public class MenuMusic : MonoBehaviour
    {

        public AudioSource AudioSource;
        private IOptionsManager _optionsManager;

        public Slider volumeSlider;
        private void Awake()
        {
            
            _optionsManager = ServiceLocator.GetService<IOptionsManager>();
             AudioSource.volume = _optionsManager.getMusicVolume();
            AudioSource.Play();
        }
       

        
        void Update()
        {
            AudioSource.volume = _optionsManager.getMusicVolume(); 
        }

        
    }
}
