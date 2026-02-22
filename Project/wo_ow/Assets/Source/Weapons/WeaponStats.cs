using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [Header("Const")]
    public GameObject camera;
    public int destroyTime = 2;
    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode AbilityKey = KeyCode.Mouse1;
    
    [Header("heating/cooling")]
    public float coolingCooldown = 1f;
    public float currentHeatValue = 0f;
    public float maxCoolingTime = 10f;
    public float stability = 1f;
    public float minStabilityValue = 0.3f;

    [Header("Ability")] 
    public float AbilityHoldTimer = 0f;
    public float AbilityReloadTimer = 0f;
    public bool AbilityReload = false;
    public bool AbilityReady = false;
    public float AbilityProjectileLifeTime = 4f;
    
    [Header("PreFabs")]
    public GameObject projectile;
    public GameObject abilityProjectile;
}
