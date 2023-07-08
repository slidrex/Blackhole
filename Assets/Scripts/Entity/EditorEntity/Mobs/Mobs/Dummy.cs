using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Mob
{
    public override byte SpaceRequired => 2;

    public override ushort Health => 100;

    public override float AttackDistance => 1;

    public override float AttackSpeed => 1;

    public override float MovementSpeed => 1;
}
