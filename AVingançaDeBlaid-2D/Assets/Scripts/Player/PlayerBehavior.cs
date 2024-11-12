using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 3;

    [Header("Propriedades de ataque")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private ParticleSystem hitParticle;

    private float moveDirection;

    private Rigidbody2D rigidbody;
    private Health health;
    private IsGroundedChecker isGroundedCheker;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        isGroundedCheker = GetComponent<IsGroundedChecker>();
        health = GetComponent<Health>();
        health.OnHurt += HandleHurt;
        health.OnDead += HandlePlayerDeath;
    }

    private void Start()
    {
        GameManager.Instance.InputManager.OnJump += HandleJump;
        UpdateLives(health.GetLives());

    }

    private void FixedUpdate()
    {
        MovePlayer();
        FlipSpriteAccordingToMoveDirection();
    }

    private void MovePlayer()
    {
        moveDirection = GameManager.Instance.InputManager.Movement;
        Vector2 directionToMove = new Vector2(moveDirection * moveSpeed, rigidbody.velocity.y);
        rigidbody.velocity = directionToMove;
    }

    private void FlipSpriteAccordingToMoveDirection()
    {
        if (moveDirection < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection > 0)
        {
            transform.localScale = Vector3.one;
        }
    }

    private void HandleJump()
    {
        if (isGroundedCheker.IsGrounded() == false) return;
        rigidbody.velocity += Vector2.up * jumpForce;
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerJump);
    }

    private void HandleHurt()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerHurt);
        PlayHitParticle();
        UpdateLives(health.GetLives());
    }

    private void HandlePlayerDeath()
    {
        GetComponent<Collider2D>().enabled = false;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerDeath);
        GameManager.Instance.InputManager.DisablePlayerInput();
        PlayHitParticle();
        UpdateLives(health.GetLives());
    }
    private void UpdateLives(int amount)
    {
        GameManager.Instance.UpdateLives(amount);
    }

    private void Attack()
    {
        Collider2D[] hittedEnemies =
            Physics2D.OverlapCircleAll(attackPosition.position, attackRange, attackLayer);

        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerAttack);

        foreach (Collider2D hittedEnemy in hittedEnemies)
        {
            if (hittedEnemy.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage();
            }
        }
    }

    private void PlayWalkSound()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerWalk);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
    private void PlayHitParticle()
    {
        ParticleSystem instantiatedParticle = Instantiate(hitParticle, transform.position, transform.rotation);
        instantiatedParticle.Play();
    }
}
