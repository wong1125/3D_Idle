using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "SO/Stat")]
public class StatData : ScriptableObject
{
    [field: SerializeField] public float MaxHealth { get; private set; } = 100f;
    [field: SerializeField] public float AttackPower { get; private set; } = 10f;
    [field: SerializeField] public float SearchRange { get; private set; } = 10f;
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;
    [field: SerializeField] public float RotateSpeed { get; private set; } = 5f;
    [field: SerializeField] public LayerMask targetMask { get; private set; }
}
