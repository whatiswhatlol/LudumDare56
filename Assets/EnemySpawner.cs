using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }  // Singleton instance

    public GameObject enemyPrefab;  // The enemy prefab to spawn
    public int enemiesPerWave = 5;  // The number of enemies per wave
    public float spawnInterval = 1f;  // Time interval between spawning each enemy
    public float waveDelay = 5f;  // Time to wait before starting the next wave
    private int currentWave = 0;  // The current wave number
    private int activeEnemies = 0;  // Keeps track of how many enemies are still active

    private bool waveInProgress = false;

    public List<EnemyStats> enemyStats;

    void Awake()
    {
        // Ensure only one instance of the spawner exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Keep the spawner across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }
    }

    void Start()
    {
        StartNextWave();  // Start the first wave immediately
    }

    void Update()
    {
        // If there are no more active enemies, start the next wave
        if (activeEnemies == 0 && !waveInProgress)
        {
            StartCoroutine(StartNextWaveWithDelay());
        }
    }

    // Coroutine to handle the delay between waves
    private IEnumerator StartNextWaveWithDelay()
    {
        waveInProgress = true;
        yield return new WaitForSeconds(waveDelay);  // Wait before starting the next wave
        StartNextWave();
    }

    // Start spawning the next wave of enemies
    private void StartNextWave()
    {
        currentWave++;
        int enemiesToSpawn = enemiesPerWave + (currentWave - 1);  // Increase number of enemies per wave
        activeEnemies = enemiesToSpawn;
        waveInProgress = false;
        StartCoroutine(SpawnEnemiesGradually(enemiesToSpawn));
    }

    // Coroutine to spawn enemies gradually off-screen
    private IEnumerator SpawnEnemiesGradually(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemyOffScreen();
            yield return new WaitForSeconds(spawnInterval);  // Wait before spawning the next enemy
        }
    }

    // Spawn a single enemy off-screen but within bounds
    private void SpawnEnemyOffScreen()
    {
        Vector3 spawnPosition = GetRandomOffScreenPosition();
        GameObject temp = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemyStats.Add(temp.GetComponent<EnemyStats>());   
    }

    // Get a random off-screen position just outside the visible area
    private Vector3 GetRandomOffScreenPosition()
    {
        float xPos = 0f;
        float yPos = 0f;

        // Randomly decide which edge of the screen to spawn the enemy
        int spawnEdge = Random.Range(0, 4);

        switch (spawnEdge)
        {
            // Left side
            case 0:
                xPos = Random.Range(-0.2f, -0.1f);  // Spawn off-screen on the left
                yPos = Random.Range(0.1f, 0.9f);    // Random Y position within visible screen bounds
                break;
            // Right side
            case 1:
                xPos = Random.Range(1.1f, 1.2f);    // Spawn off-screen on the right
                yPos = Random.Range(0.1f, 0.9f);    // Random Y position within visible screen bounds
                break;
            // Top side
            case 2:
                xPos = Random.Range(0.1f, 0.9f);    // Random X position within visible screen bounds
                yPos = Random.Range(1.1f, 1.2f);    // Spawn off-screen at the top
                break;
            // Bottom side
            case 3:
                xPos = Random.Range(0.1f, 0.9f);    // Random X position within visible screen bounds
                yPos = Random.Range(-0.2f, -0.1f);  // Spawn off-screen at the bottom
                break;
        }

        // Convert the viewport coordinates to world coordinates for spawning
        Vector3 worldPosition = Camera.main.ViewportToWorldPoint(new Vector3(xPos, yPos, Camera.main.nearClipPlane));
        worldPosition.z = 0;  // Ensure the enemy spawns on the same plane (z = 0)
        return worldPosition;
    }

    // Call this method when an enemy is defeated to reduce the active enemy count
    public void OnEnemyDefeated()
    {
        activeEnemies--;
    }
}
