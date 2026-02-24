using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Temperature Temperature;
    public WeaponStats Stats;
    public WeaponStability Stability;
    public Transform ProjectileSpawnPoint;
    
    public virtual void Shoot()
    { }

    public virtual void Ability()
    { }

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