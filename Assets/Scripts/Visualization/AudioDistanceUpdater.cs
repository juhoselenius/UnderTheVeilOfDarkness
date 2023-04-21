using Logic.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visualization
{
    public class AudioDistanceUpdater : MonoBehaviour
    {
        public IPlayerManager _playerManager;

        private void Awake()
        {
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
        }

        void Start()
        {
            UpdateAudioDistance(_playerManager.GetHearing());
        }

        private void OnEnable()
        {
            _playerManager.HearingChanged += UpdateAudioDistance;
            UpdateAudioDistance(_playerManager.GetHearing());
        }

        private void OnDisable()
        {
            _playerManager.HearingChanged -= UpdateAudioDistance;
        }

        private void UpdateAudioDistance(float newDistance)
        {
            AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

            foreach (AudioSource source in audioSources)
            {
                source.maxDistance = 1f + newDistance * 0.2f;
            }
        }
    }
}
