using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    private WeaponAnimatorParameters _parameters;

    public void SetShotParameter()
        => _parameters.IsShot = !_parameters.IsShot;

    public void SetAbilityReadyParameter()
        => _parameters.AbilityReady = !_parameters.AbilityReady;

    public void SetAbilityUseParameter()
        => _parameters.AbilityUse = !_parameters.AbilityUse;

    public void SetShotParameter(bool status)
        => _parameters.IsShot = status;

    public void SetAbilityReadyParameter(bool status)
        => _parameters.AbilityReady = status;

    public void SetAbilityUseParameter(bool status)
        => _parameters.AbilityUse = status;
}