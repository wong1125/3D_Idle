using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
        stateMachine.Enemy.transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("나가라고");
        StopAnimation(stateMachine.Enemy.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }
}
