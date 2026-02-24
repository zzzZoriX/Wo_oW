using System;
using UnityEngine;

public class LaserGun : Weapon
{
    private LaserGunStats _lazerGunStats;
    private WeaponAnimator _laserGunAnimator;
    private Timer _reloadTimer;


    private void Start()
    {
        _reloadTimer = gameObject.AddComponent<Timer>();
        _reloadTimer.Action += OnReloadEnd;
        _reloadTimer.DoWhile = false;
        
        _lazerGunStats = DeserializeData.Deserialize<LaserGunStats>("./Assets/Source/Data/LaserGunData.json");
        Temperature.TempStats = new TemperatureStats(
            _lazerGunStats.MaxHeatValue,
            _lazerGunStats.HeatPerAbility,
            _lazerGunStats.HeatPerShot
        );

        _laserGunAnimator = GetSpecificChildren("LaserGunTexture").GetComponent<WeaponAnimator>();
    }

    private void OnDestroy()
    {
        _reloadTimer.Action -= OnReloadEnd;
    }

    private void Update()
    {
        Shoot();
        Ability();
    }

    public override void Shoot()
    {
        if (!Input.GetKeyDown(Stats.shootKey) || !Stats.canAttack)
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

        Stats.canAttack = false;
        _reloadTimer.Set(_lazerGunStats.ShotReloadTime);
        _reloadTimer.Run();
        
        _laserGunAnimator.SetAbilityUseParameter(false);
    }

    public override void Ability()
    {
        if (!Stats.canAttack)
            return;
        
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

            Stats.canAttack = false;
            _reloadTimer.Set(_lazerGunStats.AbilityAttackReloadTime);
            _reloadTimer.Run();
            
            _laserGunAnimator.SetShotParameter(false);
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

    protected override void OnReloadEnd()
        => Stats.canAttack = true;
}