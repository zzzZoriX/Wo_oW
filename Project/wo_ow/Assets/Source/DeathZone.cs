using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
        => other.gameObject.GetComponent<Entity>().Die();
}