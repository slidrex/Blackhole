using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void Attack(Mob mob)
    {
        timeSinceAttack = 0.0f;
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
