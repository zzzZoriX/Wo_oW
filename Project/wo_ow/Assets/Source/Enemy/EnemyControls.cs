using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    protected Transform target;

    private void Start() {
        target = GameObject.Find("PlayerBase").transform;
    }

    public virtual void MoveToTarget(float speed)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );
    }

    public void MoveAwayFromTarget(float speed) {
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(
                target.position.x,
                0,
                target.position.z
            ),
            speed * Time.deltaTime
        );
    }
}