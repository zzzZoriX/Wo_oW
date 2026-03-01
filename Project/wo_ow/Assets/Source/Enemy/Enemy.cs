using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyControls EnemyControls;
    public EnemyStats Stats;
    public bool IsAlive;

    public void TakeDamage(float damage)
    {
        Stats.HP -= damage;
        
        if(Stats.HP <= 0) 
            Die();
    }

    private void Die() {
        IsAlive = false;
        gameObject.SetActive(false);
    }
}