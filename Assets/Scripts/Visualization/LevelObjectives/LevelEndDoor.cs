using Logic.Game;
using Logic.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Visualization
{
    public class LevelEndDoor : MonoBehaviour
    {
        private IGameManager _gameManager;
        private IPlayerManager _playerManager;
        private ScreenFader fader;
        public Animator animator;
        public bool levelObjectivesDone;
        public string sceneToLoad;

        private float levelTime;

        private void Awake()
        {
            _gameManager = ServiceLocator.GetService<IGameManager>();
            _playerManager = ServiceLocator.GetService<IPlayerManager>();
            fader = GetComponent<ScreenFader>();
            levelTime = 0;
        }

        // Update is called once per frame
        void Update()
        {
            /*if(_gameManager.GetMoveLeft() && _gameManager.GetMoveRight() && _gameManager.GetMoveForward() && _gameManager.GetMoveBackward()
                && _gameManager.GetJump() && _gameManager.GetShoot() && _gameManager.GetChangePreset() 
                && SceneManager.GetActiveScene().name == "Level1")
            {
                OpenDoor();
            }*/

            if(levelObjectivesDone)
            {
                OpenDoor();
            }
            
            if (SceneManager.GetActiveScene().name == "Level3" && _gameManager.GetLevel2ObjectivesLeft() == 0 && _gameManager.GetLevel2EnemiesLeft() == 0)
            {
                OpenDoor();
            }

            levelTime += Time.deltaTime;
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
            // This ends the scene and trasitions to another
            if(other.tag == "Player")
            {
                // Updating the level completion time to the Game Manager
                if(SceneManager.GetActiveScene().name == "Level3")
                {
                    _gameManager.SetLevel2CurrentTime(Mathf.RoundToInt(levelTime));
                    if(levelTime < _gameManager.GetLevel2BestTime())
                    {
                        _gameManager.SetLevel2BestTime(Mathf.RoundToInt(levelTime));
                    }
                }

                // Resetting the objectives for next run
                _gameManager.ResetLevel2EnemiesLeft();
                _gameManager.ResetLevel2ObjectivesLeft();

                fader.FadeOut();
                if(sceneToLoad != null)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    SceneManager.LoadScene(sceneToLoad);
                }
            }
        }

        public void OpenDoor()
        {
            animator.SetBool("OpenDoor", true);
        }
    }
}
