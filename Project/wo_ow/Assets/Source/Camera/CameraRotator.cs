using UnityEngine;
using System.IO;

public class CameraRotator : MonoBehaviour
{
    private UserData _userData;
    private float _rotationX = 0f;

    private void Start() {
        // Cursor.lockState = CursorLockMode.Locked;
        _userData = DeserializeData.Deserialize<UserData>("./Assets/Source/Data/UserData.json");
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        var mouseY = Input.GetAxis("Mouse Y") * _userData.sensitivity;

        _rotationX += mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(-_rotationX, 0f, 0f);
    }
}
