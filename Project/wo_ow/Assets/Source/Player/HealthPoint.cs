using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    internal float HP;

    public void Decrease(float damage)
    {
        HP -= damage;

        if (HP < 0)
            HP = 0;
    }
    
// TODO: heal hp
}