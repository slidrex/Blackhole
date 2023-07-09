using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRunner : MonoBehaviour
{
    public Action<bool> OnLevelRun;
    public Action OnGameStart;
    public void RunLevel()
    {
        OnLevelRun.Invoke(true);
    }
    public void StopLevel()
    {
        OnLevelRun?.Invoke(false);
    }
}
