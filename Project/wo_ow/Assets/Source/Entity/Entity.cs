using UnityEngine;

public class Entity : MonoBehaviour {
    public EntityController Controller;
    public HealthPoint Health { get; protected set; }
    public bool IsAlive { get; private set; }

    public void TakeDamage(float damage) {
        Health.Decrease(damage);
        
        if(Health.HP <= 0)
            Die();
    }

    private void Die() {
        gameObject.SetActive(false);
        IsAlive = true;
    }
}