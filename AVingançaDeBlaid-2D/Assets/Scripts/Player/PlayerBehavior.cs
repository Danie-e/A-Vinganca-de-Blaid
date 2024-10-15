using System;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private float movieSpead = 10;

    [SerializeField]
    private float jumpForce = 3;

    private Rigidbody2D rigidbody;
    private IsGroundedChecker groundedChecker;
    float movieDirection;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        groundedChecker = GetComponent<IsGroundedChecker>();
        GetComponent<Health>().OnDead += HandlePlayerDeath;
    }

    private void Start()
    {
        GameMenager.Instance.inputMenager.OnJump += HandleJump;
    }

    private void HandleJump()
    {
        if (!groundedChecker.IsGrounded())
            return;

        rigidbody.velocity = Vector2.up * jumpForce;
    }

    private void Update()
    {
        MovePlayer();
        FlipPlayer();
    }
    private void MovePlayer()
    {
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
        GetComponent<Collider2D>().enabled = false;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        GameMenager.Instance.inputMenager.DisablePlayerInput();
    }

}
