using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoisonAuraStructure : AreaEffectObject
{
    [SerializeField] private float poisonTime;
    [SerializeField, Range(0, 1f)] private float slownessPercentage;
    
    [SerializeField] private ushort poisonDamage;
    protected override bool CheckObjectsInside(Collider2D[] objects)
    {
        bool hasMobs = false;
        foreach (var obj in objects)
        {
            if (obj.TryGetComponent<Mob>(out var mob))
            {
                mob.SpeedMultiplier = -slownessPercentage;
                mob.GetComponent<SpriteRenderer>().color = Color.green;
                StartCoroutine(IntoxinMob(mob));
                hasMobs = true;
            }
        }
        if(hasMobs)
           return true;
        return false;
    }
    private IEnumerator IntoxinMob(Mob mob)
    {
        float timeSinceSlowness = 0;
        float timeSinceHit = 0;
        while (timeSinceSlowness < poisonTime)
        {
            yield return new WaitForEndOfFrame();
            timeSinceSlowness += Time.deltaTime;
            if (timeSinceHit >= poisonTime/2)
            {
                if(mob != null) mob.Damage(poisonDamage);
                timeSinceHit = 0;
            }
            else timeSinceHit += Time.deltaTime;
        }
        if(mob != null)
        {
            mob.GetComponent<SpriteRenderer>().color = mob.BaseColor;
            mob.SpeedMultiplier = 1;
        }
    }
}
