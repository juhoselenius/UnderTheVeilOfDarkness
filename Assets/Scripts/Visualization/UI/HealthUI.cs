using Logic.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public IPlayerManager _playerManager;

    public Image filler;
    public float health;
    public float previousHealth;
    public float maxHealth;

    private void Awake()
    {
        _playerManager = ServiceLocator.GetService<IPlayerManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = _playerManager.GetHealth();
        maxHealth = _playerManager.GetMaxHealth();
        filler.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(filler.fillAmount);
    }

    private void OnEnable()
    {
        _playerManager.HealthChanged += UpdateHealthBar;
        UpdateHealthBar(_playerManager.GetHealth());
    }

    private void OnDisable()
    {
        _playerManager.HealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float newHealth)
    {
        filler.fillAmount = newHealth / maxHealth;
        
        /*StopCoroutine("SmoothDecrease");
        previousHealth = health;
        health = newHealth;
        StartCoroutine(SmoothDecrease(previousHealth, health, 0.5f));*/
    }

    /*IEnumerator SmoothDecrease(float startHealth, float endHealth, float duration)
    {
        float elapsedTime = 0f;
        float fillStart = startHealth / maxHealth;
        float fillEnd = endHealth / maxHealth;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            filler.fillAmount = Mathf.Lerp(fillStart, fillEnd, t);
            yield return null;
        }
    }*/
}
