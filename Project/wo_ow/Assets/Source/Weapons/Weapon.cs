using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Temperature Temperature;
    public WeaponStability Stability;
    public WeaponAnimator WeaponAnimator;
    [SerializeField] protected Transform projectileSpawnPoint;
    [SerializeField] protected WeaponStats stats;
    

    public virtual void Attack() {
    }

    protected virtual void OnReloadEnd()
    { }
}