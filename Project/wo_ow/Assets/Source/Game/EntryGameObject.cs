using System;
using UnityEngine;

public class EntryGameObject : MonoBehaviour {
    [SerializeField] private GameManager gameManagerObject;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
            gameManagerObject.StartGame();
        }
    }
}
