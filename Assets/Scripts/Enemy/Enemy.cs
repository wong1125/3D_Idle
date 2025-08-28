using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public CharacterController controller { get; private set; }

    public Animator Animator { get; private set; }
    [field: SerializeField] public StatData EnemyStat { get; private set; }


}
