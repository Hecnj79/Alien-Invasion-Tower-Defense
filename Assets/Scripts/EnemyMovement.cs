using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    AudioManager audioManager;

    private Transform target;
    private int pathIndex = 0;

    private float baseSpeed;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        baseSpeed = moveSpeed;
        target = LevelManager.main.path[pathIndex];        
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if(pathIndex == LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                LevelManager.main.health -= 1;
                audioManager.PlaySFX(audioManager.minusHealth);
                if (LevelManager.main.health <= 0)
                {
                    LevelManager.main.gameOverMenu.SetActive(true);
                    Time.timeScale = 0f;
                    audioManager.PlaySFX(audioManager.gameOver);
                    Debug.Log("Game Over");
                }
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }
}
