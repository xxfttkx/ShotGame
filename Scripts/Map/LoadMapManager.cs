using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMapManager : Singleton<LoadMapManager>
{
    protected override void Awake()
    {
        base.Awake();
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed, 0);
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        SceneManager.LoadScene("Map1", LoadSceneMode.Additive);
    }
}
