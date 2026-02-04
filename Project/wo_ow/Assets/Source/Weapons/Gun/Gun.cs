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
        if (Input.GetKeyDown(_stats.shootKey))
        {
            var lazer = Instantiate(
                _stats.bullet,
                transform.position,
                transform.rotation
            );
            
            lazer.GetComponent<Rigidbody>().AddForce(lazer.transform.forward * _stats.speed * Time.deltaTime, ForceMode.Impulse);
            
            Destroy(lazer, _stats.destroyTime);
        }
    }
}