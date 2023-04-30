using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic.Game;
using UnityEngine.SceneManagement;

namespace Visualization
{
    public class ObjectiveCollected : MonoBehaviour
    {
        private IGameManager _gameManager;
        public AudioSource audioSource;
        public AudioClip clip;

        private void Awake()
        {
            _gameManager = ServiceLocator.GetService<IGameManager>();
        }

        private void OnTriggerEnter (Collider other)
        {
            if (other.tag == "Player")
            {
                playSound();
                _gameManager.SetLevel2ObjectivesLeft();
                Destroy(gameObject, 0.5f);
            }
        }

        public void playSound()
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
    
