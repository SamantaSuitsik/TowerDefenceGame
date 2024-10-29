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
    public GameObject StartButton;
    public TextMeshProUGUI ModalText;
    
    //TODO: bug - kui kaua oodata Level 1 vajutamisega main screenil siis
    // üks tower tulistab korraga hästi palju
    
    // praegune flow:
    // Menu -> Level 1 -> game -> surm -> main menu
    
    // TODO: Jargmisena vaja teha rohkem scenarioid ja towereid

    private void Awake()
    {
        Instance = this;
        
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

    void Start()
    {
        // ModalText.text = "Welcome to TowerDefence 2000";
        // ModalText.fontSize = 22.0f;
        // PanelModal.SetActive(true);
        // ExitToMenuButton.SetActive(false);
        // Time.timeScale = 0;
        StartButton.SetActive(false);
        PanelModal.SetActive(false);
    }

    private void StartGame()
    {
        // Time.timeScale = 1;
        StartButton.SetActive(false);
        PanelModal.SetActive(false);
        
    }

    public void StartPressed()
    {
        StartGame();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
