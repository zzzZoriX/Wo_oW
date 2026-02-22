using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Temperature Temperature;
    protected WeaponStats Stats;
    protected WeaponStability Stability;
    
    public void Shoot()
    {
        if (!Input.GetKeyDown(Stats.shootKey))
            return;

        var projectile = Instantiate(
            Stats.projectile,
            transform.position,
            transform.rotation
        );
        
        projectile.GetComponent<Bullet>().Shoot(Vector3.forward * Stats.speed, Stats.destroyTime);

        Temperature.Heat("Shot");
        Stability.ChangeStability(Stats.shotStabilityDecreaseFactor);
        Temperature.StartCoolingCooldown();
    }

    public void Ability()
    {
    }
}