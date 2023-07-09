using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedFountain : AreaEffectObject, IPlayerAreaTarget
{
    [SerializeField] private float amplificationTime;
    [SerializeField] private float speedAmplificationMultiplier;
    private bool speedAplified => Player.Instance.SpeedAmplification != 0;
    public Action<Vector2> OnPlayerPull { get; set; }
    protected override void Update()
    {
        base.Update();

        if (LevelController.Instance.IsRunning && IsReady && !speedAplified)
        {
            OnPlayerPull.Invoke(transform.position);
        }
    }
    protected override bool CheckObjectsInside(Collider2D[] objects)
    {
        foreach (var obj in objects)
        {
            if (obj.TryGetComponent<PlayerTransform>(out var player))
            {
                Player.Instance.SpeedAmplification += speedAmplificationMultiplier;
                SoundController.Instance.PlayFountain();
                StartCoroutine(UnamplifySpeed());
                return true;
            }
        }
        return false;
    }
    private IEnumerator UnamplifySpeed()
    {
        yield return new WaitForSeconds(amplificationTime);
        Player.Instance.SpeedAmplification = 0;
    }
}
