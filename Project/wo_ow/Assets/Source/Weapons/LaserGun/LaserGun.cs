using System;
using UnityEngine;

public class LaserGun : PlayerWeapon
{
    private LaserGunStats _lazerGunStats;
    private Timer _reloadTimer;


    private void Start()
    {
        _reloadTimer = gameObject.AddComponent<Timer>();
        _reloadTimer.Action += OnReloadEnd;
        _reloadTimer.DoWhile = false;
        
        _lazerGunStats = DeserializeData.Deserialize<LaserGunStats>("Jsons/LaserGunData");
        Temperature.TempStats = new TemperatureStats(
            _lazerGunStats.MaxHeatValue,
            _lazerGunStats.HeatPerAbility,
            _lazerGunStats.HeatPerShot
        );
    }

    private void OnDestroy()
    {
        _reloadTimer.Action -= OnReloadEnd;
    }

    private void Update()
    {
        Shot();
        Ability();
    }

    public override void Attack()
    {
        var projectile = Bullet.InstanceBullet(
            projectileSpawnPoint.position,
            stats.projectile,
            transform.rotation
        );

        projectile.GetComponent<Bullet>().Damage = Convert.ToSingle(Math.Round(
            stats.ShotDamage * stats.stability, 1
        ));
        
        projectile.GetComponent<Bullet>().Shoot(
            Vector3.forward * stats.ProjectileSpeed,
            stats.destroyTime
        );
        
        Temperature.Heat("Shot");
        Stability.ChangeStability(_lazerGunStats.ShotStabilityDecreaseFactor);
        Temperature.StartCoolingCooldown();
        

        stats.canAttack = false;
        _reloadTimer.Set(_lazerGunStats.ShotReloadTime);
        _reloadTimer.Run();
    }

    public void Ability()
    {
        if (stats.AbilityReady && Input.GetKeyUp(stats.AbilityKey))
        {
            WeaponAnimator.SetAbilityReady(false);
            
            var abilityProjectile = Bullet.InstanceBullet(
                projectileSpawnPoint.position,
                stats.abilityProjectile,
                transform.rotation
            );

            abilityProjectile.GetComponent<Bullet>().Damage = Convert.ToSingle(Math.Round(
                stats.AbilityDamage * stats.stability, 1
            ));

            abilityProjectile.GetComponent<Bullet>().Damage = stats.AbilityDamage;
            abilityProjectile.GetComponent<Bullet>().Shoot(
                Vector3.forward * stats.ProjectileSpeed,
                stats.AbilityProjectileLifeTime
            );

            stats.AbilityReady = false;
            stats.AbilityReload = true;
            
            Temperature.Heat("Ability");
            Stability.ChangeStability(_lazerGunStats.AbilityStabilityDecreaseFactor);
            Temperature.StartCoolingCooldown();
            

            stats.canAttack = false;
            _reloadTimer.Set(_lazerGunStats.AbilityAttackReloadTime);
            _reloadTimer.Run();
        }
        else if (stats.AbilityReload)
        {
            if (stats.AbilityReloadTimer >= _lazerGunStats.AbilityReloadTime)
            {
                stats.AbilityReloadTimer = 0f;
                stats.AbilityReload = false;
            }
            else
            {
                stats.AbilityReloadTimer += Time.deltaTime;
            }
        }
        
        if (Input.GetKey(stats.AbilityKey) && !stats.AbilityReload)
        {
            if (stats.AbilityHoldTimer >= _lazerGunStats.AbilityHoldTime)
            {
                stats.AbilityHoldTimer = _lazerGunStats.AbilityHoldTime;
                stats.AbilityReady = true;
                
                WeaponAnimator.SetAbilityReady(true);
            }
            else
            {
                stats.AbilityHoldTimer += Time.deltaTime;
            }
        }
        else
        {
            if (stats.AbilityHoldTimer > 0)
            {
                stats.AbilityHoldTimer -= Time.deltaTime;
            }
            else
            {
                stats.AbilityHoldTimer = 0f;
            }
        }
    }

    private void Shot() {
        if (!Input.GetKeyDown(stats.shootKey))
            return;
        
        WeaponAnimator.SetShotTrigger();
    }
}