using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Temperature Temperature;
    public WeaponStats Stats;
    public WeaponStability Stability;
    public Transform ProjectileSpawnPoint;

    public virtual void Shoot() {
        if (!Input.GetKeyDown(Stats.shootKey) || !Stats.canAttack)
            return;
        
        var projectile = Bullet.InstanceBullet(
            ProjectileSpawnPoint.position,
            Stats.projectile,
            transform.rotation
        );

        projectile.GetComponent<Bullet>().Damage = Convert.ToSingle(Math.Round(
            Stats.ShotDamage * Stats.stability, 1
        ));
        
        projectile.GetComponent<Bullet>().Shoot(
            Vector3.forward * Stats.ProjectileSpeed,
            Stats.destroyTime
        );
    }

    protected virtual void OnReloadEnd()
    { }

    public GameObject GetSpecificChildren(string name)
    {
        Transform[] childrens = GetComponentsInChildren<Transform>();

        foreach (var children in childrens)
        {
            if (children.gameObject.name == name)
                return children.gameObject;
        }

        return null;
    }
}