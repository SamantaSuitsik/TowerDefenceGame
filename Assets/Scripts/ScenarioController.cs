using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioController : MonoBehaviour
{
    //Events on lihtsalt triggeri (märguande) andmise jaoks
    // See klass aga päriselt muudab ja storeib neid asju, mille kohta events 
    // teateid jagab.
    
    public int Lives = 10;
    public int Gold = 30;
    private ScenarioData ScenarioData;
    private bool levelRunning;

    private void Awake()
    {
        Events.OnChangeLives += OnChangeLives; //Lisame kuulajaks
        Events.OnGetLives += OnGetLives;

        Events.OnSetGold += OnSetGold;
        Events.OnGetGold += OnGetGold;

        Events.OnScenarioLoaded += ScenarioLoaded;
    }

    private void ScenarioLoaded(ScenarioData scenario)
    {
        // StartLevel
        levelRunning = true;        
        ScenarioData = scenario;
        Events.SetGold(scenario.Gold);
        Events.ChangeLives(scenario.Lives);
        //elud kullad wave -- get the wame from the event 
        // Events.OnWaveStart(obj.Waves[CurrentWave])
    }

    private bool OnGetIsLost()
    {
        if (Lives <= 0)
            return true;
        return false;
    }

    private int OnGetGold()
    {
        return Gold;
    }

    private void OnSetGold(int newGold)
    {
        Gold = newGold;
    }

    private int OnGetLives()
    {
        // Kui keegi tahab elusid siis anname talle need siit
        return Lives;
    }

    private void OnChangeLives(int newLives)
    {
        // Kui keegi muudab elusid siis paneme elud selle muutuse järgi
        Lives = newLives;
        isLost();
    }

    private void OnDestroy()
    {
        Events.OnChangeLives -= OnChangeLives;
        Events.OnGetLives -= OnGetLives;

        Events.OnScenarioLoaded -= ScenarioLoaded;
    }

    void Start()
    {
        Events.ChangeLives(Lives);
        Events.SetGold(Gold);

    }

    private void isLost()
    {
        if (Lives > 0)
            return;
        HUD.Instance.ShowGameOverScreen(false);
        // EndScenario();
    }

    private void Update()
    {
        // TODO: bug - see ei toota praegu !! Ehk siis voita ei saa hetkel
        if (!GameObject.FindWithTag("Enemy"))
        {
            HUD.Instance.ShowGameOverScreen(true);
        }
    }

    public void EndScenario()
    {
        SceneManager.LoadScene(0);
        if (Menu.Instance != null)
        {
            Menu.Instance.gameObject.SetActive(true);
        }
    }
}
