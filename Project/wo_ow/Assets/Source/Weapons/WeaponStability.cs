using UnityEngine;

public class WeaponStability : MonoBehaviour
{
    [SerializeField] private WeaponStats _stats;
    
    public void ChangeStability(float stabilityDecreaseFactor)
    {
        _stats.stability -= stabilityDecreaseFactor;
        if (_stats.stability <= _stats.minStabilityValue)
            _stats.stability = _stats.minStabilityValue;
    }
}