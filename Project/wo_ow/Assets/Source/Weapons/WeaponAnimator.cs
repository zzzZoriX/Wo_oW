using System;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Weapon weapon;


    public void ShotEventHandler() {
        weapon.Attack();
    }

    public void SetShotTrigger()
        => animator.SetTrigger("Shot");

    public void SetAbilityReady(bool value)
        => animator.SetBool("AbilityReady", value);
}