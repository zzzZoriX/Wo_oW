using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public bool IsGrounded { get; private set; }
    public int DashCounter { get; private set; }
    public bool CanSlam { get; private set; }
    public float DashReloadTimer { get; private set; }

    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;
    private PlayerKeyConfig _playerKeyConfig;
    private PlayerConfig _playerConfig;


    public void SetConfig(PlayerConfig config)
        => _playerConfig = config;
    
    private void Start() {
        _playerKeyConfig = DeserializeData.Deserialize<PlayerKeyConfig>("Jsons/PlayerKeyConfig");

        DashCounter = 3;
    }

    private void Update() {
        if (PauseManager.Instance.GamePaused)
            return;
        
        
        CheckForGround();
        DashReload();
    }

    public void Jump(float jumpForce) {
        if (Input.GetKeyDown(_playerKeyConfig.JumpKC) && IsGrounded) {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            IsGrounded = false;
            CanSlam = true;
        }
    }

    public void Dash(Vector3 direction) {
        if (Input.GetKeyDown(_playerKeyConfig.DashKC) && DashCounter != 0) {
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
        if (Input.GetKeyDown(_playerKeyConfig.SlamKC) && CanSlam) {
            _rigidbody.AddForce(Vector3.down * _playerConfig.SlamForce, ForceMode.Impulse);
            CanSlam = false;
        }
    }

    private void CheckForGround() {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void DashReload() {
        if (DashCounter < _playerConfig.MaxDashCount)
        {
            DashReloadTimer += Time.deltaTime;
            if (DashReloadTimer >= _playerConfig.DashCooldown)
            {
                ++DashCounter;
                DashReloadTimer = 0f;
            }
        }
    }
}