using UnityEngine;

public class SuperBat : Mob
{
    public override int ExpPerKill => 70;

    public override ushort MaxHealth => 60;

    public override float AttackDistance => 4;

    public override float AttackInterval => 1.8f;

    public override ushort AttackDamage { get; set; } = 2;
    public override float MovementSpeed { get; set; } = 2;
    public override ushort CurrentHealth { get; set; }
    public override byte SpaceRequired => 20;
}
