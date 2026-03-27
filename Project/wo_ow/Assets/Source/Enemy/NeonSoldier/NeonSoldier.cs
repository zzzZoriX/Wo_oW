using System;
using UnityEngine;

public class NeonSoldier : Enemy {
    private void Start()
    {
        Settings = DeserializeData.Deserialize<EnemySettings>("./Assets/Source/Data/NeonSoldierData.json");
        Health.Initialize(Settings.HP);
    }

    private void Update()
        => UpdateActions();

    protected override void UpdateActions() {
        base.UpdateActions();
        
        enemyControls.MoveToTarget(Settings.MoveSpeed);
    }
}