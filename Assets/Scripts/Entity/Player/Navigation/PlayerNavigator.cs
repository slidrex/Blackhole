using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerNavigator
{
    public enum MoveType
    {
        NONE,
        TARGET_AREA,
        CHASE
    }
    private PlayerNavigatorModel navigatorModel;
    private Player player;
    public MoveType MoveState { get; private set; }
    private Queue<Vector2> activeCallings;
    private Vector2 _callPosition;
    private Transform _target;
    public PlayerNavigator(Player player)
    {
        this.player = player;
        activeCallings = new Queue<Vector2>();
    }
    public void Configure()
    {
        navigatorModel = new PlayerNavigatorModel(OnPlayerCall);
        LevelController.Instance.Runner.OnLevelRun += MoveToNextCall;
    }
    ~PlayerNavigator()
    {
        LevelController.Instance.Runner.OnLevelRun -= MoveToNextCall;
    }
    public void Update()
    {
        if (MoveState == MoveType.TARGET_AREA) PlayerCallMoveUpdate();
        else if (MoveState == MoveType.CHASE) PlayerChaseMoveUpdate();
    }
    private void OnPlayerCall(Vector2 callPosition)
    {
        activeCallings.Enqueue(callPosition);
        if (MoveState != MoveType.TARGET_AREA) MoveToNextCall();
    }
    
    
    private void MoveToNextCall()
    {
        if(activeCallings.Count > 0)
        {
            MoveState = MoveType.TARGET_AREA;
            _callPosition = activeCallings.Dequeue();
        }
        else if(navigatorModel.TryGetNearestMob(player.GetPosition(), out var mob))
        {
            _target = mob.transform;
            MoveState = MoveType.CHASE;
        }
        else MoveState = MoveType.NONE;
    }
    private void PlayerCallMoveUpdate()
    {
        if (!player.IsPlayerInPosition(_callPosition))
        {
            player.Move(_callPosition);
        }
        else MoveToNextCall();
    }
    private void PlayerChaseMoveUpdate()
    {
        if (_target == null) MoveToNextCall();
        if(Vector2.SqrMagnitude((Vector2)_target.transform.position - player.GetPosition()) >= player.AttackDistance * player.AttackDistance)
        {
            player.Move(_target.position);
        }
    }
}
