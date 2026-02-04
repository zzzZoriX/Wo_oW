
using UnityEngine;

public class CameraStats : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Stats")] 
    public Vector3 offset;
    public float followSpeed;
    public float rotationX = 0f;
    public float rotationY = 0f;
}
