using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    AudioManager audioManager;

    private bool isDestroyed = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if(hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
            audioManager.PlaySFX(audioManager.enemyDeath);
        }
    }
}
