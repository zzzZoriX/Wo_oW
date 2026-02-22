using UnityEngine;
using System.IO;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private CameraStats _camStats;
    private UserData _userData;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _userData = DeserializeData.Deserialize<UserData>("./Assets/Source/Data/UserData.json");
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        var mouseX = Input.GetAxis("Mouse X") * _userData.sensitivity;
        var mouseY = Input.GetAxis("Mouse Y") * _userData.sensitivity;

        _camStats.rotationY += mouseX;
        _camStats.rotationX += mouseY;
        _camStats.rotationX = Mathf.Clamp(_camStats.rotationX, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(-_camStats.rotationX, _camStats.rotationY, 0f);
    }
}
