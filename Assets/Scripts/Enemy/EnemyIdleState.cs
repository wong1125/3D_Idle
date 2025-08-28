using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    Collider[] buffer = new Collider[1];
    float lastTimeChecked;

    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
        
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
        IntervalSearchPlayer(0.5f);
    }


    void IntervalSearchPlayer(float interval)
    {
        if (Time.time - lastTimeChecked > interval)
        {
            lastTimeChecked = Time.time;
            SearchNearestTarget();
        }
    }


    void SearchNearestTarget()
    {
        Transform target = null;
        int hits = Physics.OverlapSphereNonAlloc(stateMachine.Enemy.transform.position, enemyStat.SearchRange, buffer, enemyStat.targetMask.value);
        if (hits == 1)
        {
            target = buffer[0].gameObject.transform;
            stateMachine.SetTargetPlayer(target);
            stateMachine.ChangeState(stateMachine.MoveState);
        }
        else
            return;
        
    }

}
