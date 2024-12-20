using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    [Header("Attack properties")]
    [SerializeField] private float AttackRanger = 1f;
    [SerializeField] private float AttackSize = 1f;
    [SerializeField] private Vector3 AttackOffset;
    [SerializeField] private LayerMask AttackMask;
    [SerializeField] private ParticleSystem hitParticle;

    private AudioSource audioSource;

    [Header("Audio properties")]
    [SerializeField] private AudioClip[] audioClips;

    private Vector3 attackPosition;

    private Rigidbody2D rigidbody;
    private Transform playerPosition;
    private Health health;
    private Animator animator;

    private bool canAttack = false;
    private bool isFlipped = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        health.OnHurt += BossHurt;
        health.OnDead += BossDeath;
    }

    private void Start()
    {
        playerPosition = GameManager.Instance.GetPlayer().transform;
    }
    public void FollowPlayer()
    {
        Vector2 target = new Vector2(playerPosition.position.x, transform.position.y);
        Vector2 newPos = Vector2.MoveTowards(rigidbody.position, target, moveSpeed * Time.fixedDeltaTime);
        rigidbody.MovePosition(newPos);
        LookAtPlayer();
        CheckPositionFromPlayer();
    }

    private void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > playerPosition.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < playerPosition.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    private void CheckPositionFromPlayer()
    {
        float distanceFromPlayer = Vector2.Distance(playerPosition.position, transform.position);
        if (distanceFromPlayer <= AttackRanger)
            canAttack = true;
        else
            canAttack = false;
    }

    private void Attack()
    {
        attackPosition = transform.position;
        attackPosition += transform.right * AttackOffset.x;
        attackPosition += transform.up * AttackOffset.y;

        Collider2D collisionInfo = Physics2D.OverlapCircle(attackPosition, AttackSize, AttackMask);
        if (collisionInfo != null)
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
            collisionInfo.GetComponent<Health>().TakeDamage();
        }
    }

    public bool GetCanAttack()
    {
        return canAttack;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition, AttackSize);
    }

    private void BossHurt()
    {
        PlayHitParticle();
        audioSource.clip = audioClips[1];
        audioSource.Play();
        animator.SetTrigger("hurt");
    }

    private void BossDeath()
    {
        PlayHitParticle();
        audioSource.clip = audioClips[2];
        audioSource.Play();
        animator.SetTrigger("dead");
    }

    public void StartChasing()
    {
        animator.SetBool("canChase", true);
    }

    private void PlayHitParticle()
    {
        ParticleSystem instantiatedParticle = Instantiate(hitParticle, transform.position, transform.rotation);
        instantiatedParticle.Play();
    }
}
