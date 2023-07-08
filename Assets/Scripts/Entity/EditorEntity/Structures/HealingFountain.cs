using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Entity, IPlayerAreaTarget
{
    public Action<Vector2> OnPlayerPull { get; set; }

    public override byte SpaceRequired => 5;

    private float time;
    private void Awake()
    {
        time = UnityEngine.Random.Range(10, 40);
    }
    private void Update()
    {
        if (!LevelController.Instance.IsRunning) return;
        if (time <= 0)
        {
            time = UnityEngine.Random.Range(10, 40);
            OnPlayerPull.Invoke(transform.position);
        }
        else time -= Time.deltaTime;

    }
}
