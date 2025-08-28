using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyBaseState
{
    public EnemyMoveState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.MoveParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.MoveParameterHash);
    }

    public override void Update()
    {
        base.Update();
        Move();
        if ((stateMachine.targetPlayer.position - stateMachine.Enemy.transform.position).sqrMagnitude < 1)
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }

    }

    void Move()
    {
        Vector3 movementDirection = GetMovementDirection();
        Rotate(movementDirection);
        stateMachine.Enemy.controller.Move(movementDirection * enemyStat.MoveSpeed * Time.deltaTime);
    }

    Vector3 GetMovementDirection()
    {
        if(stateMachine.targetPlayer == null)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return Vector3.zero;   
        } 
        Vector3 direction = (stateMachine.targetPlayer.position - stateMachine.Enemy.transform.position).normalized;
        return direction;
    }

    void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            stateMachine.Enemy.transform.rotation = Quaternion.Lerp(stateMachine.Enemy.transform.rotation, targetRotation, Time.deltaTime);
        }
    }

}
