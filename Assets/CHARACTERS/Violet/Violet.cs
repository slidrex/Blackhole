using System.Threading.Tasks;
using UnityEngine;

public class Violet : Mob
{
    public override int ExpPerKill => 10000;

    public override ushort MaxHealth => 120;

    public override float AttackDistance => 2.5f;

    public override float AttackInterval => 2;

    public override float MovementSpeed { get; set; } = 3;

    public override ushort CurrentHealth {get; set;}

    public override byte SpaceRequired => 0;

    public override ushort AttackDamage { get; set; } = 10;
    private Bossbar bossbar;
    protected override void Awake()
    {
        base.Awake();
        bossbar = FindObjectOfType<Bossbar>();
    }
    public override void OnLevelRun(bool run)
    {
        base.OnLevelRun(run);
        if (run) 
        {
            _animator.SetTrigger("Start");
        }
        bossbar.ActivateBar(run);
            
    }
    protected override void OnDie()
    {
        bossbar.ActivateBar(false);
        base.OnDie();
    }
    protected override void LevelRunningUpdate()
    {
        base.LevelRunningUpdate();
    }
    public override void Damage(ushort damage)
    {
        base.Damage(damage);
        bossbar.FillBar(CurrentHealth, MaxHealth);
    }
    protected override void OnAttack(Player player)
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            SpinAttack(player);
        }
        else
        {
            SplashAttack(player);
        }
        base.OnAttack(player);
    }
    private async void SpinAttack(Player player)
    {
        _animator.SetTrigger("SpinAttack");
        await Task.Delay(200);
    }
    private async void SplashAttack(Player player)
    {
        _animator.SetTrigger("SplashAttack");
        await Task.Delay(200);
    }
}
