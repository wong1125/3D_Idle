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

    private Weapon weapon;

    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        AnimationData.Initialize();
        weapon = GetComponentInChildren<Weapon>();

        stateMachine = new PlayerStateMachine(this);
    }
    void Start()
    {
        
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
}
