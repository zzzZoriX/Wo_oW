using UnityEngine;
using UnityEngine.AI;

public class NeonSoldier : Enemy {
    [SerializeField] private NavMeshAgent agent;
    
    private void Start() {
        Settings = DeserializeData.Deserialize<EnemySettings>("Jsons/NeonSoldierData");
        
        CoolPoints.Set(Settings.Points);
        
        Health.Initialize(Settings.HP);
    }

    private void Update() {
        if (PauseManager.Instance.GamePaused)
            return;
        
        if(attackZone.TagInAttackZone("Player"))
            enemyAnimator.SetHitTrigger();
        
        Move();
    }

    public override void Attack() {
        var player = attackZone.FindObjectInZone("Player"); 

        if (player == null) return;

        player.GetComponent<PlayerController>().TakeDamage(Settings.Damage);
    }

    private void Move() {
        if (!attackZone.TagInAttackZone("Player")) {
            agent.SetDestination(enemyControls.Target.transform.position);
            agent.isStopped = false;
        }
        else {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
        }
        
        enemyAnimator.SetBlend(agent.velocity.normalized.magnitude);
    }
}