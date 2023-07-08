using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Mob
{
    public override byte SpaceRequired => 2;

    public override ushort MaxHealth => 10;

    public override float AttackDistance => 1;

    public override float AttackInterval => 1;

    public override float MovementSpeed => 1;

    public override ushort CurrentHealth { get; set; }

    public override int ExpPerKill => 8;

    private readonly ushort damage = 1;
    protected override void OnAttack(Player player)
    {
        player.Damage(damage);
    }
}
