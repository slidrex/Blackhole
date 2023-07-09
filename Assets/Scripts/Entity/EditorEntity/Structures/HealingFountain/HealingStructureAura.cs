using System;
using UnityEngine;

public class HealingStructureAura : AreaEffectObject, IPlayerAreaTarget
{
    [SerializeField, Range(0, 1f)] private float healthPercentageHealCall;
    [SerializeField] private ushort _heal;
    public Action<Vector2> OnPlayerPull { get; set; }
    private bool preventCalling;
    private void Start()
    {
        Player.Instance.OnPlayerDamaged += OnPlayerDamaged;
    }
    private void OnDestroy()
    {
        Player.Instance.OnPlayerDamaged -= OnPlayerDamaged;
    }
    private void OnPlayerDamaged()
    {
        if (Player.Instance.CurrentHealth < Player.Instance.MaxHealth * healthPercentageHealCall && preventCalling == false && IsReady)
        {
            OnPlayerPull.Invoke(transform.position);
            preventCalling = true;
        }
    }
    protected override bool CheckObjectsInside(Collider2D[] objects)
    {
        foreach (var obj in objects)
        {
            if(obj.TryGetComponent<PlayerTransform>(out var player))
            {
                Player.Instance.Heal(_heal);
                preventCalling = false;
                SoundController.Instance.PlayFountain();
                return true;
            }
        }
        return false;
    }
}
