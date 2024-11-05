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
    


    private void Awake()
    {
        Instance = this;
        PanelModal.SetActive(false);
        Events.OnChangeLives += OnChangeLives;
        Events.OnSetGold += SetGold;

        Events.OnScenarioLoaded += ScenarioLoaded;
        
    }
    private void Start()
    {
        
    }

    private void ScenarioLoaded(ScenarioData obj)
    {


        foreach (Transform child in Panel.transform)
        {
            if (child != null)
            {
                Destroy(child.gameObject);
            }
        }
        
        // ScenarioData järgi lisab towerid
        foreach (var tower in obj.Towers)
        {
            var card = Instantiate<TowerCard>(TowerCardPrefab, Panel.transform);
            card.SetData(tower);
        }
    }

    private void OnDestroy()
    {
        Events.OnChangeLives -= OnChangeLives;
        Events.OnSetGold -= SetGold;
        Events.OnScenarioLoaded -= ScenarioLoaded;
    }

    private void SetGold(int newGold)
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
        // PanelModal.SetActive(true);
        // ExitToMenuButton.SetActive(true);
        // // ExitToMenuButton.GetComponent<Text>().text = "Play again";
        // if (isWon)
        // {
        //     ModalText.text = "You won";
        // }
        // else
        // {
        //     ModalText.text = "You lost";
        // }
        // TODO: lihtsam on vist uut scenei kasutada võiduks ja kaotamiseks?
        // sest mul on siuke tunne et midagi laheb katki kui seda mitte teha
        // aeg laheb edasi jms
        SceneManager.LoadScene(3);
    }
}
