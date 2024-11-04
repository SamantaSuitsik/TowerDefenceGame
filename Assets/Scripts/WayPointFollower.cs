using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    public Waypoint Next;
    public float Speed = 1f;
    private WaveData wave;
    private int damageInWave;

    private void Awake()
    {
        Events.OnWaveStart += WaveStart;

    }
    

    private void OnDestroy()
    {
        Events.OnWaveStart -= WaveStart;
    }

    private void WaveStart(WaveData data)
    {
        wave = data;
        Speed = wave.EnemyData.MovementSpeed;

    }

    public void InitializeWaveData(WaveData data)
    {
        wave = data;
        Speed = wave.EnemyData.MovementSpeed;
        damageInWave = wave.EnemyData.Damage;
    }


    void Update()
    {
        var current = transform.position;
        if (Next == null) return;
        transform.position = Vector3.MoveTowards(current, Next.transform.position, Time.deltaTime * Speed);

        if (Vector3.SqrMagnitude(current - Next.transform.position) <= float.Epsilon)
        {
            Next = Next.GetNextWaypoint();
            if (Next == null)
            {
                ReachEnd();
            }
        }
    }

    void ReachEnd()
    {
        if (wave != null && wave.EnemyData != null)
        {
            Events.ChangeLives(Events.GetLives() - damageInWave);
        }
        Destroy(gameObject);
    }
}


























