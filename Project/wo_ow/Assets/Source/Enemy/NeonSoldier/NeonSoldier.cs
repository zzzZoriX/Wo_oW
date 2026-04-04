using System;
using UnityEngine;

public class NeonSoldier : Enemy {
    private void Start() {
        Settings = DeserializeData.Deserialize<EnemySettings>("Jsons/NeonSoldierData");
        
        CoolPoints.Set(Settings.Points);
        
        Health.Initialize(Settings.HP);
    }

    private void Update() {
        if (PauseManager.Instance.GamePaused)
            return;
        
        
        UpdateActions();
    }

    protected override void UpdateActions() {
        base.UpdateActions();
        
        if(!zone.TagInAttackZone("Player"))
            enemyControls.MoveToTarget(Settings.MoveSpeed);
    }
}