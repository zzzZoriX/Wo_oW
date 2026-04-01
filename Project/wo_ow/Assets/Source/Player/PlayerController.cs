using System;
using UnityEngine;

public class PlayerController : Entity {
    [Header("Stats")] 
    public PlayerStats _playerStats;

    [Header("Controllers")] 
    [SerializeField] private PlayerMovement _playerMovement;
    
    private UserData _userData;


    protected sealed override void Die() {
        transform.Find("Main Camera").SetParent(null, true);
        
        base.Die();
    }

    private void Start()
    {
        _userData = DeserializeData.Deserialize<UserData>("Jsons/UserData");
        var config = DeserializeData.Deserialize<PlayerConfig>("Jsons/PlayerConfig");
        
        Health.Initialize(config.HP);
        
        _playerMovement.SetConfig(config);
    }
    
    private void Update() {
        if (PauseManager.Instance.GamePaused)
            return;
        
        
        var direction = PlayerStats.GetDirection();
     
        Controller.MoveEntity(direction, _playerStats._moveSpeed);
        _playerMovement.Jump(_playerStats._jumpForce);
        
        _playerMovement.GroundSlam();
        
        _playerMovement.Dash(direction);
        
        Rotate();
    }

    private void Rotate()
    {
        var mouseX = Input.GetAxis("Mouse X") * _userData.sensitivity;
        
        _playerStats.rotationY += mouseX;
        
        Controller.RotateEntity(Quaternion.Euler(0, _playerStats.rotationY, 0f));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("EnemyProjectile")) {
            TakeDamage(other.GetComponent<Bullet>().Damage);
            Destroy(other.gameObject);
        }
    }
}