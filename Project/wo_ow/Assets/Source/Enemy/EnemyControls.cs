using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    private Transform _target;

    private void Start() {
        _target = GameObject.Find("PlayerBase").transform;
    }

    public void MoveToTarget(float speed)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            _target.position,
            speed * Time.deltaTime
        );
    }

    public void MoveAwayFromTarget(float speed) {
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(
                _target.position.x,
                0,
                _target.position.z
            ),
            speed * Time.deltaTime
        );
    }

//  TODO:
    public void TeleportBehindTarget() {
        
    }

    public void TeleportInFrontOfTarget() {
        
    }
}