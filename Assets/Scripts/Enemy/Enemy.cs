using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public CharacterController controller { get; private set; }

    public Animator Animator { get; private set; }
    [field: SerializeField] public StatData EnemyStat { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public AnimationData AnimationData { get; private set; }

    private Health health;
    private Weapon weapon;

    private EnemyStateMachine stateMachine;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        AnimationData.Initialize();
        health = GetComponent<Health>();
        weapon = GetComponentInChildren<Weapon>();

        stateMachine = new EnemyStateMachine(this);
    }

    void Start()
    {
        health.OnDie += OnDie;
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    void Update()
    {
        stateMachine.Update();
    }

    public void WeaponReset()
    {
        weapon.ResethittedColliders();
    }



    public void OnDie()
    {
        Animator.SetTrigger("Die");
        gameObject.layer = 2;
        StartCoroutine(Destroy());
        enabled = false;
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }

}
