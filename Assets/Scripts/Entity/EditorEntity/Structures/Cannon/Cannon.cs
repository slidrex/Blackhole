using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEngine;

public class Cannon : Entity
{
    public override byte SpaceRequired => 5;
    [SerializeField] private float stalkingDistance;
    private enum BuildingState
    {
        IDLE,
        ATTACK
    }
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePos;
    private BuildingState state;
    private Mob target;
    private float timeSinceShot;
    [SerializeField] private float shotInterval;
    [SerializeField] private float baseIdleChangeInterval;
    private float idleChangeInterval;
    private float timeSinceIdleChanged;
    private Vector2 stalkingDirection;
    public override void OnLevelRun()
    {
        idleChangeInterval = baseIdleChangeInterval + Random.Range(-1, 3);
        timeSinceIdleChanged = 0.0f;
        timeSinceShot = 0.0f;
    }
    protected override void LevelRunningUpdate()
    {
        if (target != null && Vector2.SqrMagnitude(target.transform.position - transform.position) > stalkingDistance * stalkingDistance) target = null;
        if (target == null)
        {
            TryGetMob(out target);
            if (state != BuildingState.IDLE)
            {
                timeSinceIdleChanged = 0.0f;
                state = BuildingState.IDLE;
            }
            OnIdleState();
        }
        else
        {
            OnAttackState();
            if (state != BuildingState.ATTACK) state = BuildingState.ATTACK;
        }

        if (timeSinceShot < shotInterval)
        {
            timeSinceShot += Time.deltaTime;
        }
        else
        {
            if(target != null)
            {
                Shot();
                timeSinceShot = 0.0f;
            }
        }
        LookAtStalkingDirection();
    }
    private void OnIdleState()
    {
        if (timeSinceIdleChanged < idleChangeInterval)
        {
            timeSinceIdleChanged += Time.deltaTime;
        }
        else
        {
            stalkingDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
            timeSinceIdleChanged = 0.0f;
            idleChangeInterval = baseIdleChangeInterval + Random.Range(-1, 3);
        }
    }
    private void OnAttackState()
    {
        stalkingDirection = target.transform.position - transform.position;
    }
    private void LookAtStalkingDirection()
    {
        transform.eulerAngles = (Mathf.Atan2(stalkingDirection.y, stalkingDirection.x) * Mathf.Rad2Deg - 90.0f) * Vector3.forward;
    }
    private void Shot()
    {
        var obj = Instantiate(bullet, firePos.position, Quaternion.Euler(transform.eulerAngles));
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, stalkingDistance);
    }
    public bool TryGetMob(out Mob mob)
    {
        Vector2 playerPosition = transform.position;

        var colliders = Physics2D.OverlapCircleAll(playerPosition, stalkingDistance);
        mob = null;
        foreach (var obj in colliders) if (obj.TryGetComponent(out mob)) return true;
        return false;
    }
}
