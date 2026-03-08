using System;
using UnityEngine;

public class NeonSoldier : Enemy
{
    private void Start()
    {
        Stats = DeserializeData.Deserialize<EnemyStats>("./Assets/Source/Data/NeonSoldierData.json");
        Health.HP = Stats.HP;
    }

    private void Update()
    {
        EnemyControls.MoveToTarget(Stats.MoveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            TakeDamage(other.gameObject.GetComponent<WeaponAttack>().Damage);
            Destroy(other.gameObject);
        }
    }
}