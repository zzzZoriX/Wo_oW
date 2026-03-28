using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class AttackZone : MonoBehaviour {
    public bool SomeoneInAttackRange { get; private set; }
    public List<GameObject> ObjectsInZone { get; private set; }


    private void Start() {
        ObjectsInZone = new List<GameObject>();
    }

    [CanBeNull] 
    public GameObject FindObjectInZone(string tag)
        => ObjectsInZone.Find(go => {
            return go.CompareTag(tag);
        });

    public bool TagInAttackZone(string tag) {
        foreach (var go in ObjectsInZone) {
            if (go != null && go.CompareTag(tag))
                return true;
        }

        return false;
    }

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