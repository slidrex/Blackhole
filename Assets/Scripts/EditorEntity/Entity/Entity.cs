using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IPlaceable
{
    public abstract byte SpaceRequired { get; }

    public void OnConstruct()
    {

    }

    public void OnDestruct()
    {

    }
}
