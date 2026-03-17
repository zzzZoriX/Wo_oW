using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponAnimator WeaponAnimator;
    [SerializeField] protected Transform projectileSpawnPoint;
    [SerializeField] protected WeaponStats stats;
    

    public virtual void Attack() {
    }

    protected virtual void OnReloadEnd()
    { }
}