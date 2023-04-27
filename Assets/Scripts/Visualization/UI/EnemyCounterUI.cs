using Logic.Game;
using Logic.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCounterUI : MonoBehaviour
{
    public IGameManager _gameManager;
    private TextMeshProUGUI enemyCounterText;

    // Start is called before the first frame update
    void Awake()
    {
        _gameManager = ServiceLocator.GetService<IGameManager>();

        enemyCounterText = GetComponent<TextMeshProUGUI>();

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            enemyCounterText.text = "Enemies left: 0";
        }
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            enemyCounterText.text = "Enemies left: 18";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Level2")
        {
            enemyCounterText.text = "Enemies left: " + (_gameManager.GetLevel2EnemiesLeft()).ToString();
        }
    }
}
