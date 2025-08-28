using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private bool alreadyAppliedDealing;
    
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("공격 상태로 전환 시도");
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }
}
