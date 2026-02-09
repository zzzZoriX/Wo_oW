using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 Direction { get; private set; }

    public void Shoot(Vector3 direction, float lifeTime)
    {
        Direction = direction;
        
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Direction, Space.Self);
    }
}
