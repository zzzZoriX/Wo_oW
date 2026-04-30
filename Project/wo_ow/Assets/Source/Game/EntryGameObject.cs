using System;
using UnityEngine;

public class EntryGameObject : MonoBehaviour {
    [SerializeField] private GameManager gameManagerObject;
    [SerializeField] private GameObject player;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            player.transform.position = Vector3.zero;
            
            gameManagerObject.StartGame();
        }
    }
}
