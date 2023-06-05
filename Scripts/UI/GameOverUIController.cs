using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIController : MonoBehaviour
{
    public GameObject gameOverUI;
    private void OnEnable()
    {
        EventHandler.StartNewGame += OnStartNewGame;
        EventHandler.EndGame += OnEndGame;
    }
    private void OnDisable()
    {
        EventHandler.StartNewGame -= OnStartNewGame;
        EventHandler.EndGame -= OnEndGame;
    }

    void OnStartNewGame()
    {
        gameOverUI.SetActive(false);
    }
    void OnEndGame()
    {
        gameOverUI.SetActive(true);
    }
}
