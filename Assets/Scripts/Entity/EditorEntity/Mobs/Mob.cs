using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : Entity, IStatProvider
{
    public abstract ushort Health { get; }

    public abstract float AttackDistance { get; }

    public abstract float AttackSpeed { get; }

    public abstract float MovementSpeed { get; }

    private void Update()
    {
        Vector2 playerPos = Player.Instance.GetPosition();
        float distSqr = Vector2.SqrMagnitude((Vector2)transform.position - playerPos);
        if(distSqr > AttackDistance * AttackDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos, MovementSpeed * Time.deltaTime);
        }
    }
}
