using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/ScenarioData")]
public class ScenarioData : ScriptableObject
{
    public string PresenterName;
    public string SceneName;
    public int Lives;
    public int Gold;
    public WaveData[] Waves;
    public TowerData[] Towers;
}
