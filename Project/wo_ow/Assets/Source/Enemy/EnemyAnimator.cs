using UnityEngine;

public class EnemyAnimator : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private Enemy enemy;

    public void AttackEventHandler() {
        enemy.Attack();
    }

    public void SetAttackTrigger() {
        animator.SetTrigger("Attack");
    }

    public void SetBlend(float blend)
        => animator.SetFloat("Blend", blend);
}