using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IPlaceable
{
    public abstract byte SpaceRequired { get; }

    protected virtual void Awake()
    {
        LevelController.Instance.Runner.OnLevelRun += OnLevelRun;
    }
    private void OnDestroy()
    {
        LevelController.Instance.Runner.OnLevelRun -= OnLevelRun;
    }
    private void Update()
    {
        if (LevelController.Instance.IsRunning) LevelRunningUpdate();
    }
    protected virtual void LevelRunningUpdate()
    {

    }
    public virtual void OnLevelRun()
    {

    }

    public virtual void OnConstruct()
    {

    }

    public virtual void OnDestruct()
    {

    }
}
