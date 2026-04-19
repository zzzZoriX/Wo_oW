using UnityEngine;

public class NeonSoldierController : EnemyControls{
    public override void MoveToTarget(float speed) {
        var targetPosition = new Vector3(
            target.transform.position.x, transform.position.y, target.transform.position.z
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );
        
        CurrentSpeed = (transform.position.z - lastPosition.z) * 100;

        lastPosition = transform.position;
    }
}