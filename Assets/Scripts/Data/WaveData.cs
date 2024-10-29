using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/WaveData")]
public class WaveData : ScriptableObject
{
    public EnemyData EnemyData;
    public int NumberOfEnemies = 8;
    public float SpawnCooldown = 0.5f;
    
}
