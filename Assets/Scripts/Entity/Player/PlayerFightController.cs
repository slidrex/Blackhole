using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFightController
{
    private Player player;
    private float timeSinceAttack;
    public PlayerFightController(Player player)
    {
        this.player = player;
    }
    public void HandleFight(Mob mob)
    {
        if (CanAttack()) Attack(mob);
    }
    public void Update()
    {
        HandleAttackCooldown();
    }
    private async void Attack(Mob mob)
    {
        timeSinceAttack = 0.0f;
        player.GetAnimator().SetTrigger("Attack");
        await Task.Delay(100);
        if(mob != null)
            mob.Damage(player.damage);
    }
    private bool CanAttack() => timeSinceAttack >= player.AttackInterval;
    private void HandleAttackCooldown()
    {
        if(timeSinceAttack < player.AttackInterval)
        {
            timeSinceAttack += Time.deltaTime;
        }
    }
}
