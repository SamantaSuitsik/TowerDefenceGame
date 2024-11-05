using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameover : MonoBehaviour
{
    public Button ExitGame;
    public Button GoToMenu;
    void Awake()
    {
        ExitGame.onClick.AddListener(ExitGamePressed);
        GoToMenu.onClick.AddListener(ExitToMenuPressed);
    }
    public void ExitToMenuPressed()
    {
        print("go to menu pressed");
        SceneManager.LoadScene(0);
        if (Menu.Instance != null)
        {
            Menu.Instance.gameObject.SetActive(true);
        }
    }

    public void ExitGamePressed()
    {
        print("quit pressed!");
        Application.Quit();
    }
}

