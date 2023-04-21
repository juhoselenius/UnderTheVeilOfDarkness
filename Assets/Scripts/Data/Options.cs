
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
            mouseSensitivity = 300f;
            reverseMouse = false;
            musicVolume = 50f;
            sfxVolume = 50f;
        }
    }
}
