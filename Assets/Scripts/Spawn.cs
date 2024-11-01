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
    
    //TODO: Ootab Eventsidest kui wave start vms juhtub ja ss instantiatib!
    private void Awake()
    {
        Events.OnScenarioLoaded += ScenarioLoaded;

    }

    private void OnDestroy()
    {
        Events.OnScenarioLoaded -= ScenarioLoaded;
    }

    private void ScenarioLoaded(ScenarioData scenario)
    {
        ScenarioData = scenario;
    }

    private void Start()
    {
        nextSpawnTime = Time.time;
        waypoint = GetComponent<Waypoint>();
    }

    void Update()
    {
        if (nextSpawnTime <= Time.time)
        {
            if (WayPointFollowerPrefab == null)
            {
                return;
            }

            var follower = Instantiate(WayPointFollowerPrefab, transform.position, Quaternion.identity);

            if (waypoint == null)
            {
                return;
            }
    
            follower.Next = waypoint;  // Set the next waypoint for the follower
            nextSpawnTime += SpawnDelay;
        }
    }
}





