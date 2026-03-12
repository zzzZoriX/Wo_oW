using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Temperature Temperature;
    public WeaponStats Stats;
    public WeaponStability Stability;
    public Transform ProjectileSpawnPoint;

    public virtual void Attack() {
    }

    protected virtual void OnReloadEnd()
    { }

    public GameObject GetSpecificChildren(string name)
    {
        Transform[] childrens = GetComponentsInChildren<Transform>();

        foreach (var children in childrens)
        {
            if (children.gameObject.name == name)
                return children.gameObject;
        }

        return null;
    }
}