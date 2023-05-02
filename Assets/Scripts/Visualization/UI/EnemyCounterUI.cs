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
    public TextMeshProUGUI objectivesCounterText;

    // Start is called before the first frame update
    void Awake()
    {
        _gameManager = ServiceLocator.GetService<IGameManager>();

        enemyCounterText = GetComponent<TextMeshProUGUI>();

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            enemyCounterText.text = "Enemies left: 0";
            objectivesCounterText.text = "Objective spheres left: 0";
        }
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            enemyCounterText.text = "Enemies left: 12";
            objectivesCounterText.text = "Objective spheres left: 2";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Level3")
        {
            enemyCounterText.text = "Enemies left: " + _gameManager.GetLevel2EnemiesLeft().ToString();
            objectivesCounterText.text = "Objective spheres left: " + _gameManager.GetLevel2ObjectivesLeft().ToString();
        }
    }
}
