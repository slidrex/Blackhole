using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelInfo
{
    public Entity[] Entities { get; private set; }
    public void DestroyAllEntities()
    {
        if (Entities != null)
            foreach (var entity in Entities)
            {
                MonoBehaviour.Destroy(entity.gameObject);
            }
        UpdateMapInfo();
    }
    public void UpdateMapInfo()
    {
        Entities = Object.FindObjectsOfType<Entity>();
    }
    public void UnloadObject(Entity entity)
    {
        Entities.ToList().Remove(entity);
    }
    public void LoadObject(Entity entity)
    {
        Entities.ToList().Add(entity);
    }
}
