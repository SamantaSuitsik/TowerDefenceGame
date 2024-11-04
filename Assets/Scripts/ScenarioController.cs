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
    private int currentWaveIndex;

    private void Awake()
    {
        Events.OnChangeLives += ChangeLives; //Lisame kuulajaks
        Events.OnGetLives += GetLives;
        Events.OnSetGold += SetGold;
        Events.OnGetGold += GetGold;
        Events.OnScenarioLoaded += ScenarioLoaded;
        Events.OnWaveCompleted += WaveCompleted;
    }

    private void ScenarioLoaded(ScenarioData scenario)
    {
        // StartLevel
        levelRunning = true;        
        ScenarioData = scenario;
        Events.SetGold(scenario.Gold);
        Events.ChangeLives(scenario.Lives);
        //elud kullad wave -- get the wame from the event 
        Events.WaveStart(scenario.Waves[currentWaveIndex]);
        // TODO: suurenda wave indexit kui wave labi on!
    }

    private bool OnGetIsLost()
    {
        if (Lives <= 0)
            return true;
        return false;
    }

    private int GetGold()
    {
        return Gold;
    }

    private void SetGold(int newGold)
    {
        Gold = newGold;
    }

    private int GetLives()
    {
        // Kui keegi tahab elusid siis anname talle need siit
        return Lives;
    }

    private void ChangeLives(int newLives)
    {
        // Kui keegi muudab elusid siis paneme elud selle muutuse järgi
        Lives = newLives;
        isLost();
    }

    private void OnDestroy()
    {
        Events.OnChangeLives -= ChangeLives;
        Events.OnGetLives -= GetLives;
        Events.OnSetGold -= SetGold;
        Events.OnGetGold -= GetGold;
        Events.OnScenarioLoaded -= ScenarioLoaded;
        Events.OnWaveCompleted -= WaveCompleted;
    }

    private void WaveCompleted(bool isCompleted)
    {
        if (isCompleted)
        {
            print("waveCompleted !");
            if (ScenarioData.Waves.Length - 1 > currentWaveIndex)
            {
                // gets same wave data
                currentWaveIndex = currentWaveIndex + 1;
                print("new wave index: " + currentWaveIndex);
                print("start new wave: " + ScenarioData.Waves[currentWaveIndex]);
                Events.WaveStart(ScenarioData.Waves[currentWaveIndex]);
            }
            else
            {
                print("game end");
                HUD.Instance.ShowGameOverScreen(true);
            }
        }
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
