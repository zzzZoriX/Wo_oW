using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected EnemyControls enemyControls;
    [SerializeField] protected Zone zone;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private GameObject blood;
    private Timer _bloodDestroyerTimer;
    protected EnemySettings Settings;


    public override void Die() {
        var instantiatedBlood = Instantiate(blood, transform);

        var bloodParticles = instantiatedBlood.GetComponent<ParticleSystem>();
        
        bloodParticles.Emit(50);
        bloodParticles.Play();
        
        StartCoroutine(DestroyBlood(instantiatedBlood));
        
        transform.GetChild(0).gameObject.SetActive(false);
        IsAlive = false;
        
        GameManager.Player.GetComponent<PlayerController>().CoolPoints.Add(CoolPoints, KillTypes.PlayerState.ConvertStateToKillTypes());
    }


    private void Start() {
        enemyControls.RotateToTarget();

        _bloodDestroyerTimer = gameObject.AddComponent<Timer>();
        
        _bloodDestroyerTimer.DoWhile = false;
        _bloodDestroyerTimer.Set(2);
    }

    private void Update() {
        if (PauseManager.Instance.GamePaused || !IsAlive)
            return;
        
        
        UpdateActions();
    }

    protected virtual void UpdateActions() {
        Attack();
        enemyControls.RotateToTarget();
    }

    private void Attack() {
        if (!zone.SomeoneInAttackRange)
            return;

        if(!zone.TagInAttackZone("Player"))
            return;
        
        _weapon.Attack();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerProjectile"))
        {
            TakeDamage(other.gameObject.GetComponent<WeaponAttack>().Damage);
            Destroy(other.gameObject);
        }
    }

    private IEnumerator DestroyBlood(GameObject instantiatedBlood) {
        yield return new WaitForSeconds(2);

        Destroy(instantiatedBlood);
        Destroy(gameObject);
    }
}