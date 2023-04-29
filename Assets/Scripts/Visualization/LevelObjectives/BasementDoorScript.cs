using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Logic.Game;
using Logic.Player;

namespace Visualization
{ 
public class BasementDoorScript : MonoBehaviour
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
        
    }

    // Update is called once per frame
    void Update()
    {

        if (_gameManager.GetLevel2ObjectivesLeft() == 0)
        {
            OpenDoor();
        }

      

        
    }

 



    public void OpenDoor()
    {
        animator.SetBool("OpenDoor", true);
    }
}

}
