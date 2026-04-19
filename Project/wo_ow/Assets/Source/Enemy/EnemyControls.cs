using System;
using UnityEngine;

public class EnemyControls : MonoBehaviour {
    public float CurrentSpeed;
    
    protected GameObject target;
    protected Vector3 lastPosition;

    [SerializeField] private float rotateSpeed;


    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void MoveToTarget(float speed)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.transform.position,
            speed * Time.deltaTime
        );
    }

    public virtual void MoveAwayFromTarget(float speed) {
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(
                target.transform.position.x,
                0,
                target.transform.position.z
            ),
            speed * Time.deltaTime
        );
    }

    public virtual void RotateToTarget()
        => transform.rotation =
            Quaternion.RotateTowards(transform.rotation, target.transform.rotation, rotateSpeed * Time.deltaTime);
}