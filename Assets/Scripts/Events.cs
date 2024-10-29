using System;
using UnityEngine.SceneManagement;

public static class Events
{
   public static event Action<TowerData> OnTowerSelected; //towerdata is the input that is sent in
   //Through this we can call OnTowerSelected
   public static void TowerSelected(TowerData data) => OnTowerSelected?.Invoke(data);

   public static event Func<int> OnGetLives;
   public static int GetLives() => OnGetLives?.Invoke() ?? 0;
   
   public static event Action<int> OnChangeLives;
   public static void ChangeLives(int givenLives)
   {
      OnChangeLives?.Invoke(givenLives);
   }
   
   public static event Func<int> OnGetGold;
   public static int GetGold() => OnGetGold?.Invoke() ?? 0;

   public static event Action<int> OnSetGold;
   public static void SetGold(int givenGold) => OnSetGold?.Invoke(givenGold);

   public static event Action<ScenarioData> OnScenarioSelected;
   public static void ScenarioSelected(ScenarioData data) => OnScenarioSelected?.Invoke(data);

   public static event Action<ScenarioData> OnScenarioLoaded;
   public static void ScenarioLoaded(ScenarioData data) => OnScenarioLoaded?.Invoke(data);
   
   public static event Action<ScenarioData> OnWaveStart;

}
