using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaEffectObject : MonoBehaviour
{
    public float MinPushCooldown;
    public float MaxPushCooldown;
    private float _timeSincePush;
    [SerializeField] protected float _effectRadius;
    [SerializeField] private GameObject activateParticle;
    private bool isRunning;
    protected bool IsReady => _timeSincePush <= 0;
    private void Awake()
    {
        LevelController.Instance.Runner.OnLevelRun += BeginTimer;
    }
    private void OnDestroy()
    {
        LevelController.Instance.Runner.OnLevelRun -= BeginTimer;
    }
    protected void BeginTimer(bool run)
    {
        _timeSincePush = Random.Range(MinPushCooldown, MaxPushCooldown);
        isRunning = true;
    }
    protected void EndTimer()
    {
        isRunning = false;
    }
    protected virtual void Update()
    {
        if (isRunning)
        {
            if (_timeSincePush > 0) _timeSincePush -= Time.deltaTime;
            else
            {
                bool found = CheckObjectsInside(Physics2D.OverlapCircleAll(transform.position, _effectRadius));
                if (found)
                {
                    _timeSincePush = Random.Range(MinPushCooldown, MaxPushCooldown);
                    var obj = Instantiate(activateParticle, transform.position, Quaternion.identity);
                    Destroy(obj, 5.0f);
                }
            }
        }
    }
    /// <returns>True if you want to reset timer</returns>
    protected virtual bool CheckObjectsInside(Collider2D[] objects)
    {
        return true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _effectRadius);
    }
}
