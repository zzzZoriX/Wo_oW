using System;
using UnityEngine;

public class HealZone : Zone {
    [SerializeField] private int heal;
    
    private void Update() {
        if (TagInAttackZone("Player")) {
            FindObjectInZone("Player")?.GetComponent<PlayerController>().Health.Heal(heal);
        }
    }
}