using UnityEngine;

public class Bat : Mob
{
    public override int ExpPerKill => 5;

    public override ushort MaxHealth => 15;

    public override float AttackDistance => 4;

    public override float AttackInterval => 3;

    public override ushort AttackDamage { get; set; } = 0;
    public override float MovementSpeed { get; set; } = 2;
    public override ushort CurrentHealth { get; set; }

    public override byte SpaceRequired => 4;
    [SerializeField] private Projectile projectile;
    protected override void OnAttack(Player player)
    {
        Projectile proj = Instantiate(projectile, transform.position, Quaternion.identity);
        Vector2 dir = player.GetPosition() - new Vector2(transform.position.x, transform.position.y);
        proj.Direct(dir);
    }
}
