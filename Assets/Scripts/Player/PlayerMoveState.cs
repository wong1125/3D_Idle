using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public bool IsTargetSpotted { get; private set; }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.MoveParameterHash);
        IsTargetSpotted = false;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.MoveParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (IsTargetSpotted)
        {
            Move();
            if ((stateMachine.targetEnmey.position - stateMachine.Player.transform.position).sqrMagnitude < 1)
            {
                stateMachine.ChangeState(stateMachine.AttackState);
            }
        }
        else
        {
            SearchNearestTarget();
        }
    }

    void Move()
    {
        Vector3 movementDirection = GetMovementDirection();
        Rotate(movementDirection);
        stateMachine.Player.controller.Move(movementDirection * playerStat.MoveSpeed * Time.deltaTime);
    }

    Vector3 GetMovementDirection()
    {
        Vector3 direction = (stateMachine.targetEnmey.position - stateMachine.Player.transform.position).normalized;
        return direction;
    }

    void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            stateMachine.Player.transform.rotation = Quaternion.Lerp(stateMachine.Player.transform.rotation, targetRotation, Time.deltaTime);
        }
    }

    void SearchNearestTarget()
    {
        Transform target = null;
        Collider[] hits = Physics.OverlapSphere(stateMachine.Player.transform.position, playerStat.SearchRange, playerStat.targetMask.value);
        if (hits.Length <= 0)
        {
            IsTargetSpotted = false;
            stateMachine.SetTargetEnmey(null);
            return;
        }
        float nearSqr = float.MaxValue;
        foreach (var hit in hits)
        {
            float sqr = (hit.transform.position - stateMachine.Player.transform.position).sqrMagnitude;
            if (sqr < nearSqr)
            {
                nearSqr = sqr;
                target = hit.transform;
            }
        }
        IsTargetSpotted = true;
        stateMachine.SetTargetEnmey(target);
    }

}
