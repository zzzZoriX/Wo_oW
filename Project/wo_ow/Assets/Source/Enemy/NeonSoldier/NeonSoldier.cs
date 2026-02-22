using System;
using UnityEngine;

public class NeonSoldier : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private void Start()
    {
        _enemy.Stats = DeserializeData.Deserialize<EnemyStats>("./Assets/Source/Data/NeonSoldierData.json");
    }

    private void Update()
    {
        _enemy.EnemyControls.MoveToTarget(_enemy.Stats.MoveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            _enemy.TakeDamage(other.gameObject.GetComponent<Bullet>().Damage);
            Destroy(other.gameObject);
        }
    }
}