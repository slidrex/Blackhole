using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Mob : Entity, IStatProvider
{
    public abstract int ExpPerKill { get; }
    public abstract ushort MaxHealth { get; }

    public abstract float AttackDistance { get; }

    public abstract float AttackInterval { get; }
    public abstract ushort AttackDamage { get; set; }
    public abstract float MovementSpeed { get; set;  }
    public abstract ushort CurrentHealth { get; set; }
    public float SpeedMultiplier;
    private float timeToAttack;
    public Color BaseColor;
    protected Animator _animator;
    private GameObject bloodParticles;
    protected override void Awake()
    {
        base.Awake();
        bloodParticles = Resources.Load<GameObject>("Blood");
        BaseColor = GetComponent<SpriteRenderer>().color;
        _animator = GetComponent<Animator>();
    }
    public void Damage(ushort damage)
    {
        GameObject temp = Instantiate(bloodParticles, transform.position, Quaternion.identity);
        Destroy(temp, 1);

        if(CurrentHealth <= damage)
        {
            OnDie();
        }
        else
            CurrentHealth -= damage;
    }
    public override void OnLevelRun(bool run)
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
        float distX = transform.position.x - playerPos.x;
        _animator.SetInteger("moveX", (int)distX);
        if (distX < 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (distX > 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        if(distSqr > AttackDistance * AttackDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos, MovementSpeed * Time.deltaTime * (1 + SpeedMultiplier));
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
    protected virtual void OnAttack(Player player)
    {
        GameObject temp = Instantiate(bloodParticles, player.GetPosition(), Quaternion.identity);
        Destroy(temp, 1);
        player.Damage(AttackDamage);
    }
}
