using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
    [Header("Movement")]
    public float _moveSpeed;
    public float _jumpForce;
    public bool isGrounded;
    public float rotationY;
    public float slamForce;
    public bool canDoSlam;
    
    [Header("Dash")]
    public float dashDistance;
    public int dashCounter = 3;
    public int maxDashes = 3;
    public float dashChargeCooldown = 1f;
    [NonSerialized] public float DashChargeTimer = 0f;
    
    [Header("Input")]
    public KeyCode dashKey = KeyCode.LeftShift;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode slamKey = KeyCode.LeftControl;
    
    public static Vector3 GetDirection()
    { 
        return new Vector3(
            Input.GetAxis("Horizontal"),
            0f,
            Input.GetAxis("Vertical")
        );
    }
}