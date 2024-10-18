using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MeleeEnemy : BaseEnemy
{

    [SerializeField] private Transform detectPosition;
    [SerializeField] private Vector2 detectBoxSize;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackCooldown;

    [Header("Audio properties")]
    [SerializeField] private AudioClip audioAttack;
    [SerializeField] private AudioClip audioHit;
    [SerializeField] private AudioClip audioDie;

    private float cooldownTimer;
    protected override void Awake()
    {
        base.Awake();
        base.health.OnHurt += PlayHurtAudio;
        base.health.OnDead += PlayDeadAudio;
    }

    protected override void Update()
    {
        cooldownTimer += Time.deltaTime;
        VerifyCanAttack();
    }

    private void VerifyCanAttack()
    {
        if (cooldownTimer < attackCooldown)
            return;
        if (PlayerInSight())
        {
            animator.SetTrigger("attack");
            AttackPlayer();
        }
    }

    private bool PlayerInSight()
    {
        Collider2D playerCollider = CheckPlayerInDetectArea();
        return playerCollider != null;
    }

    private void OnDrawGizmos()
    {
        if (detectPosition == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectPosition.position, detectBoxSize);
    }

    private void AttackPlayer()
    {
        if (CheckPlayerInDetectArea().TryGetComponent(out Health playerHealth))
        {
            playerHealth.TakeDamage();
            cooldownTimer = 0;
            audioSource.clip = audioAttack;
            audioSource.Play();
        }
    }

    private Collider2D CheckPlayerInDetectArea()
    {
        return Physics2D.OverlapBox(detectPosition.position, detectBoxSize, 0f, playerLayer);
    }

    private void PlayHurtAudio()
    {
        audioSource.clip = audioHit;
        audioSource.Play();
    }

    private void PlayDeadAudio()
    {
        audioSource.clip = audioDie;
        audioSource.Play();
    }
}
