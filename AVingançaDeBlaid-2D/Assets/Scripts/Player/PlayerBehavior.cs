using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float movieSpead = 10;
    [SerializeField] private float jumpForce = 3;

    [Header("Propriedades de ataque")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private LayerMask attackLayer;

    private float movieDirection;
    private Rigidbody2D rigidbody;
    private IsGroundedChecker groundedChecker;
    private Health health;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        groundedChecker = GetComponent<IsGroundedChecker>();
        health = GetComponent<Health>();
        health.OnHurt += PlayHurtSound;
        health.OnDead += HandlePlayerDeath;
    }


    private void Start()
    {
        GameMenager.Instance.inputMenager.OnJump += HandleJump;
    }

    private void HandleJump()
    {
        if (!groundedChecker.IsGrounded())
            return;

        GameMenager.Instance.AudioManager.PlaySFX(SFX.PlayerJump);
        rigidbody.velocity = Vector2.up * jumpForce;
    }

    private void Update()
    {
        MovePlayer();
        FlipPlayer();
    }
    private void MovePlayer()
    {
        GameMenager.Instance.AudioManager.PlaySFX(SFX.PayerWalk);
        movieDirection = GameMenager.Instance.inputMenager.Movement;
        transform.Translate(movieDirection * Time.deltaTime * movieSpead, 0, 0);
    }

    private void FlipPlayer()
    {
        movieDirection = GameMenager.Instance.inputMenager.Movement;

        if (movieDirection < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = Vector3.one;
    }
    private void HandlePlayerDeath()
    {
        GameMenager.Instance.AudioManager.PlaySFX(SFX.PlayerDeath);
        GetComponent<Collider2D>().enabled = false;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        GameMenager.Instance.inputMenager.DisablePlayerInput();
    }

    private void PlayHurtSound()
    {
        GameMenager.Instance.AudioManager.PlaySFX(SFX.PlayerHurt);
    }
    private void PlayWalkSound()
    {
        GameMenager.Instance.AudioManager.PlaySFX(SFX.PayerWalk);
    }

    private void Attack()
    {
        GameMenager.Instance.AudioManager.PlaySFX(SFX.PlayerDeath);
        Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, attackLayer);
        print("Making enemy take damage");
        print(hittedEnemies.Length);

        foreach (Collider2D hittedEnemy in hittedEnemies)
        {
            print("Checking enemy");
            if (hittedEnemy.TryGetComponent(out Health enemyHealth))
            {
                print("Getting damage");
                enemyHealth.TakeDamage();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

}
