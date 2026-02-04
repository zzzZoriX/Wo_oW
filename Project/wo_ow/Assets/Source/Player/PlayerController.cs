using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;
    
    [Header("Stats")] 
    [SerializeField] private PlayerStats _playerStats;
    
    private UserData _userData;


    private void Start()
    {
        _userData = UserData.Deserialize("./Assets/Source/UserData.json");
    }
    
    private void Update()
    {
        var direction = PlayerStats.GetDirection();
        
        CheckForGround();
     
        Move(direction);
        Jump();
        
        GroundSlam();
        
        Dash(direction.normalized);
        DashRecharge();
        
        Rotate();
    }

//  Direction must be normalized
    private void Dash(Vector3 direction)
    {
        if (Input.GetKeyDown(_playerStats.dashKey) && _playerStats.dashCounter > 0)
        {
            var dashDir = direction.magnitude > 0.1f ? 
                direction.normalized : transform.forward;
            var space = direction.magnitude > 0.1f ? Space.Self : Space.World;
            
            dashDir.y = 0f;
            dashDir.Normalize();
        
            RaycastHit hitInfo;
            var actualDistance = _playerStats.dashDistance;
        
            if (Physics.Raycast(transform.position, dashDir, out hitInfo, _playerStats.dashDistance))
            {
                actualDistance = hitInfo.distance - 0.5f;
            }
        
            // transform.localPosition += dashDir * actualDistance;            
            transform.Translate(dashDir * actualDistance, space); 
            
            --_playerStats.dashCounter;
        }
    }

    private void DashRecharge()
    {
        if (_playerStats.dashCounter < _playerStats.maxDashes)
        {
            _playerStats.DashChargeTimer += Time.deltaTime;
            if (_playerStats.DashChargeTimer >= _playerStats.dashChargeCooldown)
            {
                ++_playerStats.dashCounter;
                _playerStats.DashChargeTimer = 0f;
            }
        }
    }

    private void Move(Vector3 direction)
    {
        transform.Translate(direction * Time.deltaTime * _playerStats._moveSpeed, Space.Self);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(_playerStats.jumpKey) && _playerStats.isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _playerStats._jumpForce, ForceMode.Impulse);
            
            _playerStats.isGrounded = false;
            _playerStats.canDoSlam = true;
        }
    }

    private void CheckForGround()
    {
        _playerStats.isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (_playerStats.canDoSlam)
        {
            _playerStats.canDoSlam = !_playerStats.isGrounded;
        }
    }

    private void Rotate()
    {
        var mouseX = Input.GetAxis("Mouse X") * _userData.sensitivity;
        
        _playerStats.rotationY += mouseX;
        transform.rotation = Quaternion.Euler(0, _playerStats.rotationY, 0f);
    }

    private void GroundSlam()
    {
        if (Input.GetKeyDown(_playerStats.slamKey) && !_playerStats.isGrounded)
        {
            _rigidbody.AddForce(Vector3.down * _playerStats.slamForce, ForceMode.Impulse);
            _playerStats.canDoSlam = false;
        }
    }
}