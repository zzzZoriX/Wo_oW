using UnityEngine;

public class DeathZone : MonoBehaviour {
    private void OnTriggerEnter(Collider other) 
        => other.gameObject.GetComponent<PlayerController>().TakeDamage(1000);
}