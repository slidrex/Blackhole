using System.Threading.Tasks;
using UnityEngine;

public class SuperGhost : Mob
{
    public override int ExpPerKill => 25;

    public override ushort MaxHealth => 50;

    public override float AttackDistance => 1.5f;

    public override float AttackInterval => 4;

    public override float MovementSpeed { get; set; } = 1.5f;
    public override ushort CurrentHealth { get; set; }

    public override byte SpaceRequired => 15;

    public override ushort AttackDamage { get; set; } = 2;
    [SerializeField] private Projectile projectile;
    protected override void OnAttack(Player player)
    {
        base.OnAttack(player);
        Vector2 dir = player.GetPosition() - new Vector2(transform.position.x, transform.position.y);
        Projectile proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.Direct(dir);
    }
}
