using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected EnemyControls enemyControls;
    [SerializeField] protected Zone stopZone;
    [SerializeField] protected EnemyAnimator enemyAnimator;
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
        StartAttackAnimation();
        enemyControls.RotateToTarget();
    }

    public void Attack() {
        if (!stopZone.TagInAttackZone("Player")) return;

        
        var spawnPos = transform;
        spawnPos.position += new Vector3(0, 0, 0.7f);

        var attackZoneObject = Instantiate(
            new GameObject("AttackZone"),
            spawnPos
        );

        
        var attackZone = attackZoneObject.AddComponent<BoxCollider>();

        attackZone.size = new Vector3(2f, 2.2f, 1f);
        
        var zoneComponent = attackZoneObject.AddComponent<Zone>();
        

        var player = zoneComponent.FindObjectInZone("Player");

        if (player != null)
            player.GetComponent<PlayerController>().TakeDamage(Settings.Damage);

        
        Destroy(attackZoneObject);
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

    private void StartAttackAnimation() {
        if (stopZone.SomeoneInAttackRange && stopZone.TagInAttackZone("Player"))
            enemyAnimator.SetAttackTrigger();
    }
}