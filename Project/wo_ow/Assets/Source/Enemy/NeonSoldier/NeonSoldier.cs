using System;
using UnityEngine;

public class NeonSoldier : Enemy {
    [SerializeField] private Weapon _weapon;
    
    private void Start()
    {
        Settings = DeserializeData.Deserialize<EnemySettings>("./Assets/Source/Data/NeonSoldierData.json");
        Health.HP = Settings.HP;
    }

    private void Update()
    {
        enemyControls.MoveToTarget(Settings.MoveSpeed);
    }

    private void Attack() {
        if (!attackZone.SomeoneInAttackRange)
            return;

        var player = attackZone.FindObjectInZone("Player");
        if (player is null)
            return;
        
        
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