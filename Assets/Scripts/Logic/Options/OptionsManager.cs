using System;
using UnityEngine;

namespace Logic.Options
{
    public class OptionsManager : IOptionsManager
    {
        private Data.Options _optionsState;

        public OptionsManager() : this(new Data.Options()) { }

        public OptionsManager(Data.Options initialOptionsState)
        {
            _optionsState = initialOptionsState;
        }

        public event Action<float> mouseSensitivityChanged;
        public event Action<bool> reverseMouseChanged;
        public event Action<float> musicVolumeChanged;
        public event Action<float> sfxVolumeChanged;

        public float getMouseSensitivity()
        {
            return _optionsState.mouseSensitivity;
        }

        public void updateMouseSensitivity(float value)
        {
            Debug.Log($"Mouse sensitivity in OptionsManager: {value}!");
            _optionsState.mouseSensitivity = value;
            mouseSensitivityChanged?.Invoke(_optionsState.mouseSensitivity);
        }

        public bool getReverseMouse()
        {
            return _optionsState.reverseMouse;
        }

        public void updateReverseMouse(bool value)
        {
            Debug.Log($"Reverse mouse in OptionsManager: {value}!");
            _optionsState.reverseMouse = value;
            reverseMouseChanged?.Invoke(_optionsState.reverseMouse);
        }

        public float getMusicVolume()
        {
            return _optionsState.musicVolume;
        }

        public void updateMusicVolume(float value)
        {
            Debug.Log($"Music volume in OptionsManager: {value}!");
            _optionsState.musicVolume = value;
            musicVolumeChanged?.Invoke(_optionsState.musicVolume);
        }

        public float getSfxVolume()
        {
            return _optionsState.sfxVolume;
        }

        public void updateSfxVolume(float value)
        {
            Debug.Log($"SFX volume in OptionsManager: {value}!");
            _optionsState.sfxVolume = value;
            sfxVolumeChanged?.Invoke(_optionsState.sfxVolume);
        }
    }
}
