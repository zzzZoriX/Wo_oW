using System;
using UnityEngine;
using KillTypes;

public class PlayerController : Entity {
    [Header("Stats")] 
    public PlayerStats _playerStats;

    [Header("Controllers")] 
    [SerializeField] private PlayerMovement _playerMovement;
    
    private UserData _userData;


    public sealed override void Die() {
        transform.Find("Main Camera").SetParent(null, true);
        
        base.Die();
    }

    private void Start()
    {
        _userData = DeserializeData.Deserialize<UserData>("Jsons/UserData");
        var config = DeserializeData.Deserialize<PlayerConfig>("Jsons/PlayerConfig");
        
        Health.Initialize(config.HP);
        
        _playerMovement.SetConfig(config);
        
        CoolPoints.Set();
    }
    
    private void Update() {
        if (PauseManager.Instance.GamePaused)
            return;
        
        SetStates();
        
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

    private void SetStates() {
        PlayerState.Moving = PlayerStats.GetDirection().magnitude > 0.1f || PlayerStats.GetDirection().magnitude < -0.1f;
        PlayerState.Falling = !_playerMovement.IsGrounded;
    }
}