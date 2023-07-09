using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNavigatorModel
{

    private List<IPlayerAreaTarget> targetStructures;
    private List<Mob> targetMobs;
    private readonly Action<Vector2> onPlayerCall;
    public PlayerNavigatorModel(Action<Vector2> onPlayerCall)
    {
        this.onPlayerCall = onPlayerCall;
        targetStructures = new List<IPlayerAreaTarget>();
        targetMobs = new List<Mob>();
        LevelController.Instance.Runner.OnLevelRun += OnLevelRun;
    }
    ~PlayerNavigatorModel()
    {
        LevelController.Instance.Runner.OnLevelRun -= OnLevelRun;
    }
    private void OnLevelRun(bool isRunning)
    {
        var entities = LevelController.Instance.LevelInfo.Entities;
        Debug.Log("Find IPlayerAreaTarget...");
        foreach (var entity in entities)
        {
            if (entity.TryGetComponent<IPlayerAreaTarget>(out var component))
            {
                targetStructures.Add(component);
                component.OnPlayerPull = OnPlayerCall;
            }
            else if (entity.TryGetComponent<Mob>(out var mob))
            {
                targetMobs.Add(mob);
            }
        }
    }
    private void OnPlayerCall(Vector2 callPosition)
    {
        onPlayerCall.Invoke(callPosition);
    }
    public bool TryGetNearestMob(Vector2 playerPosition, out Mob mob)
    {
        int nearestIndex = -1;
        float minDist = 0;
        mob = null;
        for (int i = 0; i < targetMobs.Count; i++)
        {
            var _mob = targetMobs[i];
            if (_mob == null) targetMobs.RemoveAt(i);
            else
            {
                float dist = Vector2.SqrMagnitude((Vector2)_mob.transform.position - playerPosition);
                if (nearestIndex == -1 || dist < minDist)
                {
                    nearestIndex = i;
                    minDist = dist;
                }
            }
        }
        if (nearestIndex == -1) return false;
        mob = targetMobs[nearestIndex];
        return true;
    }
}
