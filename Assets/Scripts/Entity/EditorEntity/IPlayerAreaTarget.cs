using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerAreaTarget
{
    Action<Vector2> OnPlayerPull { get; set; }
}
