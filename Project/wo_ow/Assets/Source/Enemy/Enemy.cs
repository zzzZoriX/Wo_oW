using System;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected EnemyControls enemyControls;
    [SerializeField] protected AttackZone attackZone;
    [SerializeField] private Weapon _weapon;
    protected EnemySettings Settings;


    private void Start() {
        enemyControls.RotateToTarget();
    }

    private void Update() {
        if (PauseManager.Instance.GamePaused)
            return;
        
        
        UpdateActions();
    }

    protected virtual void UpdateActions() {
        // Attack(); // now this method is useless
        enemyControls.RotateToTarget();
    }

    private void Attack() {
        if (!attackZone.SomeoneInAttackRange)
            return;

        if(attackZone.TagInAttackZone("Player"))
            return;
        
        _weapon.Attack();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            TakeDamage(other.gameObject.GetComponent<WeaponAttack>().Damage);
            Destroy(other.gameObject);
        }
    }
}