using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRunner : MonoBehaviour
{
    public Action OnLevelRun;
    public void RunLevel()
    {
        OnLevelRun.Invoke();
    }
}
