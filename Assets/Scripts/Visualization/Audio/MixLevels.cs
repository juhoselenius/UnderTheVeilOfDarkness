using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Logic.Player;


namespace Visualization
{
    public class MixLevels : MonoBehaviour
    {

        public AudioMixerSnapshot Mute;
        public AudioMixerSnapshot Unmute;
        public IPlayerManager _playerManager;

        void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            
        }

        private void Update()
        {
            Lowpass();
        }
        void Lowpass()
        {
            if (_playerManager.GetHearing() < 1f)
            {
                Mute.TransitionTo(0.1f);
            }

            else
            {
                Unmute.TransitionTo(.01f);
            }
        }
    }
}
