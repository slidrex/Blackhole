using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : Entity, IStatProvider
{
    public abstract int ExpPerKill { get; }
    public abstract ushort MaxHealth { get; }

    public abstract float AttackDistance { get; }

    public abstract float AttackInterval { get; }
    public abstract float MovementSpeed { get; }
    public ushort BaseDamage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public abstract ushort CurrentHealth { get; set; }

    private float timeToAttack;

    public void Damage(ushort damage)
    {
        print(CurrentHealth);
        if(CurrentHealth < damage)
        {
            OnDie();
        }
        else
            CurrentHealth -= damage;
    }
    public override void OnLevelRun()
    {
        CurrentHealth = MaxHealth;
    }
    private void OnDie()
    {
        Player.Instance.PlayerLevelController.AddExp(ExpPerKill);
        Destroy(gameObject);
    }

    protected override void LevelRunningUpdate()
    {
        Vector2 playerPos = Player.Instance.GetPosition();
        float distSqr = Vector2.SqrMagnitude((Vector2)transform.position - playerPos);
        if(distSqr > AttackDistance * AttackDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos, MovementSpeed * Time.deltaTime);
        }
        else if(CanAttack())
        {
            OnAttackPerform(Player.Instance);
        }
        HandleAttackInterval();
    }
    private void OnAttackPerform(Player player)
    {
        timeToAttack = 0.0f;
        OnAttack(player);
    }
    private bool CanAttack() => timeToAttack >= AttackInterval;
    private void HandleAttackInterval()
    {
        if(timeToAttack < AttackInterval)
        {
            timeToAttack += Time.deltaTime;
        }
    }
    protected abstract void OnAttack(Player player);
}
