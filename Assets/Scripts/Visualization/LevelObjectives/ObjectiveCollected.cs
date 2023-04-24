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

        private void Awake()
        {
            _gameManager = ServiceLocator.GetService<IGameManager>();
        }

        private void OnTriggerEnter (Collider other)
        {
            if (other.tag == "Player")
            {
                _gameManager.SetObjectivesCollected();
                Destroy(gameObject);
            }
        }
    }
}
    
