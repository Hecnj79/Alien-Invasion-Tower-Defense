using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner main;

    public GameObject gameWinMenu;

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] upgradedEnemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 15f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    AudioManager audioManager;

    public int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps; //enemies per second
    private bool isSpawning = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        onEnemyDestroy.AddListener(EnemyDestroyed);
        main = this;
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }

    private void EndWave()
    {
        if (currentWave != 50)
        {
            isSpawning = false;
            timeSinceLastSpawn = 0f;
            currentWave++;
            LevelManager.main.currency += 99 + currentWave - 1;
            //enemiesPerSecond += 0.5f;
            StartCoroutine(StartWave());
        }
        else
        {
            audioManager.PlaySFX(audioManager.gameWin);
            gameWinMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void SpawnEnemy()
    {
        if (currentWave <= 3)
        {
            
            GameObject prefabToSpawn = enemyPrefabs[0];
            Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
            
        }
        else
        {
            if (currentWave > 3 && currentWave <= 6)
            {
                int index = Random.Range(0, 8);
                GameObject prefabToSpawn = enemyPrefabs[index];
                Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
            }
            else
            {
                if ( currentWave > 6 && currentWave <= 9)
                {
                    int index = Random.Range(0, 10);
                    GameObject prefabToSpawn = enemyPrefabs[index];
                    Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
                }
                else
                {
                    if(currentWave > 9 && currentWave <= 15)
                    {
                        int index = Random.Range(0, enemyPrefabs.Length);
                        GameObject prefabToSpawn = enemyPrefabs[index];
                        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
                    }
                    else
                    {
                        if(currentWave > 15 && currentWave <= 20)
                        {
                            //int index = Random.Range(0, 11);
                            GameObject prefabToSpawn = upgradedEnemyPrefabs[0];
                            Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
                        }
                        else
                        {
                            if (currentWave > 20 && currentWave <= 27)
                            {
                                int index = Random.Range(0, 8);
                                GameObject prefabToSpawn = upgradedEnemyPrefabs[index];
                                Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
                            }
                            else
                            {
                                if (currentWave > 27 && currentWave <= 34)
                                {
                                    int index = Random.Range(0, 10);
                                    GameObject prefabToSpawn = upgradedEnemyPrefabs[index];
                                    Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
                                }
                                else
                                {
                                    if (currentWave > 34 && currentWave <= 44)
                                    {
                                        int index = Random.Range(0, 11);
                                        GameObject prefabToSpawn = upgradedEnemyPrefabs[index];
                                        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
                                    }
                                    else
                                    {
                                        if (currentWave > 44 && currentWave <= 50)
                                        {
                                            int index = Random.Range(0, upgradedEnemyPrefabs.Length);
                                            GameObject prefabToSpawn = upgradedEnemyPrefabs[index];
                                            Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }
        
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }
}
