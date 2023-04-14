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



        void Update()
        {


        }


        private void OnTriggerEnter (Collider other)
        {
            if (other.tag == "Player")
            {
                _gameManager.SetobjectivesCollected();
                Destroy(gameObject);
                
            }
        }
    }
}
    
