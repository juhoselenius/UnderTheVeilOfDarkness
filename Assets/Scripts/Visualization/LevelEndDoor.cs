using Logic.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Visualization
{
    public class LevelEndDoor : MonoBehaviour
    {
        private IGameManager _gameManager;
        private ScreenFader fader;
        public Animator animator;
        public bool openDoor;

        private void Awake()
        {
            _gameManager = ServiceLocator.GetService<IGameManager>();
            fader = GetComponent<ScreenFader>();
        }

        // Update is called once per frame
        void Update()
        {
            if(_gameManager.GetMoveLeft() && _gameManager.GetMoveRight() && _gameManager.GetMoveForward() && _gameManager.GetMoveBackward()
                && _gameManager.GetJump() && _gameManager.GetShoot() && _gameManager.GetChangePreset() 
                && SceneManager.GetActiveScene().name == "Level1")
            {
                OpenDoor();
            }
        }

        private void OnEnable()
        {
            //_gameManager.IntroLevelObjectivesComplete += OpenDoor;
            //OpenDoor();
        }

        private void OnDisable()
        {
            //_gameManager.IntroLevelObjectivesComplete -= OpenDoor;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                fader.FadeOut();
                SceneManager.LoadScene("PresetManagement");
            }
        }

        public void OpenDoor()
        {
            animator.SetBool("OpenDoor", true);
        }
    }
}
