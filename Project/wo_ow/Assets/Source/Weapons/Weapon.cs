using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected Temperature Temperature;
    protected WeaponStats Stats;
    protected WeaponStability Stability;
    
    protected virtual void Shoot()
    { }

    protected virtual void Ability()
    { }
}