using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public WayPointFollower WayPointFollowerPrefab;
    public float SpawnDelay = 0.25f;
    private float nextSpawnTime = 0;
    private Waypoint waypoint;
    private ScenarioData scenario;
    private WaveData currentWave;
    private int enemiesLeft = 0;

    private bool isWaveStarted;

    private void Awake()
    {
        Events.OnScenarioLoaded += ScenarioLoaded;
        Events.OnWaveStart += WaveStart;
    }

    private void OnDestroy()
    {
        Events.OnScenarioLoaded -= ScenarioLoaded;
        Events.OnWaveStart -= WaveStart;
    }

    private void WaveStart(WaveData data)
    {
        currentWave = data;
        isWaveStarted = true;
        enemiesLeft = currentWave.NumberOfEnemies;
        print("Wave started. isWaveStarted set to true. Enemies to spawn: " + enemiesLeft);
    }

    private void ScenarioLoaded(ScenarioData data)
    {
        scenario = data;
        print("Scenario loaded: " + scenario);
    }

    private void Start()
    {
        nextSpawnTime = Time.time;
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        if (isWaveStarted && nextSpawnTime <= Time.time && enemiesLeft > 0)
        {
            SpawnEnemy();
        }

        CheckWaveCompletion();
    }

    private void SpawnEnemy()
    {
        if (WayPointFollowerPrefab == null)
        {
            return;
        }

        print("Spawning enemy...");
        var follower = Instantiate(WayPointFollowerPrefab, transform.position, Quaternion.identity);

        if (waypoint == null)
        {
            print("No waypoint assigned. Stopping further spawns.");
            return;
        }

        var healthComponent = follower.GetComponent<Health>();
        if (healthComponent != null && currentWave.EnemyData != null)
        {
            healthComponent.InitializeEnemyInfo(currentWave.EnemyData);
        }

        follower.Next = waypoint;
        follower.InitializeWaveData(currentWave);
        
        nextSpawnTime = Time.time + currentWave.SpawnCooldown;
        enemiesLeft--;
        print("Enemy spawned. Enemies left to spawn: " + enemiesLeft);
    }

    private void CheckWaveCompletion()
    {
        if (enemiesLeft <= 0 && !GameObject.FindWithTag("Enemy"))
        {
            isWaveStarted = false;
            print("Wave completed!");
            Events.WaveCompleted(true);
        }
    }
}
