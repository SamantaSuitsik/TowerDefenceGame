using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    public TextMeshProUGUI Title;
    private void Start()
    {   
        //TODO: leiab alati et elud on 0-is
        
        print("lives: " + Events.GetLives());
        if (Events.GetLives() > 0)
        {
            Title.text = "You won!";
            return;
        }

        Title.text = "You lost!";
    }

    public void ExitToMenuPressed()
    {
        SceneManager.LoadScene(0);
        if (Menu.Instance != null)
        {
            Menu.Instance.gameObject.SetActive(true);
        }
    }
}

