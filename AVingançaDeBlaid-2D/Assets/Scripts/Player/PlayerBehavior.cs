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

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        groundedChecker = GetComponent<IsGroundedChecker>();
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
        float movieDirection = GameMenager.Instance.inputMenager.Movement;
        transform.Translate(movieDirection * Time.deltaTime * movieSpead, 0, 0);
    }

}
