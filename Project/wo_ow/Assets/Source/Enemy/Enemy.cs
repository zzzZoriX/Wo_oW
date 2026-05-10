using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected EnemyAnimator enemyAnimator;
    [SerializeField] protected EnemyControls enemyControls;
    [SerializeField] protected Zone attackZone;
    [SerializeField] private GameObject blood;
    [SerializeField] private float hitDistance;
    private Timer _bloodDestroyerTimer;
    protected EnemySettings Settings;


    private void Start() {
        _bloodDestroyerTimer = gameObject.AddComponent<Timer>();
        
        _bloodDestroyerTimer.DoWhile = false;
        _bloodDestroyerTimer.Set(2);
    }

    public override void Die() {
        var instantiatedBlood = Instantiate(blood, transform);

        var bloodParticles = instantiatedBlood.GetComponent<ParticleSystem>();
        
        bloodParticles.Emit(50);
        bloodParticles.Play();
        
        StartCoroutine(DestroyBlood(instantiatedBlood));
        
        AudioPlayer.GetPlayer().PlayOnce(AudioPlayer.GetPlayer().AvailableClips["DeadByLG"]);
        
        transform.GetChild(0).gameObject.SetActive(false);
        IsAlive = false;
        
        GameManager.Player.GetComponent<PlayerController>().CoolPoints.Add(CoolPoints, KillTypes.PlayerState.ConvertStateToKillTypes());
    }
    
    public virtual void Attack() { }
    
    private void OnTriggerEnter(Collider other) {
        if (!IsAlive) return;
        
        if (other.gameObject.CompareTag("PlayerProjectile"))
        {
            TakeDamage(other.gameObject.GetComponent<WeaponAttack>().Damage);
            Destroy(other.gameObject);

            // TODO: complete this code
            // transform.position = new Vector3(
            //     transform.position.x,
            //     transform.position.y,
            //     transform.position.z - 1 * hitDistance
            // );
        }
    }

    private IEnumerator DestroyBlood(GameObject instantiatedBlood) {
        yield return new WaitForSeconds(2);

        Destroy(instantiatedBlood);
        Destroy(gameObject);
    }
}