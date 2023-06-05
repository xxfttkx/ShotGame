using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemy : MonoBehaviour
{
    private void OnEnable()
    {
        EventHandler.StartNewGame += OnStartNewGame;
    }
    private void OnDisable()
    {
        EventHandler.StartNewGame -= OnStartNewGame;
    }
    void OnStartNewGame()
    {
        PoolManager.Instance.ReleaseDeadEnemy(this.gameObject);
    }
}
