using UnityEngine;
using System.IO;

public class CameraRotator : MonoBehaviour
{
    private UserData _userData;
    private float _rotationX = 0f;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        _userData = DeserializeData.Deserialize<UserData>("Jsons/UserData");
    }

    private void Update() {
        if (PauseManager.Instance.GamePaused)
            return;
        
        
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
