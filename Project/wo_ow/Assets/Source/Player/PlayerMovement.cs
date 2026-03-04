using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;
    private PlayerKeyConfig _playerKeyConfig;
    private PlayerConfig _playerConfig;
    
    [Header("Stats")]
    [SerializeField] private bool _isGrounded;
    [SerializeField] private int _dashCounter;
    [SerializeField] private bool _canSlam;
    [SerializeField] private float _dashReloadTimer = 0f;

    private void Start() {
        _playerKeyConfig = DeserializeData.Deserialize<PlayerKeyConfig>("./Assets/Source/Data/PlayerKeyConfig.json");
        _playerConfig = DeserializeData.Deserialize<PlayerConfig>("./Assets/Source/Data/PlayerConfig.json");

        _dashCounter = 3;
    }

    private void Update() {
        CheckForGround();
        DashReload();
    }

    public void Jump(float jumpForce) {
        if (Input.GetKeyDown(_playerKeyConfig.JumpKC) && _isGrounded) {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            _isGrounded = false;
            _canSlam = true;
        }
    }

    /// <param name="direction">
    /// must be normilized
    /// </param>
    public void Dash(Vector3 direction) {
        if (Input.GetKeyDown(_playerKeyConfig.DashKC) && _dashCounter != 0) {
            var dashDir = direction.magnitude > 0.1f ? 
                direction.normalized : transform.forward;
            var space = direction.magnitude > 0.1f ? Space.Self : Space.World;
            
            dashDir.y = 0f;
            dashDir.Normalize();
        
            var actualDistance = _playerConfig.DashDistance;
        
            if (Physics.Raycast(transform.position, dashDir, out var hitInfo, _playerConfig.DashDistance))
                actualDistance = hitInfo.distance - 0.5f;
        
            transform.Translate(dashDir * actualDistance, space); 
            
            --_playerConfig.DashDistance;
        }
    }

    public void GroundSlam() {
        if (Input.GetKeyDown(_playerKeyConfig.SlamKC) && _canSlam) {
            _rigidbody.AddForce(Vector3.down * _playerConfig.SlamForce, ForceMode.Impulse);
            _canSlam = false;
        }
    }

    private void CheckForGround() {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void DashReload() {
        if (_dashCounter < _playerConfig.MaxDashCount)
        {
            _dashReloadTimer += Time.deltaTime;
            if (_dashReloadTimer >= _playerConfig.DashCooldown)
            {
                ++_dashCounter;
                _dashReloadTimer = 0f;
            }
        }
    }
}