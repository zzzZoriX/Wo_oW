using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected EnemyControls enemyControls;
    [SerializeField] protected AttackZone attackZone;
    protected EnemySettings Settings;
}