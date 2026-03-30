using UnityEngine;

public class Bullet : WeaponAttack
{
    public Vector3 Direction { get; private set; }

    public void Shoot(Vector3 direction, float lifeTime)
    {
        Direction = direction;
        
        Destroy(gameObject, lifeTime);
    }

    public static GameObject InstanceBullet(Vector3 position, GameObject prefab, Quaternion rotation)
    {
        return Instantiate(prefab, position, rotation);
    }

    private void Update() {
        if (PauseManager.Instance.GamePaused)
            return;
        
        
        transform.Translate(Direction, Space.Self);
    }
}
