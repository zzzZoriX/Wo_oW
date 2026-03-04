using UnityEngine;

public class EntityController : MonoBehaviour {
    public void MoveEntity(Vector3 direction, float moveSpeed)
        => transform.Translate(direction * moveSpeed * Time.deltaTime, Space.Self);

    public void RotateEntity(Quaternion rotation)
        => transform.rotation = rotation;
}