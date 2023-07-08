public class Dummy : Mob
{
    public override byte SpaceRequired => 2;

    public override ushort MaxHealth => 15;

    public override float AttackDistance => 1;

    public override float AttackInterval => 1;

    public override float MovementSpeed { get; set; } = 1;

    public override ushort CurrentHealth { get; set; }

    public override int ExpPerKill => 8;

    public override ushort AttackDamage { get; set; } = 1;
}
