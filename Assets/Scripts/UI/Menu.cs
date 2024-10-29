using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu Instance;
    public ScenarioData SelectedScenario;
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Awake()
    {
        Events.OnScenarioLoaded += ScenarioSelected;
        
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
            Events.OnScenarioSelected += ScenarioSelected;
        }
    }

    private void ScenarioSelected(ScenarioData scenario)
    {
        SceneManager.LoadScene(scenario.SceneName);
    }

    private void OnLevelWasLoaded(int level)
    {
        gameObject.SetActive(level == 0);
        if (SelectedScenario != null)
        {
            Events.ScenarioLoaded(SelectedScenario);
        }

    }
}
