using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Temperature Temperature;
    public WeaponStats Stats;
    public WeaponStability Stability;
    public Transform ProjectileSpawnPoint;
    
    protected virtual void Shoot()
    { }

    protected virtual void Ability()
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