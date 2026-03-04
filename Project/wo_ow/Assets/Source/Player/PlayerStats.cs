using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Game")] 
    public HealthPoint HP;
    
    [Header("Movement")]
    public float _moveSpeed;
    public float _jumpForce;
    public float rotationY;
    
    public static Vector3 GetDirection()
    { 
        return new Vector3(
            Input.GetAxis("Horizontal"),
            0f,
            Input.GetAxis("Vertical")
        );
    }
}