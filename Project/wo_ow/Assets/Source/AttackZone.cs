using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour {
    public bool SomeoneInAttackRange { get; private set; }
    public List<GameObject> ObjectsInZone { get; private set; }

    
    private void OnTriggerEnter(Collider other) {
        if (!SomeoneInAttackRange)
            SomeoneInAttackRange = true;
        
        ObjectsInZone.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        ObjectsInZone.Remove(other.gameObject);

        if (ObjectsInZone.Count == 0)
            SomeoneInAttackRange = false;
    }
}