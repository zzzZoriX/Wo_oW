using UnityEngine;

public class DeathZone : MonoBehaviour {
    private void OnTriggerEnter(Collider other) 
        => other.gameObject.GetComponent<Entity>().Die();
}