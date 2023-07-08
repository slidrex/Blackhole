using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaEffectObject : MonoBehaviour
{
    public float PushCooldown;
    private float _timeSincePush;
    [SerializeField] protected float _effectRadius;
    private bool isRunning;
    protected void BeginTimer()
    {
        _timeSincePush = 0.0f;
        isRunning = true;
    }
    protected void EndTimer()
    {
        isRunning = false;
    }
    private void Update()
    {
        if (isRunning)
        {
            if (_timeSincePush > 0) _timeSincePush += Time.deltaTime;
            else
            {
                bool found = CheckObjectsInside(Physics2D.OverlapCircleAll(transform.position, _effectRadius));
                if (found) _timeSincePush = PushCooldown;
            }
        }
    }
    /// <returns>True if you want to reset timer</returns>
    protected virtual bool CheckObjectsInside(Collider2D[] objects)
    {
        return true;
    }
}
