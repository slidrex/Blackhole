using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo
{
    public Entity[] Entities { get; private set; }
    public void UpdateMapInfo()
    {
        Entities = Object.FindObjectsOfType<Entity>();
    }
}
