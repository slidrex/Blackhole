using System.Threading.Tasks;
using UnityEngine;

public class Slime : Mob
{
    public override int ExpPerKill => 4;
    public override ushort MaxHealth => 10;

    public override float AttackDistance => 1;

    public override float AttackInterval => 3;

    public override float MovementSpeed { get; set; } = 2.5f;
    public override ushort CurrentHealth { get; set; }

    public override byte SpaceRequired => 3;

    public override ushort AttackDamage { get; set; } = 2;

    protected async override void OnAttack(Player player)
    {
        _animator.SetTrigger("Attack");
        await Task.Delay(300);
        base.OnAttack(player);
    }
}
