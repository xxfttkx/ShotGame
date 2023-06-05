using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : Singleton<RecordManager>
{
    public int shootBulletNum;
    public int killEnemyNum;
    public int playerHealth;
    protected override void Awake()
    {
        base.Awake();
        playerHealth = Settings.playerHealth;
    }
    private void OnEnable()
    {
        EventHandler.CreateBullet += OnCreateBullet;
        EventHandler.KillEnemy += OnKillEnemy;
        EventHandler.StartNewGame += OnStartNewGame;
        EventHandler.HurtPlayer += OnHurtPlayer;
    }
    private void OnDisable()
    {
        EventHandler.CreateBullet -= OnCreateBullet;
        EventHandler.KillEnemy -= OnKillEnemy;
        EventHandler.StartNewGame -= OnStartNewGame;
        EventHandler.HurtPlayer -= OnHurtPlayer;
    }
    void OnCreateBullet(Vector3 pos, Vector2 dir)
    {
        shootBulletNum++;
        EventHandler.CallUpdateRecordPanelUI();
    }
    void OnKillEnemy(GameObject obj)
    {
        killEnemyNum++;
        EventHandler.CallUpdateRecordPanelUI();
    }
    void OnStartNewGame()
    {
        shootBulletNum = 0;
        killEnemyNum = 0;
        playerHealth = Settings.playerHealth;
        EventHandler.CallUpdateRecordPanelUI();
    }
    void OnHurtPlayer(Vector3 enemyPos, int damage)
    {
        if (playerHealth > 0)
        {
            playerHealth -= damage;
            EventHandler.CallUpdateRecordPanelUI();
            if (playerHealth <= 0)
            {
                //TODO GameOver
            }
        }
    }
}
