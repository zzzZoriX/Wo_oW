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

        if (!stopZone.TagInAttackZone("Player")) {
            enemyControls.MoveToTarget(Settings.MoveSpeed);
        }
        else {
            enemyControls.CurrentSpeed = 0f;
        }
        
        enemyAnimator.SetBlend(enemyControls.CurrentSpeed);
    }
}