using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    // Start is called before the first frame update
    public GameState gameState;
    protected override void Awake()
    {
        base.Awake();
        gameState = GameState.GamePlay;
    }
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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameState == GameState.GamePlay)
            {
                gameState = GameState.GamePause;
                Time.timeScale = 0;
            }
            else if (gameState == GameState.GamePause)
            {
                gameState = GameState.GamePlay;
                Time.timeScale = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            EventHandler.CallStartNewGame();
        }

    }

    void OnStartNewGame()
    {
        gameState = GameState.GamePlay;
        Time.timeScale = 1;
    }
    void OnEndGame()
    {
        gameState = GameState.GameEnd;
        Time.timeScale = 0;
    }
}
