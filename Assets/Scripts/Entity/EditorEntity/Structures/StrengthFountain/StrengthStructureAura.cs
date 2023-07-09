using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthStructureAura : AreaEffectObject, IPlayerAreaTarget
{
    [SerializeField] private float amplificationTime;
    [SerializeField] private float damageAmplificationMultiplier;
    private bool strengthAplified => Player.Instance.DamageAmplification != 0;
    public Action<Vector2> OnPlayerPull { get; set; }
    protected override void Update()
    {
        base.Update();

        if (LevelController.Instance.IsRunning && IsReady && !strengthAplified)
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
                Player.Instance.DamageAmplification += damageAmplificationMultiplier;
                StartCoroutine(UnamplifyDamage());
                SoundController.Instance.PlayFountain();
                return true;
            }
        }
        return false;
    }
    private IEnumerator UnamplifyDamage()
    {
        yield return new WaitForSeconds(amplificationTime);
        Player.Instance.DamageAmplification = 0;
    }
}
