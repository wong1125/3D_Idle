using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth { get; private set; }
    public float currentHealth { get; private set; }

    public event Action<float> OnHealthChange;
    public event Action OnDie;

    private bool isPlayer;

    private void Awake()
    {
        if (this.CompareTag("Player"))
            isPlayer = true;
        else
            isPlayer = false;

        if(isPlayer)
        {
            MaxHealth = GetComponent<Player>().PlayerStat.MaxHealth;
        }
        else
        {
            MaxHealth = GetComponent<Enemy>().EnemyStat.MaxHealth;
        }
    }

    private void Start()
    {
        currentHealth = MaxHealth;
    }

    public void ChangeHealth(float healthChange)
    {
        currentHealth = Mathf.Max(currentHealth + healthChange, 0);

        OnHealthChange(currentHealth);
        
        if (currentHealth == 0)
        {
            OnDie?.Invoke();
        }
    }

}
