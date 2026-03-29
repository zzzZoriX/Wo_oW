using System;
using UnityEngine;

public class NeonSoldier : Enemy {
    private void Start()
    {
        Settings = DeserializeData.Deserialize<EnemySettings>("Jsons/NeonSoldierData");
        Health.Initialize(Settings.HP);
    }

    private void Update()
        => UpdateActions();

    protected override void UpdateActions() {
        base.UpdateActions();
        
        if(!attackZone.TagInAttackZone("Player"))
            enemyControls.MoveToTarget(Settings.MoveSpeed);
    }
}