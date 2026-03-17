using UnityEngine;

public class NeonSoldierController : EnemyControls{
    public override void MoveToTarget(float speed) {
        var targetPosition = new Vector3(
            target.position.x, transform.position.y, target.position.z
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );
    }
}