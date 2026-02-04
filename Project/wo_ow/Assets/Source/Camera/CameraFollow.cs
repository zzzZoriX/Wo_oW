using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private CameraStats _camStats;
    
    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        transform.position = Vector3.Lerp(
            transform.position, 
            _camStats.target.position + _camStats.offset, 
            _camStats.followSpeed * Time.deltaTime
        );
    }
}
