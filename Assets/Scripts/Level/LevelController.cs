using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public LevelRunner Runner;
    public LevelInfo LevelInfo;
    public LevelInteractController InteractController;
    public static LevelController Instance;
    public bool IsRunning { get; private set; }
    private void Awake()
    {
        LevelInfo = new LevelInfo();
        Runner.OnLevelRun += OnLevelStart;
        Instance = this;
    }
    private void OnDestroy()
    {
        Runner.OnLevelRun -= OnLevelStart;
    }
    private void OnLevelStart(bool isRunning)
    {
        IsRunning = isRunning;
        LevelInfo.UpdateMapInfo();
    }
}
