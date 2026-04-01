using System;
using UnityEngine;

public class Entity : MonoBehaviour {
    public EntityController Controller;
    public HealthPoint Health { get; private set; }
    public bool IsAlive { get; private set; }


    private void Awake() {
        IsAlive = true;

        Health = gameObject.AddComponent<HealthPoint>();
    }

    protected virtual void TakeDamage(float damage) {
        Health.Decrease(damage);
        
        if(Health.HP <= 0)
            Die();
    }

    public virtual void Die() {
        gameObject.SetActive(false);
        IsAlive = false;
    }
}