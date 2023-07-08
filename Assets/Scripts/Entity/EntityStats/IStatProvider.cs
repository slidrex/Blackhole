using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatProvider
{
    ushort MaxHealth { get; }
    ushort CurrentHealth { get; }
    float AttackDistance { get; }
    float AttackInterval { get; }
    float MovementSpeed { get; }
    void Damage(ushort damage);
}
