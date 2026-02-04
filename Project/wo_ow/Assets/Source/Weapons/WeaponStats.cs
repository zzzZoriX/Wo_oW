using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [Header("Stats")] 
    public int damage;
    
    [Header("Const")]
    public GameObject player;
    public int destroyTime = 2;
    public KeyCode shootKey = KeyCode.Mouse0;

    [Header("Attack")] 
    public GameObject bullet;
    public int count;
    public float speed;
}
