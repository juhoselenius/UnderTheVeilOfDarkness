using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.Options
{ 
    public interface IOptionsManager
    {
        event Action<float> mouseSensitivityChanged;
        float getMouseSensitivity();
        void updateMouseSensitivity(float change);

        event Action<bool> reverseMouseChanged;
        bool getReverseMouse();
        void updateReverseMouse(bool change);

        event Action<float> musicVolumeChanged;
        float getMusicVolume();
        void updateMusicVolume(float change);

        event Action<float> sfxVolumeChanged;
        float getSfxVolume();
        void updateSfxVolume(float change);

    }
}
