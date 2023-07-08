using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRunner : MonoBehaviour
{
    public Action OnLevelRun;
    public Action OnLevelEnd;
    public void RunLevel()
    {
        OnLevelRun.Invoke();
    }
    public void StopLevel()
    {
        OnLevelEnd.Invoke();
    }
}
