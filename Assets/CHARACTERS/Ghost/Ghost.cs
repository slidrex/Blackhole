using UnityEngine;

public class Ghost : Mob
{
    public override int ExpPerKill => 10;

    public override ushort MaxHealth => 6;

    public override float AttackDistance => 1.5f;

    public override float AttackInterval => 2.0f;

    public override float MovementSpeed { get; set; } = 2;
    public override ushort CurrentHealth { get; set; }

    public override byte SpaceRequired => 6;

    public override ushort AttackDamage { get; set; } = 3;
}
