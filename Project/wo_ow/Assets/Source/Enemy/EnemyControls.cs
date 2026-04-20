using System;
using UnityEngine;

public class EnemyControls : MonoBehaviour {
    public GameObject Target { get; private set; }

    [SerializeField] private float rotateSpeed;


    private void Start() {
        Target = GameObject.FindFirstObjectByType<PlayerController>().gameObject;
    }

    public virtual void MoveToTarget(float speed)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            Target.transform.position,
            speed * Time.deltaTime
        );
    }

    public virtual void MoveAwayFromTarget(float speed) {
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(
                Target.transform.position.x,
                0,
                Target.transform.position.z
            ),
            speed * Time.deltaTime
        );
    }

    public virtual void RotateToTarget()
        => transform.rotation =
            Quaternion.RotateTowards(transform.rotation, Target.transform.rotation, rotateSpeed * Time.deltaTime);
}