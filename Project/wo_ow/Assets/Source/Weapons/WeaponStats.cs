using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [Header("Const")]
    public GameObject camera;
    public int destroyTime = 2;
    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode AbilityKey = KeyCode.Mouse1;
    public float AbilityProjectileLifeTime = 4f;
    public float AbilityReloadTime = 2f;
    public float AbilityHoldTime = 2f;
    
    [Header("heating/cooling")]
    public float coolingCooldown = 1f;
    public float maxHeatValue = 100f;
    public float heatPerShot = 10f;
    public float heatPerAbility = 40f;
    public float currentHeatValue = 0f;
    public float maxCoolingTime = 10f;
    public float stability = 1f;
    public float shotStabilityDecreaseFactor = 0.07f;
    public float abilityStabilityDecreaseFactor = 0.28f;
    public float minStabilityValue = 0.3f;

    [Header("Attack")] 
    public int count;
    public float speed;
    public float shotDamage = 3f;

    [Header("Ability")] 
    public float AbilityHoldTimer = 0f;
    public float AbilityReloadTimer = 0f;
    public bool AbilityReload = false;
    public bool AbilityReady = false;
    public float AbilityDamage = 12f;
    
    [Header("PreFabs")]
    public GameObject projectile;
    public GameObject abilityProjectile;
}
