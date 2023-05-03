using UnityEngine;
using Logic.Options;
using UnityEngine.UI;
using TMPro;

namespace Visualization
{
    public class SetupMenu : MonoBehaviour
    {
        private IOptionsManager _optionsManager;
        public Slider mouseSensitivitySlider;
        public Slider musicVolumeSlider;
        public Slider sfxVolumeSlider;
        public Toggle reverseToggle;
        public TextMeshProUGUI mousevalue;
        public TextMeshProUGUI musicValue;
        public TextMeshProUGUI sfxValue;
        public TextMeshProUGUI reverseValue;
        

        private void Awake()
        {
            _optionsManager = ServiceLocator.GetService<IOptionsManager>();
        }

        private void Start()
        {
            mouseSensitivitySlider.value = _optionsManager.getMouseSensitivity();
            mousevalue.text = mouseSensitivitySlider.value.ToString();

            reverseToggle.isOn = _optionsManager.getReverseMouse();
            reverseValue.text = reverseToggle.isOn.ToString();

            musicVolumeSlider.value = _optionsManager.getMusicVolume();
            musicValue.text = musicVolumeSlider.value.ToString();
            
           

            sfxVolumeSlider.value = _optionsManager.getSfxVolume();
            sfxValue.text = sfxVolumeSlider.value.ToString();
        }
        
            
        

        public void setMouseSensitivity()
        {
            _optionsManager.updateMouseSensitivity(mouseSensitivitySlider.value);
            mousevalue.text = mouseSensitivitySlider.value.ToString();
        }

        public void setReverseMouse()
        {
            _optionsManager.updateReverseMouse(reverseToggle.isOn);
            reverseValue.text = reverseToggle.isOn.ToString();
        }

        public void setMusicVolume()
        {
            _optionsManager.updateMusicVolume(musicVolumeSlider.value);
            musicValue.text = Mathf.RoundToInt(musicVolumeSlider.value * 100).ToString();
            
        }
        
        public void setSFXVolume()
        {
            _optionsManager.updateSfxVolume(sfxVolumeSlider.value);
            sfxValue.text = Mathf.RoundToInt(sfxVolumeSlider.value * 100).ToString();
        }
    }
}
