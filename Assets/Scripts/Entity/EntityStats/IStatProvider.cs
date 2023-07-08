using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatProvider
{
    ushort Health { get; }
    float AttackDistance { get; }
    float AttackSpeed { get; }
    float MovementSpeed { get; }
}
