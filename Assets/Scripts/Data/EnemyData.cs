using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int Health = 3;
    public int Damage = 1;
    public float MovementSpeed = 5f;
}
