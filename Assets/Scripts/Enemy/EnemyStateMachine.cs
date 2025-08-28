using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }

    public Transform targetPlayer { get; private set; }

    public EnemyIdleState IdleState { get; private set; }
    public EnemyMoveState MoveState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }

    public EnemyStateMachine(Enemy enemy)
    {
        this.Enemy = enemy;

        IdleState = new EnemyIdleState(this);
        MoveState = new EnemyMoveState(this);
        AttackState = new EnemyAttackState(this);
    }
    
    public void SetTargetPlayer(Transform transform)
    {
        targetPlayer = transform;
        if (targetPlayer != null)
        {
            var health = targetPlayer.gameObject.GetComponentInParent<Health>();
            if (health != null)
            {
                health.OnDie += ResetTarget;
            }
            else
            {
                Debug.LogWarning("Å¸°Ù¿¡ Health ÄÄÆ÷³ÍÆ®°¡ ¾øÀ½");
            }
        }
    }

    void ResetTarget()
    {
        ChangeState(IdleState);
        targetPlayer.gameObject.GetComponentInParent<Health>().OnDie -= ResetTarget;
    }

}
