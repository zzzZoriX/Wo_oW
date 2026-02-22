using System;
using UnityEngine;

public class LazerGun : Weapon
{
    private LazerGunStats _lazerGunStats;


    private void Start()
    {
        _lazerGunStats = DeserializeData.Deserialize<LazerGunStats>("./Assets/Source/Data/LazerGunData.json");
        Temperature.TempStats = new TemperatureStats(
            _lazerGunStats.MaxHeatValue,
            _lazerGunStats.HeatPerAbility,
            _lazerGunStats.HeatPerShot
        );
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
        
        var projectile = Instantiate(
            Stats.projectile,
            transform.position,
            transform.rotation
        );

        projectile.GetComponent<Bullet>().Damage = _lazerGunStats.ShotDamage;
        projectile.GetComponent<Bullet>().Shoot(
            Vector3.forward * _lazerGunStats.ProjectileSpeed,
            Stats.destroyTime
        );
        
        Temperature.Heat("Shot");
        Stability.ChangeStability(_lazerGunStats.ShotStabilityDecreaseFactor);
        Temperature.StartCoolingCooldown();
    }

    protected override void Ability()
    {
        if (Stats.AbilityReady && Input.GetKeyUp(Stats.AbilityKey))
        {
            var abilityProjectile = Instantiate(
                Stats.abilityProjectile,
                transform.position,
                transform.rotation
            );

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
        
        if (Input.GetKey(Stats.AbilityKey))
        {
            if (Stats.AbilityHoldTimer >= _lazerGunStats.AbilityHoldTime)
            {
                Stats.AbilityHoldTimer = _lazerGunStats.AbilityHoldTime;
                Stats.AbilityReady = true;
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