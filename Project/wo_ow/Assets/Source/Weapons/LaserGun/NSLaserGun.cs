using System;
using UnityEngine;

public class NSLaserGun : Weapon {
    private GameObject _player;
    private NeonSoldierWeaponConfig _weaponConfig;
    private Timer _reloadTimer;


    private void Start() {
        _player = GameObject.Find("PlayerBase");
        
        _reloadTimer = gameObject.AddComponent<Timer>();

        _reloadTimer.DoWhile = false;
        _reloadTimer.Action += SetAttackStatus;

        _weaponConfig = DeserializeData.Deserialize<NeonSoldierWeaponConfig>("Jsons/NeonSoldierWeaponConfig");
    }

    private void OnDisable() {
        _reloadTimer.Action -= SetAttackStatus;
    }

    private void SetAttackStatus() {
        stats.canAttack = !stats.canAttack;
    }

    public override void Attack() {
        if (!stats.canAttack)
            return;

        var rotation = Quaternion.LookRotation(_player.transform.position);
        
        var projectile = Bullet.InstanceBullet(
            projectileSpawnPoint.position,
            stats.projectile,
            rotation
        );

        projectile.GetComponent<Bullet>().Damage = _weaponConfig.Damage;
        projectile.GetComponent<Bullet>().Shoot(
            Vector3.forward * stats.ProjectileSpeed,
            stats.destroyTime
        );

        SetAttackStatus();
        _reloadTimer.SetNRun(_weaponConfig.AttackSpeed);
    }
}