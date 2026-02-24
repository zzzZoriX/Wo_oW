using System;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private WeaponAnimatorParameters _parameters;

    private void Start()
    {
        _parameters = new WeaponAnimatorParameters();
    }

    public void SetShotParameter()
    {
        _parameters.IsShot = !_parameters.IsShot;
        _animator.SetBool("IsShot", _parameters.IsShot);
    }

    public void SetAbilityReadyParameter()
    {
        _parameters.AbilityReady = !_parameters.AbilityReady;
        _animator.SetBool("AbilityReady", _parameters.AbilityReady);
    }

    public void SetAbilityUseParameter()
    {
        _parameters.AbilityUse = !_parameters.AbilityUse;
        _animator.SetBool("AbilityUse", _parameters.AbilityUse);
    }

    public void SetShotParameter(bool status)
    {
        _parameters.IsShot = status;
        _animator.SetBool("IsShot", _parameters.IsShot);
    }

    public void SetAbilityReadyParameter(bool status)
    {
        _parameters.AbilityReady = status;
        _animator.SetBool("AbilityReady", _parameters.AbilityReady);
    }

    public void SetAbilityUseParameter(bool status)
    {
        _parameters.AbilityUse = status;
        _animator.SetBool("AbilityUse", _parameters.AbilityUse);
    }
}