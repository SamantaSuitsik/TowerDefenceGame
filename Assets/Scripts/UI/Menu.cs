using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu Instance;
    public ScenarioCard ScenarioCard;
    public RectTransform ScenarioPanel;
    [HideInInspector]
    public ScenarioData SelectedScenario;
    public List<ScenarioData> Scenarios;

    private void Start()
    {
        foreach (var scenario in Scenarios)
        {
            ScenarioCard card = GameObject.Instantiate(ScenarioCard, ScenarioPanel);
            card.SetData(scenario);
            
        }
    }

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

    public void ScenarioSelected(ScenarioData scenario)
    {
        SelectedScenario = scenario;
        SceneManager.LoadScene(scenario.SceneName);
    }

    private void OnLevelWasLoaded(int level)
    {
        print("olen on level was loaded methodis");
        gameObject.SetActive(level == 0);
        if (SelectedScenario != null)
        {
            print("scenario action was fired");
            Events.ScenarioLoaded(SelectedScenario);
        }

    }
}
