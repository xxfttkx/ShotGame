using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static event Action<Vector3> CreateEnemy;
    public static void CallCreateEnemy(Vector3 pos)
    {
        CreateEnemy?.Invoke(pos);
    }

    // public static event Action<GameObject, int> HurtEnemy;
    // public static void CallHurtEnemy(GameObject obj, int damage)
    // {
    //     HurtEnemy?.Invoke(obj, damage);
    // }

    public static event Action<GameObject> KillEnemy;
    public static void CallKillEnemy(GameObject obj)
    {
        KillEnemy?.Invoke(obj);
    }

    public static event Action<Vector3, Vector2> CreateBullet;
    public static void CallCreateBullet(Vector3 pos, Vector2 force)
    {
        CreateBullet?.Invoke(pos, force);
    }

    public static event Action<GameObject> ReleaseBullet;
    public static void CallReleaseBullet(GameObject obj)
    {
        ReleaseBullet?.Invoke(obj);
    }

    public static event Action<Vector3> CreateDeadEnemy;
    public static void CallCreateDeadEnemy(Vector3 pos)
    {
        CreateDeadEnemy?.Invoke(pos);
    }

    public static event Action StartNewGame;
    public static void CallStartNewGame()
    {
        StartNewGame?.Invoke();
    }

    public static event Action EndGame;
    public static void CallEndGame()
    {
        EndGame?.Invoke();
    }

    public static event Action UpdateRecordPanelUI;
    public static void CallUpdateRecordPanelUI()
    {
        UpdateRecordPanelUI?.Invoke();
    }
    public static event Action<Vector3, int> HurtPlayer;
    public static void CallHurtPlayer(Vector3 enemyPos, int damage)
    {
        HurtPlayer?.Invoke(enemyPos, damage);
    }

    /// <summary>
    /// 爆炸特效
    /// </summary>
    public static event Action<Vector3> CreateBoom;
    public static void CallCreateBoom(Vector3 pos)
    {
        CreateBoom?.Invoke(pos);
    }

    /// <summary>
    /// 枪口特效
    /// </summary>
    public static event Action<Vector3> CreateGunFire;
    public static void CallCreateGunFire(Vector3 pos)
    {
        CreateGunFire?.Invoke(pos);
    }

    /// <summary>
    /// 声音特效
    /// </summary>
    public static event Action<SoundDetails> CreateSound;
    public static void CallCreateSound(SoundDetails soundDetails)
    {
        CreateSound?.Invoke(soundDetails);
    }
}
