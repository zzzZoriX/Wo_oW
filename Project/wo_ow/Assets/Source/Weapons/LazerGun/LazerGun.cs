using UnityEngine;

public class LazerGun : Weapon
{
    private void Update()
    {
        Shoot();
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

        projectile.GetComponent<Bullet>().Shoot(
            Vector3.forward * Stats.speed,
            Stats.destroyTime
        );
        
        Temperature.Heat("Shot");
        Stability.ChangeStability(Stats.shotStabilityDecreaseFactor);
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

            abilityProjectile.GetComponent<Bullet>().Shoot(
                Vector3.up * Stats.speed,
                Stats.AbilityProjectileLifeTime
            );

            Stats.AbilityReady = false;
            Stats.AbilityReload = true;
            
            Temperature.Heat("Ability");
            Stability.ChangeStability(Stats.abilityStabilityDecreaseFactor);
            Temperature.StartCoolingCooldown();
        }
        else if (Stats.AbilityReload)
        {
            if (Stats.AbilityReloadTimer >= Stats.AbilityReloadTime)
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
            if (Stats.AbilityHoldTimer >= Stats.AbilityHoldTime)
            {
                Stats.AbilityHoldTimer = Stats.AbilityHoldTime;
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