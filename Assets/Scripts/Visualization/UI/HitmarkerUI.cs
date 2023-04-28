using Logic.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitmarkerUI : MonoBehaviour
{
    private IGameManager _gameManager;

    public Sprite[] hitmarkerSprites;
    public Image hitmarker;
    
    // Start is called before the first frame update
    void Awake()
    {
        _gameManager = ServiceLocator.GetService<IGameManager>();

        if (hitmarker.enabled)
        {
            hitmarker.enabled = false;
        }
    }

    private void OnEnable()
    {
        _gameManager.Level2EnemiesLeftChanged += SetDeathHitmarker;
    }

    private void OnDisable()
    {
        _gameManager.Level2EnemiesLeftChanged -= SetDeathHitmarker;
    }

    private void SetDeathHitmarker(int value)
    {
        DeathHitActive();
        Invoke("DeactivateHitmarker", 0.2f);
    }

    void DeathHitActive()
    {
        hitmarker.enabled = true;
        hitmarker.sprite = hitmarkerSprites[1];
    }

    void DeactivateHitmarker()
    {
        hitmarker.enabled = false;
    }

    public void SetHitmarker()
    {
        hitmarker.enabled = true;
        hitmarker.sprite = hitmarkerSprites[0];
        Invoke("DeactivateHitmarker", 0.2f);
    }
}
