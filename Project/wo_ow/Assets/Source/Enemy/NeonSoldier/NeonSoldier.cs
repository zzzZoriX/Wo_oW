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
}