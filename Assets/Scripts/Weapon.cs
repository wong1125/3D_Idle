using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool isEnemy;
    
    [SerializeField] Collider parentCollider;
    private int damage;

    private List<Collider> hittedColliders = new List<Collider>();

    private void Awake()
    {
        if (this.transform.root.CompareTag("Enemy"))
            isEnemy = true;
        else
            isEnemy = false;

        if (isEnemy)
        {
            damage = (int)GetComponentInParent<Enemy>().EnemyStat.AttackPower;
        }
        else
        {
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
