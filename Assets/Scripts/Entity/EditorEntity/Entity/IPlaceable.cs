using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceable
{
    byte SpaceRequired { get; }
    void OnConstruct();
    void OnDestruct();
}
