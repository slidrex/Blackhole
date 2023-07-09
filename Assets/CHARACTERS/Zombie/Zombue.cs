using System.Threading.Tasks;
using UnityEngine;

public class Zombue : Mob
{
    public override int ExpPerKill => 10;

    public override ushort MaxHealth => 20;

    public override float AttackDistance => 1;
    public override float AttackInterval => 2;

    public override ushort AttackDamage { get; set; } = 2;
    public override float MovementSpeed { get; set; } = 1.5f;
    public override ushort CurrentHealth { get; set; }

    public override byte SpaceRequired => 4;
    protected async override void OnAttack(Player player)
    {
        _animator.SetTrigger("Attack");
        await Task.Delay(100);
        base.OnAttack(player);
    }
}
