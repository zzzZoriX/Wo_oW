using UnityEngine;

public class EntityController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;

    public void MoveEntity(Vector3 direction, float moveSpeed) {
        direction.Normalize();
        
        var cameraForward = Camera.main.transform.forward;
        var cameraRight = Camera.main.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        var moveDirection = cameraForward * direction.z + cameraRight * direction.x;
        moveDirection.Normalize();

        var velocity = moveDirection * moveSpeed;

        velocity.y = rigidbody.velocity.y;

        rigidbody.velocity = velocity;
    }

    public void RotateEntity(Quaternion rotation)
        => transform.rotation = rotation;
}