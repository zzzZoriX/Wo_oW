using UnityEngine;

public class Temperature : MonoBehaviour
{
    [SerializeField] private WeaponStats _stats;
    [SerializeField] private Timer _coolingTimer;
    private float CoolingFactor { get; set; }

    public void Heat(string atttackType)
    {
        
    }

    public void StartCoolingCooldown()
    {
    }

    private void OnCoolingCooldownEnd()
    {
        var coolingTime = 
    }

    private void Cooling()
    {
        if (_stats.currentHeatValue > 0)
        {
            _stats.currentHeatValue -= CoolingFactor * Time.deltaTime;
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