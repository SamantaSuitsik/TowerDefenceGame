using System;
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
    //TODO: Ootab Eventsidest kui wave start vms juhtub ja ss instantiatib!
    //TODO: bug -  kui wave started event tuleb siis isWaveSTarted laheb false-ks miskiparast
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
    }

    private void ScenarioLoaded(ScenarioData data)
    {
        scenario = data;
    }

    private void Start()
    {
        nextSpawnTime = Time.time;
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        if (currentWave != null)
        {
            var data = currentWave.EnemyData;
           // print("current wave: " + data);
        }
        else
        {
            print("current wave data is not yet initialized");
        }
        
        if (nextSpawnTime <= Time.time && enemiesLeft > 0)
        {
            print("prefab: " + WayPointFollowerPrefab);
            if (WayPointFollowerPrefab == null)
            {
                print("enemy prefab is null");
                return;
            }
            print("spawn is instantiating prefabs");
            var follower = Instantiate(WayPointFollowerPrefab, transform.position, Quaternion.identity);

            if (waypoint == null)
            {
                print("waypoint is null");
                // siis enam enemyd ei spawni!
                return;
            }
            
            print("SPAWNING!");
            follower.Next = waypoint;  // Set the next waypoint for the follower
            follower.InitializeWaveData(currentWave);
            nextSpawnTime += currentWave.SpawnCooldown;
            
            enemiesLeft--;
            
        }

        else if (!GameObject.FindWithTag("Enemy") && enemiesLeft <= 0)
        {
            nextSpawnTime += Time.time;
            print("WAVE COMPLETED!!");
            Events.WaveCompleted(true);
        }
    }
}





