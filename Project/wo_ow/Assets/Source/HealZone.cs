using System;
using UnityEngine;

public class HealZone : Zone {
    [SerializeField] private int heal;
    private int _healCount = 1;
    
    private void Update() {
        if (TagInAttackZone("Player") && _healCount != 0) {
            FindObjectInZone("Player")?.GetComponent<PlayerController>().Health.Heal(heal);

            --_healCount;
        }
    }
}