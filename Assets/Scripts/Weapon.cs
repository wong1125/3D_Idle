using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool isEnemy;
    
    private Collider parentCollider;
    private int damage;

    private List<Collider> hittedColliders = new List<Collider>();

    private void Awake()
    {
        if (this.CompareTag("Enemy"))
            isEnemy = true;
        else
            isEnemy = false;

        //플레이어는 Weapon이 자식에, 적은 자신에게 컴포넌트가 붙음
        if (isEnemy)
        {
            parentCollider = GetComponent<Collider>();
            damage = (int)GetComponent<Enemy>().EnemyStat.AttackPower;
        }
        else
        {
            parentCollider = GetComponentInParent<Collider>();
            damage = (int)GetComponentInParent<Player>().PlayerStat.AttackPower;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other == parentCollider) return;
        if (hittedColliders.Contains(other)) return;

        hittedColliders.Add(other);

        if (other.TryGetComponent(out Health health))
        {
            health.ChangeHealth(-damage);
        }

    }

    public void ResethittedColliders()
    {
        hittedColliders.Clear();
    }
}
