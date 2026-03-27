using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected EnemyControls enemyControls;
    [SerializeField] protected AttackZone attackZone;
    [SerializeField] private Weapon _weapon;
    protected EnemySettings Settings;


    private void Update()
        => UpdateActions();

    protected virtual void UpdateActions() {
        Attack();
        enemyControls.RotateToTarget();
    }

    private void Attack() {
        if (!attackZone.SomeoneInAttackRange)
            return;

        var player = attackZone.FindObjectInZone("Player");
        if (player is null)
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