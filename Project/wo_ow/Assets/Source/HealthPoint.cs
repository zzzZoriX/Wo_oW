using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    internal float HP;
    internal float MaxHP;

    public void Initialize(float maxHP)
    {
        MaxHP = maxHP;
        HP = maxHP;
    }

    public void Decrease(float damage)
    {
        HP -= damage;

        if (HP < 0)
            HP = 0;
    }
    
    public void Heal(float healPoints)
    {
        HP += healPoints;

        if (HP > MaxHP)
            HP = MaxHP;
    }

    public float GetHealthPrecent()
        => HP / MaxHP;
}