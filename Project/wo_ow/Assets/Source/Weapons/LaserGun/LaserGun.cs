using System;
using UnityEngine;

public class LaserGun : Weapon
{
    private LaserGunStats _lazerGunStats;
    private WeaponAnimator _laserGunAnimator;


    private void Start()
    {
        _lazerGunStats = DeserializeData.Deserialize<LaserGunStats>("./Assets/Source/Data/LaserGunData.json");
        Temperature.TempStats = new TemperatureStats(
            _lazerGunStats.MaxHeatValue,
            _lazerGunStats.HeatPerAbility,
            _lazerGunStats.HeatPerShot
        );

        _laserGunAnimator = GetSpecificChildren("LaserGunTexture").GetComponent<WeaponAnimator>();
    }

    private void Update()
    {
        Shoot();
        Ability();
    }

    protected override void Shoot()
    {
        if (!Input.GetKeyDown(Stats.shootKey))
            return;
        
        var projectile = Bullet.InstanceBullet(
            ProjectileSpawnPoint.position,
            Stats.projectile,
            transform.rotation
        );

        projectile.GetComponent<Bullet>().Damage = Convert.ToSingle(Math.Round(
            _lazerGunStats.ShotDamage * Stats.stability, 1
        ));
        
        projectile.GetComponent<Bullet>().Shoot(
            Vector3.forward * _lazerGunStats.ProjectileSpeed,
            Stats.destroyTime
        );
        
        Temperature.Heat("Shot");
        Stability.ChangeStability(_lazerGunStats.ShotStabilityDecreaseFactor);
        Temperature.StartCoolingCooldown();
        
        _laserGunAnimator.SetShotParameter();
    }

    protected override void Ability()
    {
        if (Stats.AbilityReady && Input.GetKeyUp(Stats.AbilityKey))
        {
            var abilityProjectile = Bullet.InstanceBullet(
                ProjectileSpawnPoint.position,
                Stats.abilityProjectile,
                transform.rotation
            );

            abilityProjectile.GetComponent<Bullet>().Damage = Convert.ToSingle(Math.Round(
                _lazerGunStats.AbilityDamage * Stats.stability, 1
            ));

            abilityProjectile.GetComponent<Bullet>().Damage = _lazerGunStats.AbilityDamage;
            abilityProjectile.GetComponent<Bullet>().Shoot(
                Vector3.forward * _lazerGunStats.ProjectileSpeed,
                Stats.AbilityProjectileLifeTime
            );

            Stats.AbilityReady = false;
            Stats.AbilityReload = true;
            
            Temperature.Heat("Ability");
            Stability.ChangeStability(_lazerGunStats.AbilityStabilityDecreaseFactor);
            Temperature.StartCoolingCooldown();
            
            _laserGunAnimator.SetAbilityUseParameter();
            _laserGunAnimator.SetAbilityReadyParameter(false);
        }
        else if (Stats.AbilityReload)
        {
            if (Stats.AbilityReloadTimer >= _lazerGunStats.AbilityReloadTime)
            {
                Stats.AbilityReloadTimer = 0f;
                Stats.AbilityReload = false;
            }
            else
            {
                Stats.AbilityReloadTimer += Time.deltaTime;
            }
        }
        
        if (Input.GetKey(Stats.AbilityKey) && !Stats.AbilityReload)
        {
            if (Stats.AbilityHoldTimer >= _lazerGunStats.AbilityHoldTime)
            {
                Stats.AbilityHoldTimer = _lazerGunStats.AbilityHoldTime;
                Stats.AbilityReady = true;
                
                _laserGunAnimator.SetAbilityReadyParameter();
            }
            else
            {
                Stats.AbilityHoldTimer += Time.deltaTime;
            }
        }
        else
        {
            if (Stats.AbilityHoldTimer > 0)
            {
                Stats.AbilityHoldTimer -= Time.deltaTime;
            }
            else
            {
                Stats.AbilityHoldTimer = 0f;
            }
        }
    }
}