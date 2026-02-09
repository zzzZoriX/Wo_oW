using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private WeaponStats _stats;

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if(!Input.GetKeyDown(_stats.shootKey))
            return;
        
        var lazer = Instantiate(
            _stats.bullet,
            transform.position,
            transform.rotation
        );
        
        // lazer.GetComponent<Rigidbody>().velocity = lazer.transform.forward * _stats.speed;
        // lazer.transform.Translate(Vector3.forward * _stats.speed, Space.Self);
        
        lazer.GetComponent<Bullet>().Shoot(Vector3.forward * _stats.speed, _stats.destroyTime);
        
        Destroy(lazer, _stats.destroyTime);
    }

    private void Ability()
    {
        
    }

//  drawing aim ray
    private void OnDrawGizmos()
    {
        Debug.DrawRay(_stats.camera.transform.position, _stats.camera.transform.forward, Color.red);
    }
}