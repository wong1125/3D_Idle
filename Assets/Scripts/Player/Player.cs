using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller {  get; private set; }

    public Animator Animator { get; private set; }
    [field: SerializeField] public StatData PlayerStat { get; private set; }


    [field: Header("Animations")]
    [field: SerializeField] public AnimationData AnimationData { get; private set; }

    private Health health;
    private Weapon weapon;

    private PlayerStateMachine stateMachine;

    private bool isPlayerDie;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        AnimationData.Initialize();
        health = GetComponent<Health>();
        weapon = GetComponentInChildren<Weapon>();

        stateMachine = new PlayerStateMachine(this);
    }
    void Start()
    {
        health.OnDie += OnDie;
        stateMachine.ChangeState(stateMachine.MoveState);
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
        isPlayerDie = true;
        enabled = false;
    }


}
