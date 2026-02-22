using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Temperature Temperature;
    public WeaponStats Stats;
    public WeaponStability Stability;
    
    protected virtual void Shoot()
    { }

    protected virtual void Ability()
    { }
}