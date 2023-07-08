using UnityEngine;

public class Ghost : Mob
{
    public override int ExpPerKill => 10;

    public override ushort MaxHealth => 15;

    public override float AttackDistance => 1.5f;

    public override float AttackInterval => 2.5f;

    public override float MovementSpeed { get; set; } = 2;
    public override ushort CurrentHealth { get; set; }

    public override byte SpaceRequired => 5;

    public override ushort AttackDamage { get; set; } = 3;
}