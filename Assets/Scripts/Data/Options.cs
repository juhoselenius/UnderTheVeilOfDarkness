
using System;

namespace Data
{
    [Serializable]
    public class Options
    {
        public float mouseSensitivity;
        public bool reverseMouse;
        public float musicVolume;
        public float sfxVolume;


        public Options()
        {
            mouseSensitivity = 40f;
            reverseMouse = false;
            musicVolume = 0.5f;
            sfxVolume = 0.5f;
        }
    }
}
