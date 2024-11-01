using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD Instance;

    public List<TowerData> Towers;
    public GameObject Panel;
    public TowerCard TowerCardPrefab;
    public TextMeshProUGUI LivesText;
    public TextMeshProUGUI GoldText;

    public GameObject PanelModal;
    public GameObject ExitToMenuButton;
    public TextMeshProUGUI ModalText;
    
    //TODO: bug - kui kaua oodata Level 1 vajutamisega main screenil siis
    // üks tower tulistab korraga hästi palju
    
    // praegune flow:
    // Menu -> Level 1 -> game -> surm -> main menu
    
    // TODO: Jargmisena vaja teha rohkem scenarioid ja towereid

    private void Awake()
    {
        Instance = this;
        PanelModal.SetActive(false);
        Events.OnChangeLives += OnChangeLives;
        Events.OnSetGold += OnSetGold;

        Events.OnScenarioLoaded += ScenarioLoaded;
        
    }

    private void ScenarioLoaded(ScenarioData obj)
    {
        foreach (var tower in obj.Towers)
        {
            var card = Instantiate<TowerCard>(TowerCardPrefab, Panel.transform);
        }
    }

    private void OnDestroy()
    {
        Events.OnChangeLives -= OnChangeLives;
        Events.OnSetGold -= OnSetGold;
    }

    private void OnSetGold(int newGold)
    {
        GoldText.text = "Gold: " + newGold;
    }

    private void OnChangeLives(int newLives)
    {
        LivesText.text = "Lives: " + newLives;
    }
    
    public void ExitToMenuPressed()
    {
        SceneManager.LoadScene(0);
        if (Menu.Instance != null)
        {
            Menu.Instance.gameObject.SetActive(true);
        }
    }

    public void ShowGameOverScreen(bool isWon)
    {
        PanelModal.SetActive(true);
        ExitToMenuButton.SetActive(true);
        // ExitToMenuButton.GetComponent<Text>().text = "Play again";
        if (isWon)
        {
            ModalText.text = "You won";
        }
        else
        {
            ModalText.text = "You lost";
        }

    }
}
