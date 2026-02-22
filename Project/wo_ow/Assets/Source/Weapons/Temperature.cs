using System;
using UnityEngine;

public class Temperature : MonoBehaviour
{
    [SerializeField] private WeaponStats _stats;
    private Timer _coolingTimer;
    private Timer _cooldownRemaining;
    private float _coolingFactor;

    private void Start()
    {
        _coolingTimer = gameObject.AddComponent<Timer>();
        _coolingTimer.DoWhile = true;
        _coolingTimer.Set(_stats.maxCoolingTime);

        _cooldownRemaining = gameObject.AddComponent<Timer>();
        _coolingTimer.DoWhile = false;
    }

    public void Heat(string attackType)
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
        if (_stats.currentHeatValue >= _stats.maxHeatValue)
            _stats.currentHeatValue = _stats.maxHeatValue;
    }

    public void StartCoolingCooldown()
    {
        if (_cooldownRemaining is null)
            return;

        _cooldownRemaining.Action += OnCoolingCooldownEnd;
        
        _cooldownRemaining.Set(_stats.coolingCooldown / _stats.stability);
        _cooldownRemaining.Run();
    }

    private void OnCoolingCooldownEnd()
    {
        var coolingTime = GetCoolingTime();
        if (coolingTime <= 0)
            return;

        _coolingFactor = _stats.currentHeatValue / coolingTime;

        _coolingTimer.Action += Cooling;
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
            _stats.stability = 1f;
        }
    }

    private float GetCoolingTime()
    {
        return _stats.currentHeatValue * (_stats.maxCoolingTime / _stats.maxHeatValue);
    }
}