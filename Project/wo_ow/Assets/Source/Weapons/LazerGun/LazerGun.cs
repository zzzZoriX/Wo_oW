using System;
using UnityEngine;

public class LazerGun : MonoBehaviour
{
    [SerializeField] private WeaponStats _stats;
    private Timer _cooldownRemaining;
    private Timer _coolingTimer;
    private float _coolingFactor = 0f;

    private void Start()
    {
        _cooldownRemaining = gameObject.AddComponent<Timer>();
        _coolingTimer = gameObject.AddComponent<Timer>();
        
        _cooldownRemaining.DoWhile = false;
    }

    private void Update()
    {
        Shoot();
        Ability();
    }

    private void Shoot()
    {
        if(!Input.GetKeyDown(_stats.shootKey))
            return;
        
        var lazer = Instantiate(
            _stats.projectile,
            transform.position,
            transform.rotation
        );
        

        lazer.GetComponent<Bullet>().Shoot(Vector3.forward * _stats.speed, _stats.destroyTime);
        
        Destroy(lazer, _stats.destroyTime);

        Heat("Shot");
        StartCoolingCooldown();
    }

    private void Ability()
    {
        if (_stats.AbilityReady)
        {
            if (Input.GetKeyUp(_stats.AbilityKey))
            {
                var lazer = Instantiate(
                    _stats.abilityProjectile,
                    transform.position,
                    transform.rotation
                );

                lazer.GetComponent<Bullet>().Shoot(Vector3.forward * _stats.speed, _stats.destroyTime);
                
                Destroy(lazer, _stats.AbilityProjectileLifeTime);
                
                _stats.AbilityReady = false;
                _stats.AbilityReload = true;
                
                Heat("Ability");
                StartCoolingCooldown();
            }

            return;
        }

        if (_stats.AbilityReload)
        {
            _stats.AbilityReloadTimer += Time.deltaTime;
            
            if (_stats.AbilityReloadTimer >= _stats.AbilityReloadTime)
            {
                _stats.AbilityReloadTimer = 0f;
                _stats.AbilityReload = false;
            }
            else
            {
                _stats.AbilityReloadTimer += Time.deltaTime;
            }

            return;
        }


        if (!Input.GetKey(_stats.AbilityKey))
        {
            if (_stats.AbilityHoldTimer > 0)
            {
                _stats.AbilityHoldTimer -= Time.deltaTime;
            }
            else
            {
                _stats.AbilityHoldTimer = 0f;
            }

            return;
        }

        if (_stats.AbilityHoldTimer >= _stats.AbilityHoldTime)
        {
            _stats.AbilityHoldTimer = 0f;

            _stats.AbilityReady = true;
        }
        else
        {
            _stats.AbilityHoldTimer += Time.deltaTime;
        }
    }

    private void Heat(string attackType)
    {
        var addingHeatValue = 0f;

        switch (attackType)
        {
            case "Shot":
                addingHeatValue = _stats.heatPerShot;
                break;
            case "Ability":
                addingHeatValue = _stats.heatPerAbility;
                break;
        }

        _stats.currentHeatValue += addingHeatValue;
        if (_stats.currentHeatValue > _stats.maxHeatValue)
            _stats.currentHeatValue = _stats.maxHeatValue;
    }

    private void StartCoolingCooldown()
    {
        if (_cooldownRemaining is null) return;
        
        _cooldownRemaining?.Set(_stats.coolingCooldown);
        _cooldownRemaining.Action += OnCooldownEnd;
        
        _cooldownRemaining?.Run();
    }

    private void OnCooldownEnd()
    {
        var coolingTime = GetCoolingTime();
        if (coolingTime <= 0)
            return;
        
        _coolingFactor = _stats.currentHeatValue / coolingTime;
        
        _coolingTimer.DoWhile = true;
        _coolingTimer.Action += Cooling;
        
        _coolingTimer.Set(_stats.maxCoolingTime);
        _coolingTimer.Run();
    }

    private void Cooling()
    {
        if (_stats.currentHeatValue > 0)
        {
            _stats.currentHeatValue -= _coolingFactor * Time.deltaTime;
        }
        else
        {
            _stats.currentHeatValue = 0f;
            _coolingTimer.Action -= Cooling;
        }
    }

    private float GetCoolingTime()
    {
        return _stats.currentHeatValue * (_stats.maxCoolingTime / _stats.maxHeatValue);
    }

//  drawing aim ray
    private void OnDrawGizmos()
    {
        Debug.DrawRay(_stats.camera.transform.position, _stats.camera.transform.forward, Color.red);
    }
}