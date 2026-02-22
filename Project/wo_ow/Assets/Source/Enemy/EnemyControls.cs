using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    [SerializeField] private Transform _target;

    public void MoveToTarget(float speed)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            _target.position,
            speed * Time.deltaTime
        );
    }
}