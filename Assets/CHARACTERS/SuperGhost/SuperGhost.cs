using UnityEngine;

public class SuperGhost : Mob
{
    public override int ExpPerKill => 25;

    public override ushort MaxHealth => 50;

    public override float AttackDistance => 1.5f;

    public override float AttackInterval => 3;

    public override float MovementSpeed { get; set; } = 1.5f;
    public override ushort CurrentHealth { get; set; }

    public override byte SpaceRequired => 15;

    public override ushort AttackDamage { get; set; } = 5;
}
