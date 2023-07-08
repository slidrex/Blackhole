using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public LevelRunner Runner;
    public LevelInfo LevelInfo;
    public static LevelController Instance;
    public bool IsRunning { get; private set; }
    private void Awake()
    {
        Runner.OnLevelRun += OnLevelStart;
        Instance = this;
    }
    private void OnDestroy()
    {
        Runner.OnLevelRun -= OnLevelStart;
    }
    private void OnLevelStart()
    {
        LevelInfo = new LevelInfo();
        Debug.Log("Create MapInfo...");
        IsRunning = true;
        LevelInfo.UpdateMapInfo();
    }
}
